# Lab 05 — Done means

## Automatic (Contract, 40%)

`sq test` shows:

```
 OK   Lab 02       36/36    contract ≈ 4.0 / 4
 OK   Lab 03       26/26    contract ≈ 4.0 / 4
 OK   Lab 04       41/41    contract ≈ 4.0 / 4
 OK   Lab 05       39/39    contract ≈ 4.0 / 4
```

…and the latest run in your repository's **Actions** tab is green. Only the CI run counts.

What the 39 checks cover:

| Group | Checks |
|---|---|
| The collection (14) | still an `IItemStore` · implements `IEnumerable<IItem>` · **something in it is written with `yield`** · an indexer by position and one by id · position follows insertion order (×3) · a position off either end throws `ArgumentOutOfRangeException` (×2) · the id indexer finds what `FindById` finds, and gives null otherwise · `foreach` over the store sees everything in order · an empty store yields nothing · enumerating twice gives the same answer · an item added afterwards shows up next time · **it's your own work, not the catch-up kit's**: neither your store nor your wiring may reach into `Capstone.Core.CatchUp` |
| The lazy query (8) | empty text matches everything · a word from a display name finds its item · case is ignored · nonsense matches nothing · store order is kept · **nothing happens until you enumerate** · the store is unchanged · it can be enumerated more than once |
| The generic type (17) | it's yours, and open (`typeof(Thing<>)`) · its type parameter is actually used · newest first · bounded by capacity · a repeat moves to the front · the same value repeatedly is still one value · a repeat counts as newest · nothing pushed, nothing held · fewer values than capacity · capacity below 1 throws (×2) · works for strings, for numbers and for your own items · two uses don't share state |

## By hand (you'll be asked to show these)

- [ ] Your `GetEnumerator`, and what `yield return` does that `return _list.GetEnumerator()` doesn't
- [ ] The two indexers, and why the id one returns `null` while the position one throws
- [ ] Your generic type, and one line in it that would break if you replaced `T` with `object`
- [ ] *(stretch)* `store.Where(...)` working on your store, or your own extension method

## Example output (Pokédex reference)

```
──────────── SharpQuest ────────────
 OK   Lab 02       36/36    contract ≈ 4.0 / 4
 OK   Lab 03       26/26    contract ≈ 4.0 / 4
 OK   Lab 04       41/41    contract ≈ 4.0 / 4
 OK   Lab 05       39/39    contract ≈ 4.0 / 4
 OK   Your tests   48/48
────────────────────────────────────
```

```
dotnet run --project src/Capstone.App -- --recent
Last 3 entries looked at, newest first:
  Pikachu
  Squirtle
  Charmander
```
