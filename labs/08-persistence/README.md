# Lab 08 — Persistence & async

**Today:** your capstone starts remembering things between runs, and `await` finally gets explained instead of copied.

**Required feature (matrix row 8):** an async save/load round-trip that survives a restart

> **In your project:** open your theme's spec ([the 21 themes](https://github.com/x86bogdan/fcpl-2026/blob/main/themes/README.md)) → *What each session adds* → row **8** (your own idea: the required feature above, said for your app). Before you write code, put three lines in your README: which of your classes change today, what they do in your app, and where you'll see it working.

---

## 0. Start (5 min)

**On a lab PC (browser only, no git):** your repository on GitHub → **Code → Download ZIP** → extract it → open the folder in Rider. Then, in **Rider's terminal** (**Alt+F12**):

```
sh sq.sh update
sh sq.sh test
```

**On your laptop, with git:** in your repository folder,

```
git pull
sq update          (Linux/macOS: sh sq.sh update)
sq test
```

Lab 08 shows as *not started*; Labs 02–07 should still be green. **Today more than any other week, start green.** Three new ideas in one evening is enough without also fixing Lab 05.

**Looking for the syntax?** The [C# phrasebook](https://x86bogdan.github.io/fcpl-2026/cheatsheet/#lab08) has today's C# next to Java and C++, with the errors you're likely to meet.

## 1. The bug this lab is named after (10 min)

Here is one line that looks completely reasonable:

```csharp
var json = JsonSerializer.Serialize(pokedex);     // the store itself
```

It compiles. It runs. It writes a file. And the file contains, per entry, an Id and a display name — **and nothing else** — because your store is an `IEnumerable<IItem>`, and `System.Text.Json` writes the *declared* element type. `IItem` declares two properties, so two properties is what you get. Species, level, nickname, owner: gone, quietly, with no error anywhere.

Then you read it back and it stops being quiet:

```
NotSupportedException: The collection type 'Pokedex' is abstract, an interface, or is read only,
                       and could not be instantiated and populated.
NotSupportedException: Deserialization of interface or abstract types is not supported.
                       Type 'Pokemon'.
```

Two different refusals, both of them fair. Your store isn't a list it can fill, and your base type is abstract — which it is *because* Lab 04 made it abstract, on purpose, correctly.

**The fix isn't a clever serializer setting.** It's to write down what you actually mean: one row per item, with a field for everything the item carries, and something in the row that says which kind it is. You already have that: the DTO and mapper from Lab 02, which Lab 04 taught to tell your kinds apart.

## 2. Save and load (25 min)

```csharp
public async Task SaveAsync(IItemStore store, string path)
{
    ArgumentNullException.ThrowIfNull(store);
    ArgumentException.ThrowIfNullOrWhiteSpace(path);

    if (Path.GetDirectoryName(Path.GetFullPath(path)) is { Length: > 0 } folder)
        Directory.CreateDirectory(folder);          // asking the user to mkdir first is rude

    var rows = store.All().Select(ToRow).ToList();  // your rows, not the store object

    await using var stream = File.Create(path);
    await JsonSerializer.SerializeAsync(stream, rows, Options);
}
```

`LoadAsync` is the same in reverse, with two decisions in it that are the whole of step 4.

**The contract compares your items property by property**, both ways round the trip, for every kind you declared in Lab 04. A field with no row in your DTO is a field you lose, and the test will tell you which one by name. (Writing this lab, the reference lost `Habitat` on the first run and the owner on the second. The check found both.)

## 3. `await`, for real this time (20 min)

You've been using `async` since Lab 02's stretch without anyone explaining it. Here it is.

A method marked `async` that returns `Task` doesn't run on another thread. It runs on *this* one, until it hits an `await` on something that hasn't finished — and then it **returns**, giving the thread back, and picks up where it left off when the disk (or the network) is done. That's the whole idea: not "faster", but "not sitting there".

Which means the two ways to fake it are both worse than not bothering:

```csharp
public Task SaveAsync(IItemStore store, string path)
{
    File.WriteAllText(path, Json(store));    // blocks the thread…
    return Task.CompletedTask;               // …and hands back a task that's already done
}

var rows = JsonSerializer.DeserializeAsync<List<Row>>(stream).Result;   // blocks, and can deadlock
```

The contract reads your compiled code for both: your save and load must reach a real `…Async` call in `System.IO` or `System.Text.Json`, and neither may block on a task with `.Result` or `.Wait()`. Next week's UI and the week after's web server are where `.Result` stops being merely wasteful and starts being a hang.

> `await using` on the file stream is doing something too — closing the handle when the block ends, even if the write throws. That's `IDisposable`, and it's why `using` exists. One sentence today; it comes back when it earns more.

## 4. Nothing saved yet, and a file full of nonsense (15 min)

Two failures that look similar in the debugger and are opposites in real life:

| What happened | What it means | What to do |
|---|---|---|
| The file isn't there | Nothing saved yet — the first run of every program ever written | Hand back an **empty store**. Not an error |
| The file is there and unreadable | Something went wrong: a crash mid-save, an edit, the wrong file | **Throw `InvalidDataException`** |

Treating the second as the first is how somebody loses a semester's work and doesn't find out for a week. Your program quietly starting empty is the scariest thing it can do.

A folder that doesn't exist is the first case too — `File.Exists` says false either way, so one check covers both.

## 5. Push (5 min)

```
sq test
git add . && git commit -m "Lab 08: persistence" && git push
```

**No git (lab PC)?** `sh sq.sh test`, then `sh sq.sh upload`, then **Add file → Upload files** on your repository's page: drag in what it gathered, **Commit changes**. Step by step: [working on a lab PC](https://github.com/x86bogdan/fcpl-2026/blob/main/LAB-PC.md).

**On time** means green in your repository's **Actions** tab by **Friday 23:59** this week, whichever group you're in. After that it's late work, worth up to 8.

## Done means

See `ACCEPTANCE.md`. In short: Lab 08 at full marks, Labs 02–07 still green, CI green.

## Missed session 7 — or this one?

`catchup/` in your repository has a **catch-up kit** for the labs that later labs lean on. Copy its files into `src/Capstone.Core/CatchUp/` and carry on. It earns you nothing for the lab it belongs to — the contract tests know the namespace and say so by name, and a wiring class of your own that hands back the kit's objects is reported too. To get those points, write your own and be verified in S13 or S14.

## Stretch

**Save without stopping the world.** Right now a save rewrites the whole file. Write to a temporary file and move it over the real one when it's complete (`File.Move(tmp, path, overwrite: true)`) — now a crash halfway through leaves the old file intact instead of half a new one. That is what every serious program does, it's four lines, and it turns your `Half_a_file_is_an_error` test from a rule into a thing that can't happen.

Or: **make the format someone else's problem.** Add a second implementation that saves CSV instead of JSON, behind the same two methods, and let the caller pick. If that's hard, your save and load know too much about each other.

---

**Next session, S9:** a window. The same model, the same store, and a user interface that shows what's in it — including, now, what was in it yesterday.
