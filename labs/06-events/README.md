# Lab 06 — Functions as values & events

**Today:** two ideas that look unrelated and turn out to be the same one. A function can be a *value* you pass to someone else's code. An event is a *list of functions* other people handed you, called when something happens.

**Required feature (matrix row 6):** one publisher and two subscribers; one function passed as a parameter

> **In your project:** open your theme's spec ([the 21 themes](https://github.com/x86bogdan/fcpl-2026/blob/main/themes/README.md)) → *What each session adds* → row **6** (your own idea: the required feature above, said for your app). Before you write code, put three lines in your README: which of your classes change today, what they do in your app, and where you'll see it working.

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

Lab 06 shows as *not started*; Labs 02–05 should still be green.

**Project route:** today is **checkpoint 2**. See `CHECKPOINT-2.md` — bring your repository, green where it can be.

**Looking for the syntax?** The [C# phrasebook](https://x86bogdan.github.io/fcpl-2026/cheatsheet/#lab06) has today's C# next to Java and C++, with the errors you're likely to meet.

## 1. Functions as parameters (25 min)

You've been passing functions around since Lab 05's stretch (`store.Where(...)`), probably without noticing. Now you write the other side: code that *takes* a function.

```csharp
public IReadOnlyList<IItem> Where(IItemStore store, Func<IItem, bool> keep)
{
    var kept = new List<IItem>();
    foreach (var item in store)
        if (keep(item))          // the caller's rule, not yours
            kept.Add(item);
    return kept;
}
```

Three of them today:

| Signature | Reads as |
|---|---|
| `Where(store, Func<IItem, bool> keep)` | "give me the ones this rule says yes to" |
| `Map(store, Func<IItem, string> describe)` | "turn each one into a string, this way" |
| `ForEach(store, Action<IItem> action)` | "do this to each one" |

`Func<A, B>` takes an `A` and returns a `B`. `Action<A>` takes an `A` and returns nothing. That's the whole vocabulary.

**The rules your code has to keep:** call the function once per item, in store order, and never second-guess it. A null function is the caller's bug: `ArgumentNullException`.

## 2. A lambda remembers where it came from (10 min)

```csharp
var total = 0;
ForEach(store, item => total += item.DisplayName.Length);   // total is the one out here
```

That's a *closure*: the lambda keeps a live link to the variables around it, not a copy. It's the feature that makes small functions useful, and the one that produces the strangest bugs when the variable changes later.

## 3. The event (30 min)

Your store knows when something is added. Other parts of your program want to know — a counter, a log, later a UI. The store shouldn't have to know about any of them.

```csharp
public sealed class Catalogue : IItemStore
{
    public event Action<IItem>? ItemAdded;     // the list of interested functions

    public void Add(IItem item)
    {
        // … all the Lab 02 rules first: null check, duplicate check, store it …

        ItemAdded?.Invoke(item);               // then tell whoever is listening
    }
}
```

Four things the contract checks, and they're the four things people get wrong:

1. **`event`, not a public `Action` field.** With the keyword, only the class itself can raise it or clear the list; outsiders can only `+=` and `-=`.
2. **Raised after the change, not before.** A subscriber that reads `Count` must see the new number.
3. **Nothing announced when `Add` throws.** A rejected duplicate is not an addition.
4. **`?.Invoke`, because an event with nobody attached is `null`.** This is the crash everyone meets once.

Subscribing and unsubscribing:

```csharp
store.ItemAdded += Handler;     // add this function to the list
store.ItemAdded -= Handler;     // remove exactly this one
```

`+=` twice means it's called twice — an event is a list, not a set. And `-=` needs *the same* function to remove it, which is why you keep the handler in a variable when you intend to unsubscribe.

## 4. Two subscribers (15 min)

Write two, doing genuinely different things — a counter and a "recently added" list, say — and prove to yourself that neither one knows about the other, and the store knows about neither.

Then implement `ILab06Wiring` in `src/Capstone.Core/Wiring/`. `Subscribe` is two lines:

```csharp
store.ItemAdded += handler;
return () => store.ItemAdded -= handler;   // the unsubscribe, as a function
```

That return value is the whole lab in one line: a function, produced by your code, handed to a caller, holding on to the variables it needs.

## 5. Push (5 min)

```
sq test
git add . && git commit -m "Lab 06: events" && git push
```

**No git (lab PC)?** `sh sq.sh test`, then `sh sq.sh upload`, then **Add file → Upload files** on your repository's page: drag in what it gathered, **Commit changes**. Step by step: [working on a lab PC](https://github.com/x86bogdan/fcpl-2026/blob/main/LAB-PC.md).

**On time** means green in your repository's **Actions** tab by **Friday 23:59** this week, whichever group you're in. After that it's late work, worth up to 8.

## Done means

See `ACCEPTANCE.md`. In short: Lab 06 at full marks, Labs 02–05 still green, CI green.

## Stretch

**Say what happened, not just that it happened.** Add a second event — removed, changed, whatever your domain has — and give one of them a richer payload than a single item (a small `record` describing what changed). Then ask yourself which of your subscribers still compile, and why that's the argument for `EventHandler<TArgs>`.

Or: unsubscribe *automatically*. Make `Subscribe` return an `IDisposable` instead of an `Action`, so callers can write `using var subscription = store.Subscribe(...)`. That's the pattern S8 will use for files.

---

**Next session, S7:** LINQ — the same functions-as-values idea, with fifty methods already written for you.
