# Lab 04 — Done means

## Automatic (Contract, 40%)

`sq test` shows:

```
 OK   Lab 02       36/36    contract ≈ 4.0 / 4
 OK   Lab 03       26/26    contract ≈ 4.0 / 4
 OK   Lab 04       41/41    contract ≈ 4.0 / 4
```

…and the latest run in your repository's **Actions** tab is green. Only the CI run counts.

**Lab 02 and Lab 03 have to stay green.** Today's refactor is graded on not breaking them as much as on the new code.

What the 41 checks cover:

| Group | Checks |
|---|---|
| The family (13) | wiring exists · the base is an abstract class · the base is an `IItem` · two or more kinds, all different · every kind is concrete and derives from the base · the base declares an abstract or virtual member taking no parameters and returning a string · every kind overrides it · `CreateOfKind` returns that kind (×2) · Ids stable per kind and seed, different otherwise · every kind goes into your store · the hierarchy is your own code · items still have an Id and a DisplayName · the polymorphic member says something |
| Polymorphism (8) | `Describe` says something · the two kinds describe themselves differently once the Id and name are removed · `Describe` returns exactly what the override returns · the same item twice gives the same answer · an empty store describes nothing · `DescribeAll` covers every item, in store order · describing doesn't change the store · a store of one kind works |
| Pattern matching (7) | every kind gets a label · the label depends on the kind, not the item · different kinds get different labels · something that isn't one of your kinds gets a fallback label · `null` throws `ArgumentNullException` · classifying changes nothing · `Classify` and `Describe` don't return the same text |
| `out` and `params` (13) | a stored item is found by name · a missing name gives false and null · an empty store finds nothing · a null or blank name is not found, without throwing (×3) · finding doesn't change the store · `AddAll()` with nothing adds nothing · `AddAll` adds every item in order · `AddAll` takes a single item · a duplicate in the batch throws and keeps what came before · a null item in the batch throws |

## By hand (you'll be asked to show these)

- [ ] The loop that describes everything — and that it contains no `if` and no type test
- [ ] Your switch expression, including the `_` arm and the `null` arm
- [ ] What you changed to keep Lab 02 green, and what you deliberately didn't touch
- [ ] *(stretch)* the third kind, and the loop that didn't change

## Example output (Pokédex reference)

The reference makes `Pokemon` abstract, with `WildPokemon` and `CaughtPokemon` deriving from it. The mapper picks the kind from the JSON — one extra branch. Nothing else about Lab 02 or Lab 03 changed.

```
  Lab 03: broken versions caught 8/8

──────────── SharpQuest ────────────
 OK   Lab 02       36/36    contract ≈ 4.0 / 4
 OK   Lab 03       26/26    contract ≈ 4.0 / 4
 OK   Lab 04       41/41    contract ≈ 4.0 / 4
 OK   Your tests   36/36
────────────────────────────────────
```

```
dotnet run --project src/Capstone.App -- --kinds
  [wild] #pkmn-001 Bulbasaur · Grass/Poison · Lv 5 · wild, in the tall grass
  [caught] #pkmn-025 Pikachu · Electric · Lv 12 · caught by Ash
```
