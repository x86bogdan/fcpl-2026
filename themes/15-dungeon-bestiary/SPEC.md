# Dungeon Bestiary — full spec

## The pitch

A game master's bestiary. It holds every creature in your world, where it lives, what it drops, and builds encounters that fit a party. By January it is a desktop app, a web API and a database — the same creatures underneath all three.

## The domain

At least these, named however you like:

- **Creature** — the item. Id, name, size, challenge rating, hit points, armour class, stats, loot table
- **Size** and **Damage type** — `enum`s (Tiny…Gargantuan; Fire, Cold, Poison…)
- **Stats** — a `record`: Strength, Dexterity, Constitution, Intelligence, Wisdom, Charisma
- **Loot entry** — a `record`: item name, gold value, chance to drop
- **Dungeon** — the parent: name, recommended level, a line of lore
- **Your feature** — pick **one** and build it across the semester:
  - *Encounter builder* — a party's level and size → a set of creatures within a difficulty budget
  - *Loot* — rolls on defeat, expected gold per creature, the best creature to farm
  - *Weaknesses* — resistances and vulnerabilities, and "what should I hit this with?"

**Data:** at least **30 creatures** in at least **five dungeons**, every kind represented, challenge ratings spread from easy to deadly.

## What each session adds

| S | Required feature | In this theme |
|---|---|---|
| 2 | 3+ types, an enum, an interface, zero nullable warnings | `Creature`, `Dungeon`, `Stats` and `LootEntry` records, `Size`. A lair action is nullable — most creatures don't have one. Optionally fetch one monster from the SRD API with `HttpClient` |
| 3 | 5+ tests over your own domain logic | Your feature: an encounter stays within budget, a stat modifier ((stat − 10) / 2, rounded down — careful with negatives), a loot table with no entries |
| 4 | Abstract base + 2 kinds, a polymorphic loop, a switch expression | `Beast` / `Undead` / `Boss` (a boss has phases), each describing itself differently. The switch labels by challenge — or by what beats it: undead → "radiant", fire elemental → "cold" |
| 5 | Custom collection with indexer + iterator; a generic type | The bestiary: `bestiary[0]`, `bestiary["cr-ghoul"]`, `foreach`. Your generic type holds *recently viewed* |
| 6 | An event with 2 subscribers; a function as a parameter | "Creature added" — heard by the dungeon's danger rating and by a log. *Defeated* (and its loot roll) is the stretch |
| 7 | 5 queries answering non-trivial questions | e.g. the deadliest creature per dungeon · expected gold per creature · creatures vulnerable to fire · average hit points per size · dungeons fit for a level-3 party |
| 8 | Async save/load that survives a restart | The bestiary to JSON, each kind back as its own kind, **stats and loot tables intact** |
| 9 | A window bound to the model | The bestiary with search, a stat block in the details panel |
| 10 | 4 endpoints + DI + no secrets in source | `/items` — list, one, add, delete. Your players' app can read what they've met |
| 11 | A migration + one one-to-many | **Dungeon → Creatures.** A creature lives in one dungeon or wanders; deleting a dungeon keeps its creatures |
| 12 | An attribute-driven feature + your own runner | `[Show("AC")]` decides what the stat block lists. Your `[Check]`s test your feature |

## At the end, you can show

1. The window: search "dragon", select one, see its stat block and dungeon
2. Your feature working — an encounter for a given party, a loot roll, or a weakness answer
3. Your five questions, answered on your data
4. The app closed and reopened, the bestiary intact
5. The API answering in a browser, and refusing a POST without the key

## Stretch ideas

A defeated event that rolls loot · a seeded random encounter you can replay · creature art in the window · a simple initiative tracker.

## Watch out for

- **Random must be testable.** Pass a `Random` (with a seed) into anything that rolls; the same seed gives the same encounter, every test run
- **Use SRD content only** if you pull from the API — it's the part that's free to use. Your own invented creatures are always fine
- **Don't build combat.** You're describing creatures and building encounters, not running turns
