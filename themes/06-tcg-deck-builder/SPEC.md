# TCG Deck Builder — full spec

## The pitch

A deck builder with a rules engine. It keeps your card collection, lets you build decks from it, and checks each deck against a format's rules — and tells you exactly which rule it breaks. By January it is a desktop app, a web API and a database — the same cards underneath all three.

## The domain

At least these, named however you like:

- **Card** — the item. Id, name, cost, colour(s), rarity, type line, and power/toughness where it has them
- **Rarity** and **Colour** — `enum`s
- **Cost** — a `record`: how much and of what colour
- **Set** — the parent: the expansion a card was printed in. Code, name, release date
- **Deck** — a name and a list of (card, copies). Decks are your feature, not the items in your store
- **Your feature** — pick **one** and build it across the semester:
  - *Legality* — a format's rules (deck size, copy limits, banned cards), and a list of every broken rule
  - *Deck analysis* — mana curve, colour balance, average cost, "what's missing"
  - *Collection value* — what you own, duplicates to trade, what a deck needs that you don't have

**Data:** at least **60 cards** from at least **four sets**, all card types represented. Pick a game you know — rules you understand make the tests easy to write.

## What each session adds

| S | Required feature | In this theme |
|---|---|---|
| 2 | 3+ types, an enum, an interface, zero nullable warnings | `Card`, `Set`, `Cost` record, `Rarity`, `Colour`. Power is nullable — a spell has none, which is not the same as 0. Optionally fetch one card from Scryfall with `HttpClient` |
| 3 | 5+ tests over your own domain logic | The rules: 59 cards is illegal, five copies is illegal, but basic lands are unlimited; the curve of an empty deck |
| 4 | Abstract base + 2 kinds, a polymorphic loop, a switch expression | `Creature` / `Spell` / `Land`, each describing itself differently. The switch classifies — a creature with power 5+ is a "finisher", a land is "mana", a cheap spell is "interaction" |
| 5 | Custom collection with indexer + iterator; a generic type | The collection: `cards[0]`, `cards["dmu-107"]`, `foreach`. Your generic type holds *recently viewed* |
| 6 | An event with 2 subscribers; a function as a parameter | "Card added to the collection" — heard by the collection counter and by a log. *Deck became illegal* is the stretch |
| 7 | 5 queries answering non-trivial questions | e.g. the mana curve (count per cost, 7+ together) · colour balance · cards owned but in no deck · most common type per set · average cost per rarity |
| 8 | Async save/load that survives a restart | The collection to JSON, each kind back as its own kind, **costs and colours intact** |
| 9 | A window bound to the model | The collection with search, card details, and — your feature — a deck list with its legality or curve |
| 10 | 4 endpoints + DI + no secrets in source | `/items` — list, one, add, delete. A friend can browse your collection before a trade |
| 11 | A migration + one one-to-many | **Set → Cards.** Each printing belongs to one set; deleting a set keeps its cards |
| 12 | An attribute-driven feature + your own runner | `[Show("Cost")]` decides what the details panel lists. Your `[Check]`s test your format's rules |

## At the end, you can show

1. The window: search a card, see its details and set
2. Your feature working — a deck that breaks two rules, and the app naming both
3. Your five questions, answered on your data
4. The app closed and reopened, the collection intact
5. The API answering in a browser, and refusing a POST without the key

## Stretch ideas

A deck-became-illegal event · a second format with different rules behind the same interface · card images in the window · deck import from the text format players already share.

## Watch out for

- **Decks → cards is many-to-many.** A card is in many decks, with a number of copies. S11 asks for one-to-many, so Set → Cards is the graded relationship; decks in the database are a stretch after S11
- **Scryfall requires a User-Agent and an Accept header** on every request, and asks for a short pause between them. Fetch your cards once, save them
- **Don't implement the game.** You're checking decks, not playing them. Card text stays text
