# Lab 03 — Done means

## Automatic (Contract, 40%)

`sq test` shows:

```
  Lab 03: broken versions caught 8/8
 OK   Lab 03       26/26    contract ≈ 4.0 / 4
 OK   Your tests   ...
```

…and the latest run in your repository's **Actions** tab is green. Only the CI run counts.

What the 26 checks cover:

| Group | Checks |
|---|---|
| Search (your wiring) | wiring exists · empty and blank query → everything, in store order · a shouted word finds its item (×3) · accents in the query are ignored · word order doesn't matter · nonsense finds nothing · every word must match · searching doesn't change the store |
| Your tests (read from the compiled project) | 5+ tests that use your own types · one of them checks that bad input is rejected · a `[Theory]` with several cases · 4+ SearchMatcher tests · no peeking at the grader |
| Mutation | all your tests pass · your SearchMatcher tests pass on the correct matcher · each of the 8 broken matchers is caught (×8) |

## By hand (you'll be asked to show these)

- [ ] The comment in `SmokeTests.cs`: one Lab 02 test explained in your own words
- [ ] Every test name reads as a sentence about behaviour
- [ ] You can point at the test that catches broken matcher #5, and say why it catches it
- [ ] *(stretch)* the line you broke, and the test that now catches it

## Example output (Pokédex reference)

```
  Lab 03: running your tests against 8 broken versions ...
  Lab 03: broken versions caught 8/8

──────────── SharpQuest ────────────
 OK   Lab 02       36/36    contract ≈ 4.0 / 4
 OK   Lab 03       26/26    contract ≈ 4.0 / 4
 OK   Your tests   30/30
────────────────────────────────────
```

With only three rules tested (R1, R2, R4), the same project shows `caught 3/8` and names the five rules nobody checked.
