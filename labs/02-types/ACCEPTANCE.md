# Lab 02 — Done means

## Automatic (Contract, 40%)

`sq test` shows:

```
 OK   Lab 02       36/36    contract ≈ 4.0 / 4
```

…and the latest run in your repository's **Actions** tab is green. Only the CI run counts.

What the 36 checks cover:

| Group | Checks |
|---|---|
| Model | a wiring class exists · 3+ types of your own · an enum · an interface of your own · a record · items are your type |
| Samples | non-empty Id and DisplayName · same seed → same Id · 100 seeds → 100 distinct Ids |
| Store | starts empty · add then find · unknown Id → null · case-sensitive Ids · null → `ArgumentNullException` · duplicate → `ArgumentException`, nothing changes · insertion order · `All()` is a snapshot · remove → true/false · re-add after remove · stores don't share items |
| Mapper | round trip keeps Id and DisplayName · output is a JSON object · 7 kinds of bad input → null without throwing · `{}` → null · blank Id → null |

The build itself enforces **zero nullable warnings**, since a nullable warning is an error. If it builds, that requirement is met.

## By hand (you'll be asked to show these)

- [ ] `dotnet run --project src/Capstone.App` loads items from a JSON file in your repository and prints them
- [ ] At least one invalid entry in that file gets skipped, not crashed on
- [ ] You can point to one place you used `required` or `init`, and say why
- [ ] You can point to one nullable property, and say what `null` *means* for it in your domain

## Example output (Pokédex reference)

```
skipped invalid entry: { "id": "pkmn-999", "species": "", "primaryType": "Normal" }
4 Pokémon loaded:
  #pkmn-001 Bulbasaur · Grass · Lv 5
  #pkmn-004 Ember the Charmander · Fire · Lv 7
  #pkmn-006 Charizard · Fire/Dragon · Lv 36
  #pkmn-025 Pikachu · Electric · Lv 12
Trainer Ash · last caught Pikachu
```
