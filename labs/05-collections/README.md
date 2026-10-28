# Lab 05 — Collections & generics

**Today:** your store stops being a list with rules and becomes a collection. `foreach` works on it, `[0]` and `["id"]` work on it, and it has an iterator you wrote. Then one generic type of your own, so `T` stops being something only other people write.

**Required feature (matrix row 5):** a custom collection with an indexer and an iterator; one generic type or method

> **In your project:** open your theme's spec ([the 21 themes](https://github.com/x86bogdan/fcpl-2026/blob/main/themes/README.md)) → *What each session adds* → row **5** (your own idea: the required feature above, said for your app). Before you write code, put three lines in your README: which of your classes change today, what they do in your app, and where you'll see it working.

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

Lab 05 shows as *not started*; Labs 02–04 should still be green.

**Missed a lab?** `reference/` has the earlier solutions, and the recovery rules are in the course contract.

**Looking for the syntax?** The [C# phrasebook](https://x86bogdan.github.io/fcpl-2026/cheatsheet/#lab05) has today's C# next to Java and C++, with the errors you're likely to meet.

## 1. `foreach` over your store (20 min)

Right now the only way to walk your items is `store.All()`. That copies the whole thing first. Implement `IEnumerable<IItem>` on your store class and write the enumerator yourself:

```csharp
public sealed class Catalogue : IItemStore, IEnumerable<IItem>
{
    private readonly List<IItem> _ordered = [];

    public IEnumerator<IItem> GetEnumerator()
    {
        foreach (var item in _ordered)
            yield return item;          // one at a time, as the caller asks
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();   // the old, untyped one
}
```

`yield return` is the point of this step. `return _ordered.GetEnumerator();` compiles and works — and the grader will tell you it isn't an iterator you wrote.

**What `yield` actually does:** the compiler turns that method into a small state machine. Nothing runs until somebody enumerates, and it runs again, from the top, for each new `foreach`. That's why an iterator can be infinite, and why it sees changes made after it was created.

## 2. Index into it (15 min)

Two indexers, because there are two natural questions:

```csharp
public IItem this[int index] => _ordered[index];              // "the third one"
public IItem? this[string id] => _byId.GetValueOrDefault(id); // "the one called X"
```

Position counts from the first item added, and an index off the end throws `ArgumentOutOfRangeException` — the list gives you that for free. The id indexer answers `null`, exactly like `FindById`.

## 3. A lazy query (15 min)

One more iterator, this time with a filter:

```csharp
public IEnumerable<IItem> Matching(string text)
{
    foreach (var item in _ordered)
        if (item.DisplayName.Contains(text, StringComparison.OrdinalIgnoreCase))
            yield return item;
}
```

The contract checks that this is lazy: ask for the matches, add an item, *then* enumerate — and the new item is in there. Build a `List` inside and return it, and it won't be.

## 4. Your own generic type (30 min)

Write a type with a type parameter, doing a job that has nothing to do with your domain. The contract asks for one specific job, so it can be tested:

**A "recently used" bag.** Keeps the last *N* distinct values, newest first.

```csharp
public sealed class RecentlyUsed<T>
{
    public RecentlyUsed(int capacity) { /* capacity < 1 → ArgumentOutOfRangeException */ }
    public void Push(T value) { /* … */ }
    public IEnumerable<T> Items() { /* newest first */ }
}
```

The rules, in full:

- **Newest first.** The last value pushed comes out first.
- **Bounded.** Never more than `capacity` values; the oldest drops out.
- **No duplicates.** Pushing something already in there moves it to the front instead of taking a second place. Compare with `EqualityComparer<T>.Default`, not `==` — that's what makes it work for *any* `T`.
- A capacity below 1 throws `ArgumentOutOfRangeException`.

The tests use it with strings, with numbers and with your own items, so `object` in a trench coat won't pass.

## 5. Wire it up and push (10 min)

Implement `ILab05Wiring` in `src/Capstone.Core/Wiring/`, then:

```
sq test
git add . && git commit -m "Lab 05: collections" && git push
```

**No git (lab PC)?** `sh sq.sh test`, then `sh sq.sh upload`, then **Add file → Upload files** on your repository's page: drag in what it gathered, **Commit changes**. Step by step: [working on a lab PC](https://github.com/x86bogdan/fcpl-2026/blob/main/LAB-PC.md).

**On time** means green in your repository's **Actions** tab by **Friday 23:59** this week, whichever group you're in. After that it's late work, worth up to 8.

## Done means

See `ACCEPTANCE.md`. In short: Lab 05 at full marks, Labs 02–04 still green, and CI green on GitHub.

## Stretch

**Make your collection speak `LINQ`.** Because it's an `IEnumerable<IItem>`, `store.Where(...)`, `store.OrderBy(...)` and `store.Take(3)` already work — try them. Then write one extension method of your own on `IEnumerable<IItem>` (say `MostRecent(int n)`), and notice that you've just written the same shape LINQ uses.

Or: give `RecentlyUsed<T>` a constructor taking an `IEqualityComparer<T>`, and use it to make a case-insensitive version for strings.

---

**Next session, S6:** functions as values and events — one publisher, two subscribers, and *checkpoint 2* for the project route.
