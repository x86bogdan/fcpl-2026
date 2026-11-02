# Lab 07 — LINQ

**Today:** last week you wrote code that takes a function and runs it over a collection. Today you find out that fifty of those already exist, that they click together, and that most of them don't do a thing until you ask for the answer.

**Required feature (matrix row 7):** five queries answering real questions, at least one of them your own extension method

> **In your project:** open your theme's spec ([the 21 themes](https://github.com/x86bogdan/fcpl-2026/blob/main/themes/README.md)) → *What each session adds* → row **7** (your own idea: the required feature above, said for your app). Before you write code, put three lines in your README: which of your classes change today, what they do in your app, and where you'll see it working.

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

Lab 07 shows as *not started*; Labs 02–06 should still be green.

**Looking for the syntax?** The [C# phrasebook](https://x86bogdan.github.io/fcpl-2026/cheatsheet/#lab07) has today's C# next to Java and C++, with the errors you're likely to meet.

## 1. You wrote three of these last week (10 min)

`Where`, `Map` and `ForEach` from Lab 06 are `Where`, `Select` and — near enough — `ForEach` in `System.Linq`. Same idea, same shape, already written:

```csharp
using System.Linq;                        // usually already there via ImplicitUsings

var fast = store.Where(p => p.Speed > 90);          // your Lab 06 Where, but theirs
var names = store.Select(p => p.DisplayName);       // your Map
var sorted = store.OrderBy(p => p.DisplayName);     // one you didn't write
```

Each one is a static method whose first parameter is the sequence. That's the whole mechanism, and you'll write one yourself in step 4.

There is a second spelling, *query syntax*, which compiles to exactly the same calls:

```csharp
var sorted = from p in store orderby p.DisplayName select p;
```

Use whichever reads better. The tests can't tell the difference, because there isn't one.

## 2. A query is not a result (15 min)

This is the part that surprises people.

```csharp
var names = store.Select(item => item.DisplayName);   // nothing has happened yet
store.Add(somethingNew);                              // …
Console.WriteLine(names.Count());                     // now it runs — and counts the new one
```

`Select` hands back a *question*, not an answer. It runs when something enumerates it: a `foreach`, a `Count()`, a `ToList()`. That's **deferred execution**, and it's why `store.Where(…).Select(…).OrderBy(…)` doesn't build three intermediate lists.

`ToList()` is where you stop asking and take the answer:

```csharp
var page = store.OrderBy(i => i.DisplayName).Skip(10).Take(10).ToList();   // a finished list
```

**Your first query, `Names(store)`, has to stay lazy.** Query the store itself — the collection you made it in Lab 05 — not the snapshot `All()` hands you, which froze the answer the moment you asked. One test adds an item after calling `Names` and expects to see it.

*Your store isn't a collection yet, because you missed or didn't finish Lab 05?* Copy `catchup/Lab05/CatchUpCollection.cs` into `src/Capstone.Core/CatchUp/` and query `store.AsCollection()` instead: it reads your store when the query runs, so `Names` stays lazy. It unblocks this lab; Lab 05 itself still needs your own collection.

## 3. Order, and a page of it (20 min)

Two rules used all over today, worth reading once:

| Rule | Means |
|---|---|
| **Name order** | by `DisplayName`, ignoring case (`StringComparer.OrdinalIgnoreCase`), ties broken by `Id` (`StringComparer.Ordinal`) |
| **Length order** | longest `DisplayName` first, ties broken by `Id` |

```csharp
items.OrderBy(i => i.DisplayName, StringComparer.OrdinalIgnoreCase)
     .ThenBy(i => i.Id, StringComparer.Ordinal)
```

`ThenBy` is the tie-break, and `OrderBy` alone leaves ties in the order they arrived — fine until the input order changes and your output quietly changes with it.

Then `Page(items, skip, take)`: name order, `Skip`, `Take`. Two things to notice. Skipping *before* ordering answers a different question. And `Skip(-1)` doesn't complain — it just does nothing — so a negative page is yours to reject: `ArgumentOutOfRangeException`.

## 4. Write one of your own (15 min)

`Longest(items, n)` — the n items with the longest display names, longest first — is written as an **extension method**, in a static class of your own in `Capstone.Core`:

```csharp
public static class ItemQueries
{
    public static IEnumerable<IItem> Longest(this IEnumerable<IItem> items, int n)
    {
        ArgumentNullException.ThrowIfNull(items);
        ArgumentOutOfRangeException.ThrowIfNegative(n);

        return items.OrderByDescending(i => i.DisplayName.Length)
                    .ThenBy(i => i.Id, StringComparer.Ordinal)
                    .Take(n);
    }
}
```

`this` on the first parameter is the entire trick: the compiler lets you write `store.Longest(3)` instead of `ItemQueries.Longest(store, 3)`. Which is exactly what `Where` and `Select` are — extension methods on `IEnumerable<T>`, sitting in a static class called `Enumerable`.

Point `ExtensionHost` at your class. The tests call your extension directly and expect the same answer your `Longest` gives, so the wiring method should be one line.

## 5. The whole thing in one line (10 min)

`Summary(items)` returns a `StoreStats`: how many, how many different initials (ignoring case), the longest name, the average name length. Four aggregates, no loops.

One trap, and it's a good one: **`Average()` on an empty sequence throws.** So does `Max()`. An empty sequence needs an answer of its own — `new StoreStats(0, 0, null, 0)` — which you'll write a guard for whether you like it or not.

## 6. Push (5 min)

```
sq test
git add . && git commit -m "Lab 07: LINQ" && git push
```

**No git (lab PC)?** `sh sq.sh test`, then `sh sq.sh upload`, then **Add file → Upload files** on your repository's page: drag in what it gathered, **Commit changes**. Step by step: [working on a lab PC](https://github.com/x86bogdan/fcpl-2026/blob/main/LAB-PC.md).

**On time** means green in your repository's **Actions** tab by **Friday 23:59** this week, whichever group you're in. After that it's late work, worth up to 8.

## Done means

See `ACCEPTANCE.md`. In short: Lab 07 at full marks, Labs 02–06 still green, CI green.

**One check that isn't about answers.** The grader reads your compiled code and makes sure each of the five queries really goes through `System.Linq`, and that at least five different verbs appear between them. A hand-written `foreach` that produces the right answer fails this lab on purpose: you proved you could write the loop last week. `ToList` and `ToArray` are conversions, not questions, so they don't count towards the five.

## Stretch

**Ask a question that needs two sequences.** `GroupBy` gives you one entry per key holding everything that shares it — how many of each type, the count per owner, the collection by initial. `Join` matches two sequences on a key. Both look strange the first time and are ordinary the third. Then look at what `GroupBy` actually returns, and notice it's a sequence of sequences.

Or: **a small vocabulary of your own.** Two or three more extension methods that make your domain read well, on a type you don't own:

```csharp
public static string OneLine(this IEnumerable<IItem> items) => string.Join(", ", items.Select(i => i.DisplayName));
public static bool IsEmpty(this IEnumerable<IItem> items) => !items.Any();
```

`store.OneLine()` and `store.IsEmpty()`. (C# 14, on .NET 10, can also declare extension *properties*, so the brackets go: `store.OneLine`. This course builds with C# 13 on every machine, because the lab PCs have .NET 9, so that version won't compile here. It's worth reading about.)

---

**Next session, S8:** persistence and async — your capstone starts saving what it holds, and `await` finally gets explained rather than copied.
