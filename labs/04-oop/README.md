# Lab 04 — OOP & pattern matching

**Today:** your model grows a family. One abstract base, two kinds that behave differently, one loop that doesn't care which it's holding, and one switch expression that does. Last week's tests come along as a safety net: if today's refactor breaks Lab 02 or Lab 03, you'll know within seconds.

**Required feature (matrix row 4):** an abstract base with 2 derived types, one polymorphic loop, one switch expression

> **In your project:** open your theme's spec ([the 21 themes](https://github.com/x86bogdan/fcpl-2026/blob/main/themes/README.md)) → *What each session adds* → row **4** (your own idea: the required feature above, said for your app). Before you write code, put three lines in your README: which of your classes change today, what they do in your app, and where you'll see it working.

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

Lab 04 shows as *not started*. Lab 02 and Lab 03 should still be green — that's your before picture, and you'll check it again after every step today.

**Missed a lab?** `reference/` has the earlier solutions, and the recovery rules are in the course contract.

## 1. Find the family in your model (15 min)

Look at your types from Lab 02. Somewhere in there are things that are *almost* the same: an order that's been paid and one that hasn't, a book on loan and one on the shelf, a wild Pokémon and a caught one. Same data, different behaviour.

Pick one such split and write down, in one line each:

- what **all of them** have (that's the base)
- what **each kind** does differently (that's the override)

If nothing in your model splits naturally, add a small family beside it — two kinds is enough.

## 2. Make the base abstract (25 min)

In `src/Capstone.Core/Model/`:

```csharp
public abstract class Entry : IItem          // your noun, not "Entry"
{
    public required string Id { get; init; }
    public required string DisplayName { get; init; }

    /// Each kind answers this differently.
    public abstract string Describe();

    /// The part every kind shares.
    protected string Card() => $"#{Id} {DisplayName}";
}

public sealed class OnLoan : Entry
{
    public required string Borrower { get; init; }
    public override string Describe() => $"{Card()} · out with {Borrower}";
}
```

Two rules that make this survivable:

- **Don't rename or delete what Lab 02 tests.** The store, the mapper and the interfaces stay as they are.
- **Run `sq test` after every few minutes.** Lab 02 going red is the refactor telling you something, and the failing test name says what.

The one place that must change: wherever you used to write `new Thing { … }`, you now pick a kind. Your mapper is usually the interesting one — the JSON decides which kind to build.

## 3. One loop, one switch expression (30 min)

Two different jobs, and the difference is the lesson.

**The loop** asks each item to speak for itself:

```csharp
foreach (var item in store.All())
    lines.Add(item.Describe());        // the override decides. No 'if' in sight.
```

**The switch expression** is the outside world sorting items into boxes:

```csharp
public string Classify(IItem item) => item switch
{
    null => throw new ArgumentNullException(nameof(item)),
    OnLoan => "out",
    OnShelf => "available",
    _ => "something else",             // for anything that isn't one of your kinds
};
```

Adding behaviour *to* a type is the override. Asking *about* a type from outside is the switch. Both today, so you can feel which is which.

## 4. `out` and `params` (15 min)

Two keywords worth knowing, in the two places they're actually used:

```csharp
bool TryFindByName(IItemStore store, string name, out IItem? found);  // answers false, never throws
int AddAll(IItemStore store, params IItem[] items);                   // AddAll(store), AddAll(store, a, b, c)
```

A `Try` method's deal: return `true` and set the `out` parameter, or return `false` and set it to `null`. A missing or blank name is simply *not found*, not an exception.

`AddAll` adds in order and returns how many it added. It doesn't second-guess your store: a null item or a duplicate Id still throws, and whatever was added before the bad one stays added.

## 5. Wire it up and push (10 min)

Implement `ILab04Wiring` in `src/Capstone.Core/Wiring/` — it tells the tests about your base type, your kinds, and the four methods above. Then:

```
sq test
git add . && git commit -m "Lab 04: oop" && git push
```

**No git (lab PC)?** `sh sq.sh test`, then `sh sq.sh upload`, then **Add file → Upload files** on your repository's page: drag in what it gathered, **Commit changes**. Step by step: [working on a lab PC](https://github.com/x86bogdan/fcpl-2026/blob/main/LAB-PC.md).

**On time** means green in your repository's **Actions** tab by **Friday 23:59** this week, whichever group you're in. After that it's late work, worth up to 8.

## Done means

See `ACCEPTANCE.md`. In short: Lab 04 at full marks, **Lab 02 and Lab 03 still green**, and CI green on GitHub.

## Stretch

**Add a third kind without touching the loop.** Write it, override the member, add its arm to the switch expression, and check that the loop that prints everything needed no change at all. Then show me the diff: a feature added, and one `foreach` that didn't notice.

Pattern matching goes further than types, and a switch expression can test properties too:

```csharp
OnLoan { DaysOverdue: > 30 } => "very overdue",
OnLoan => "out",
```

---

**Next session, S5:** collections and generics — your store grows an indexer and an iterator.
