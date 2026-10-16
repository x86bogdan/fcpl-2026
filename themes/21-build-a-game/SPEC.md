# Build a Game — full spec

## The pitch

A game you design, whose rules are written in plain C# and satisfy the same requirements as every other theme. The game is yours; the requirements are everyone's.

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
| Metroidvania (abilities, the gates they open, rooms, enemies) | Any pure-reflex arcade game |
| Management sim (units, resources, upgrades) | |

The failures fail question 1 — nothing for collections and LINQ to work on — and question 3.

## Two ways to build it

| | **Plain C#** | **In Unity: the game track** |
|---|---|---|
| Template | The course template, like every other theme | Its own: **[SharpQuest-Game-2026](https://github.com/x86bogdan/SharpQuest-Game-2026)** |
| Your rules live in | `src/Capstone.Core` | `Assets/Game/Core/` in your Unity project |
| What draws the game | A console app, MonoGame, or an Avalonia window | Unity: your scenes and MonoBehaviours |
| Route | Guided or project | **Project route only**: you're seen at the four checkpoints |
| Graded rows | All eleven (sessions 2–12) | Nine: sessions 2–9 and 12. The web API and the database (sessions 10–11) are optional |
| On top | The stretch tasks, as everyone | **Extra points for the final demo of the game itself**, which is what makes up for the two optional rows |
| Where you work | Lab PC or laptop | **Your laptop**: Unity can't be installed on the lab PCs |

Another engine (Godot with C#, for example) goes the game-track way too: say so at checkpoint 1 and we agree the folders.

## The rule that makes it work: the rules live on their own

Your game's **rules and content** — the classes, the combat maths, the inventory, the waves — live in a plain C# library that never touches the screen. The part that draws and reads the keyboard is separate. That split is what lets the course's tests run your rules, and it's the same split session 9 (MVVM) teaches.

- **Plain C#:** the rules are `Capstone.Core`; the front end is a console app, MonoGame or a window.
- **Unity:** the rules are the folder `Assets/Game/Core/`, an assembly with **No Engine References**: Unity itself won't let code there use `UnityEngine`. Your MonoBehaviours in `Assets/Game/Unity/` call it and show what it decided. Unity recommends this split anyway (it's what assembly definitions are for); here it's also how you're graded.

## The game track: using the template

### Once, at the start

1. Open **[SharpQuest-Game-2026](https://github.com/x86bogdan/SharpQuest-Game-2026)** → **Use this template** → create a **private** repository → *Settings → Collaborators* → add your instructor. Send the new repository's link in place of your session-1 one.
2. Clone it to your laptop (GitHub Desktop is fine).
3. Install **Unity Hub**, sign in (that activates the free Personal licence), and install the **exact Unity version named in the template's README**. Everyone on the track uses the same one.
4. Unity Hub → *New project* → that version → a 2D or 3D template. *Location*: the folder you cloned into. *Project name*: **`Game`**. Create it, then close Unity once it has opened.
5. In a terminal at the top of the repository: **`sq init`** (`sh sq.sh init` on Linux or macOS). It makes the folders below, the three assembly definitions, and adds the JSON package to your Unity project.
6. Open the project in Unity again and let it import. Commit and push: everything, including the `.meta` files.

Already started a Unity project? Copy the template's files into your project's repository (not over your project), then `sq init <your Unity project's folder>`.

### Where things go

| Folder | What |
|---|---|
| `Game/Assets/Game/Core/` | **Your rules.** Plain C#, no `UnityEngine`. Everything graded is here |
| `Game/Assets/Game/Unity/` | Your MonoBehaviours: they call Core, draw, play sounds, read input |
| `Game/Assets/Game/Tests/Core/` | Your tests (NUnit). They run in Unity's Test Runner *and* in `sq test` |
| `Game/Assets/Game/Content/` | Your content as JSON. Unity loads it as a `TextAsset`; the tests read the same file |
| `SharpQuest/Wiring/` | One small class per session that tells the tests which of your types to look at |

### Every session

`sq update` (the new session's contract) → write it in Core → add that session's wiring class → `sq test` → commit and push. The run in GitHub's **Actions** tab is your Contract score, exactly as on the other templates. A session you haven't started shows as *not started*, not as a failure.

### Rules for Core (the build tells you when you break one)

- **No `UnityEngine`.** Need the time? Take `float deltaTime` as a parameter. Randomness? A seeded `System.Random`. A position? A `record` of your own
- **C# 9**, because that's Unity's compiler: `namespace X { … }` with braces, constructors instead of primary constructors, `new[] { … }` instead of `[ … ]`
- **No implicit usings**: write `using System;` and `using System.Collections.Generic;` yourself
- **.NET Standard 2.1**, Unity's library: no `DateOnly`; JSON is Newtonsoft (`using Newtonsoft.Json;`)
- **Zero nullable warnings**, and **properties, not public fields**, for what your content has

The template's README has the details and a worked example of a wiring class.

## The domain

Yours to design, but it must have:

- **Content** — the item. At least 20 things with Id, name and at least four attributes
- **An `enum`** — rarity, element, damage type, tier
- **A `record`** — stats, a cost, a position
- **A parent** — Level, Player, Deck, Wave, Biome, Region
- **Your feature** — the heart of your game, in your rules library: combat resolution, a loot system, a wave generator, a crafting tree, an ability-gated map, an economy. Name it at CP1

## What each session adds

| S | Required feature | In a game | On the game track (Unity) |
|---|---|---|---|
| 2 | 3+ types, an enum, an interface, zero nullable warnings | Content, parent, a record, an enum. Something nullable that means something — an equipped weapon slot that's empty | Your content loads from a JSON file in `Content/`; a bad file gives an empty list, not a crash |
| 3 | 5+ tests over your own domain logic | Your feature's rules: damage after armour, a loot roll with a fixed seed, a level-up at exactly the threshold | NUnit tests in `Tests/Core/` (`[Test]`, `[TestCase]`, `Assert.Throws`), plus 4+ tests of the `SearchMatcher` sq gives you, graded by how many of its 8 broken versions they catch. Then a search over your content that uses it |
| 4 | Abstract base + 2 kinds, a polymorphic loop, a switch expression | Kinds of content (`Melee` / `Ranged` / `Boss`; `Weapon` / `Armour` / `Potion`), each with a rule of its own. A switch for effectiveness, rarity colour or loot tier | At least one override must **decide** something (damage taken, a gate passed, the next move), not only describe |
| 5 | Custom collection with indexer + iterator; a generic type | The inventory or bestiary: an indexer, an iterator. Your generic type holds *recently picked up* | Same, in Core |
| 6 | An event with 2 subscribers; a function as a parameter | "Enemy killed", "item picked up" — heard by two parts of the game | Raised in Core; your MonoBehaviours subscribe (and unsubscribe in `OnDisable`) |
| 7 | 5 queries answering non-trivial questions | Balance questions: damage per cost, strongest per tier, drop rates, enemies a level-5 player can beat, average wave difficulty | Same, in Core, as methods passed to `Ask(...)`: between them one filters, one sorts, one adds up, one groups, and one is an extension method. Show them on a stats or debug screen |
| 8 | Async save/load that survives a restart | **A save game.** Each kind back as its own kind, stats intact | With Newtonsoft; shown in the **built** game: quit, reopen, same state |
| 9 | A window bound to the model | An inventory, roster, deck or bestiary screen — **not the game viewport** | A view model in Core (an observable list, a selection, a details line, a command); a UI Toolkit screen in Unity shows it |
| 10 | 4 endpoints + DI + no secrets in source | `/items` — list, one, add, delete: a content catalogue, or a leaderboard | *Optional*: a small leaderboard or run-history API the game posts to |
| 11 | A migration + one one-to-many | Your parent → your content: Level → Enemies, Player → Inventory. One parent per item at a time | *Optional*, with session 10: Player → Runs |
| 12 | An attribute-driven feature + your own runner | `[Show("Damage")]` decides what the inventory's details list. Your `[Check]`s test your game's rules | Same, in Core (a tooltip or a tuning panel driven by the attribute) |

The game track's checkpoints are the project route's: **checkpoint 2** in session 6 scores sessions 2–6, **checkpoint 3** in session 9 scores 7–9, **checkpoint 4** in session 12 scores 12 (and 10–11 if you built them).

## At the end, you can show

1. The game running — a minute of play (**on the game track: the built game, not the editor**)
2. The inventory/roster screen: search, select, details
3. Your feature working, and your five balance questions answered
4. A save, a quit, a load — and you're where you were
5. Plain C#: the API answering in a browser, and refusing a POST without the key. Game track: the same if you built it

## On tutorials

Following one is explicitly permitted: *"Following a tutorial is fine. You will be asked to change what it produced."* The verification will ask you to change a rule, live, and explain why the code works. If you can do that, the tutorial helped you learn. If you can't, it did the lab instead of you — and the Change and Comprehension halves of the grade will show it.

**Unity tutorials put the rules inside MonoBehaviours** (`Update()` deciding damage, `OnTriggerEnter` counting coins). On the game track, part of the work is moving each rule into Core and leaving the MonoBehaviour to call it. That's exactly what the checkpoints ask about.

## Watch out for

- **Scope.** A small game with rich content beats a big game with none. You have one semester and the requirements above
- **Random must be testable.** Pass a seeded `Random` into anything that rolls
- **Decks → cards** is many-to-many if a card can be in several decks. One deck per card, or pick a different parent
- **Game track: polish earns nothing until the end.** Art, animation and level design are the final demo's business; the checkpoints score the C# in Core. Get a grey-box scene working first
- **Game track: big files.** Keep large art and audio out of the first commits, or ask about Git LFS before your repository grows past a few hundred MB
