# Pokédex — full spec

## The pitch

A trainer's Pokédex. It pulls Pokémon from PokéAPI, keeps the ones you've registered, groups them under their trainers, and answers questions about your collection. By January it is a desktop app, a web API and a database — the same Pokémon underneath all three.

## Read this first: you are not the worked example

Every lab's slides and reference solution use a Pokédex. That's why this theme is supported so well, and also why it has one extra rule. **These parts must be different from the reference:**

| The reference has | Yours must not |
|---|---|
| Kinds `WildPokemon` / `CaughtPokemon` | Split Pokémon by *whether they're caught*. Split them by what they **are** (see S4 below) |
| A `Trainer` with a name and a last catch | Stop at a name. Your trainer has something of its own (badges, a region, a team limit) |
| The queries on the slides | Be your five questions. Yours are in the S7 row |

Copying the reference is easy to spot at the verification — you'll be asked to change it, live, and it will be obvious whose code it is.

## The domain

At least these, named however you like:

- **Pokémon** — the item. Id, species, one or two types, level, base stats (HP, Attack, Defense, Speed at minimum), optional nickname
- **Type** — an `enum`. At least eight of the eighteen
- **Stats** — a `record`: four or more numbers that belong together and compare by value
- **Trainer** — the parent. Name, and one thing of your own
- **Your feature** — pick **one** and build it across the semester:
  - *Type matchups* — "what beats this?", using a small effectiveness table
  - *Evolution* — a Pokémon evolves at a level; the dex updates and says so
  - *Team builder* — six at most, and the app tells you what your team is weak to

**Data:** at least **30 Pokémon**, across at least six types and three trainers. Load them from PokéAPI once, then save your own JSON seed so the labs don't depend on the network.

## What each session adds

| S | Required feature | In this theme |
|---|---|---|
| 2 | 3+ types, an enum, an interface, zero nullable warnings | `Pokemon`, `Trainer`, `Stats` record, `PokemonType` enum, an interface of your own. The nickname is nullable — and null *means* "no nickname". Fetch one species with `HttpClient` and map it |
| 3 | 5+ tests over your own domain logic | Test your feature: a matchup, an evolution threshold, a team that's full. Not getters |
| 4 | Abstract base + 2 kinds, a polymorphic loop, a switch expression | Kinds by what a Pokémon **is**: e.g. `Regular` / `Legendary` / `Mythical`, or `Basic` / `Evolved`. The switch classifies — e.g. a Legendary above level 70 is "a boss" |
| 5 | Custom collection with indexer + iterator; a generic type | The dex itself: `dex[3]`, `dex["pkmn-025"]`, `foreach`. Your generic type holds the *recently viewed* Pokémon |
| 6 | An event with 2 subscribers; a function as a parameter | "Registered in the dex" — heard by the trainer's summary and by a log. Your feature's event (evolved, levelled up) is the stretch |
| 7 | 5 queries answering non-trivial questions | e.g. strongest per type · types with nobody in them · average level per trainer · dual-types sorted by total stats · the best counter to a given type |
| 8 | Async save/load that survives a restart | The whole dex to JSON, **each kind coming back as its own kind** |
| 9 | A window bound to the model | The dex list, a search box, a details panel with stats. Sprite image is a stretch |
| 10 | 4 endpoints + DI + no secrets in source | `/items` — list, one, add, delete. A friend's app can read your dex |
| 11 | A migration + one one-to-many | **Trainer → Pokémon.** A Pokémon has one trainer or none; removing a trainer frees their Pokémon |
| 12 | An attribute-driven feature + your own runner | `[Show("Attack")]` on stats decides what the details panel lists. Your `[Check]`s test your feature |

## At the end, you can show

1. The window: search "char", select one, see its stats and trainer
2. Your feature working — a matchup answered, an evolution happening, or a team's weakness named
3. Your five questions, answered on your data
4. The app closed and reopened, the data still there
5. The API answering in a browser, and refusing a POST without the key

## Stretch ideas

Sprites in the window · a second event for your feature · caching PokéAPI responses on disk · a battle simulation (simple: higher effective attack wins).

## Watch out for

- **PokéAPI is generous, not unlimited.** Fetch your 30 once and save them. Never call it in a loop on every start
- **Don't model all 1,000+ species.** Thirty well-chosen ones beat a thousand you can't reason about
