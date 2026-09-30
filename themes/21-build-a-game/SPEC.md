# Build a Game — full spec

## The pitch

A game you design, built in plain C#, that satisfies the same eleven requirements as every other theme. The game is yours; the requirements are everyone's.

## Does your game qualify?

Every theme must pass four questions. Games fail them more often than anything else, so check yours now:

1. **20+ things with 4+ varied attributes?** Enemies with health, damage, speed and loot; cards with cost, attack, rarity and effect.
2. **Does something happen?** An item is picked up, a level gained, a wave cleared.
3. **A parent with children?** A level has enemies, a player has an inventory, a deck has cards.
4. **Something to look at besides the game?** An inventory, a roster, a bestiary, a deck list.

| Passes | Fails |
|---|---|
| Roguelike (items, enemies, loot tables) | Pong |
| Deck-builder / auto-battler (cards, stats, synergies) | Flappy Bird |
| RPG with inventory and quests | Snake |
| Tower defence (unit and tower types, waves) | Breakout |
| Management sim (units, resources, upgrades) | Any pure-reflex arcade game |

The failures fail question 1 — nothing for collections and LINQ to work on — and question 3.

## The rule that makes it work: the game lives in `Capstone.Core`

Your game's **rules and content** — the classes, the combat maths, the inventory — go in `Capstone.Core`, a plain library the tests can reach. The part that draws and reads the keyboard is a separate project: a console app, or an Avalonia window. That split is what lets the contract tests grade you, and it's the same split S9 (MVVM) teaches.

**Not allowed: Unity, Godot, or any engine that owns your project.** S9's MVVM, S10's API and S11's EF Core don't map onto them, and you would spend the semester fighting the engine instead of learning C#. MonoGame or a console game is fine, as long as the rules live in `Capstone.Core`.

## The domain

Yours to design, but it must have:

- **Content** — the item. At least 20 things with Id, name and at least four attributes
- **An `enum`** — rarity, element, damage type, tier
- **A `record`** — stats, a cost, a position
- **A parent** — Level, Player, Deck, Wave, Biome
- **Your feature** — the heart of your game, in `Capstone.Core`: combat resolution, a loot system, a wave generator, a crafting tree, an economy. Name it at CP1

## What each session adds

| S | Required feature | In a game |
|---|---|---|
| 2 | 3+ types, an enum, an interface, zero nullable warnings | Content, parent, a record, an enum. Something nullable that means something — an equipped weapon slot that's empty |
| 3 | 5+ tests over your own domain logic | Your feature's rules: damage after armour, a loot roll with a fixed seed, a level-up at exactly the threshold |
| 4 | Abstract base + 2 kinds, a polymorphic loop, a switch expression | Kinds of content (`Melee` / `Ranged` / `Boss`; `Weapon` / `Armour` / `Potion`), each describing itself. A switch for effectiveness, rarity colour or loot tier |
| 5 | Custom collection with indexer + iterator; a generic type | The inventory or bestiary: an indexer, an iterator. Your generic type holds *recently picked up* |
| 6 | An event with 2 subscribers; a function as a parameter | "Content added" — heard by two parts of the game. Level-up, item acquired or wave cleared is the stretch |
| 7 | 5 queries answering non-trivial questions | Balance questions: damage per cost, strongest per tier, drop rates, enemies a level-5 player can beat, average wave difficulty |
| 8 | Async save/load that survives a restart | **A save game.** Each kind back as its own kind, stats intact |
| 9 | A window bound to the model | An inventory, roster, deck or bestiary screen — **not the game viewport** |
| 10 | 4 endpoints + DI + no secrets in source | `/items` — list, one, add, delete: a content catalogue, or a leaderboard |
| 11 | A migration + one one-to-many | Your parent → your content: Level → Enemies, Player → Inventory. One parent per item at a time |
| 12 | An attribute-driven feature + your own runner | `[Show("Damage")]` decides what the inventory's details list. Your `[Check]`s test your game's rules |

## At the end, you can show

1. The game running — a minute of play
2. The inventory/roster window: search, select, details
3. Your feature working, and your five balance questions answered
4. A save, a quit, a load — and you're where you were
5. The API answering in a browser, and refusing a POST without the key

## On tutorials

Following one is explicitly permitted: *"Following a tutorial is fine. You will be asked to change what it produced."* The verification will ask you to change a rule, live, and explain why the code works. If you can do that, the tutorial helped you learn. If you can't, it did the lab instead of you — and the Change and Comprehension halves of the grade will show it.

## Watch out for

- **Scope.** A small game with rich content beats a big game with none. You have one semester and ten other requirements
- **Random must be testable.** Pass a seeded `Random` into anything that rolls
- **Decks → cards** is many-to-many if a card can be in several decks. One deck per card, or pick a different parent
