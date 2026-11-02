# Lab 07 — Done means

## Automatic (Contract, 40%)

`sq test` shows:

```
 OK   Lab 02       36/36    contract ≈ 4.0 / 4
 OK   Lab 03       26/26    contract ≈ 4.0 / 4
 OK   Lab 04       41/41    contract ≈ 4.0 / 4
 OK   Lab 05       38/38    contract ≈ 4.0 / 4
 OK   Lab 06       28/28    contract ≈ 4.0 / 4
 OK   Lab 07       47/47    contract ≈ 4.0 / 4
```

…and the latest run in your repository's **Actions** tab is green. Only the CI run counts.

What the 47 checks cover:

| Group | Checks |
|---|---|
| A query, not a result (7) | one name per item · the display names, in store order · an empty store · **an item added after you asked for the query still shows up** · so does a removal · the same query can be read twice · asking doesn't change the store |
| Order (8) | alphabetical · case ignored · ties broken by the Id · every item still there and none invented · the sequence you were handed is not rearranged · nothing in, nothing out · a null sequence throws · and the same on your own items |
| A page of it (8) | the first page is the start of name order · the next page carries on · past the end is empty · a last page gives what's left · taking none gives none · a negative skip throws · a negative take throws · a null sequence throws |
| Your own extension (11) | longest name first · ties broken by the Id · asking for one · asking for more than there are · asking for none · a negative count throws · an empty sequence · a null sequence throws · **the host is a static class of your own, not the wiring** · **it declares an extension method on a sequence of items** · **calling that extension directly gives exactly what your `Longest` gives** |
| One line about the whole thing (7) | the count · the different initials, ignoring case · the longest name · the average name length · **an empty sequence answers instead of throwing** · a null sequence throws · and it agrees with `Longest` |
| These already exist (6) | each of the five queries calls into `System.Linq` (5) · at least five different verbs between them (1) |

The last group is read from your compiled code, not from the answers it produces. A correct implementation written with `foreach` fails those six and passes the other 41.

## By hand (you'll be asked to show these)

- [ ] Where your `Longest` extension lives, and what `this` does to the first parameter
- [ ] A query that hasn't run yet, and the line that makes it run
- [ ] Your `ThenBy`, and what happens without it
- [ ] The guard in `Summary` for an empty sequence, and which call forced it
- [ ] *(stretch)* a `GroupBy`, or a few extension methods of your own

## Example output (Pokédex reference)

```
──────────── SharpQuest ────────────
 OK   Lab 02       36/36    contract ≈ 4.0 / 4
 OK   Lab 03       26/26    contract ≈ 4.0 / 4
 OK   Lab 04       41/41    contract ≈ 4.0 / 4
 OK   Lab 05       38/38    contract ≈ 4.0 / 4
 OK   Lab 06       28/28    contract ≈ 4.0 / 4
 OK   Lab 07       47/47    contract ≈ 4.0 / 4
 OK   Your tests   62/62
────────────────────────────────────
```

```
dotnet run --project src/Capstone.App
…
The query written before the file was read now answers: Bulbasaur, Ember the Charmander, Charizard, Pikachu

dotnet run --project src/Capstone.App -- --queries
The first page, three at a time, by name:
  Bulbasaur
  Charizard
  Ember the Charmander
The three longest names:
  Ember the Charmander
  Bulbasaur
  Charizard
4 entries · 4 different initials · longest "Ember the Charmander" · 11.3 characters on average
All of them: Bulbasaur, Ember the Charmander, Charizard, Pikachu
```
