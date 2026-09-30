# Plant Care Tracker — full spec

## The pitch

A plant tracker that remembers for you. Each plant knows how often it wants water; you log what you do; the app says what's due, what's overdue and which plant is in trouble. By January it is a desktop app, a web API and a database — the same plants underneath all three.

## The domain

At least these, named however you like:

- **Plant** — the item. Id, nickname, species, light, date added, optional last-watered date, care history
- **Light** — an `enum`: Shade, Medium, Bright, Direct sun
- **Care event** — a `record`: date and what was done (watered, fed, repotted, pruned — another enum)
- **Room** — the parent: Kitchen, Bedroom, Balcony. Name, light it gets, indoor or outdoor
- **Your feature** — pick **one** and build it across the semester:
  - *Schedule* — due today, overdue, due this week, per plant and per room
  - *Health* — how far each plant's real care is from what it wants; the ones in trouble first
  - *Weather-aware* — outdoor plants skip watering after rain (Open-Meteo), indoor ones don't

**Data:** at least **25 plants** in at least **five rooms**, all kinds, each with at least three care events over the last two months. A real flat, a friend's, or a believable invented one.

## What each session adds

| S | Required feature | In this theme |
|---|---|---|
| 2 | 3+ types, an enum, an interface, zero nullable warnings | `Plant`, `Room`, `CareEvent` record, `Light`. Last watered is nullable — null means "never, since you got it", which is its own warning. Optionally fetch today's rain from Open-Meteo with `HttpClient` |
| 3 | 5+ tests over your own domain logic | Next watering date, a plant never watered, overdue by exactly one day, a repot not counting as watering |
| 4 | Abstract base + 2 kinds, a polymorphic loop, a switch expression | `Succulent` / `Tropical` / `Herb` — **each overrides how often it wants water**, the cleanest polymorphism in the catalogue. The switch labels — overdue by more than twice its interval is "you are killing this one" |
| 5 | Custom collection with indexer + iterator; a generic type | The collection: `plants[0]`, `plants["pl-monstera"]`, `foreach`. Your generic type holds *recently cared for* |
| 6 | An event with 2 subscribers; a function as a parameter | "Plant added" — heard by the room's count and by a log. *Watered* and *needs water* are the stretch — and the theme's heart |
| 7 | 5 queries answering non-trivial questions | e.g. due today, grouped by room · the most neglected room · each plant's real average interval vs what it wants · plants never fed · the most overwatered plant |
| 8 | Async save/load that survives a restart | The collection to JSON, each kind back as its own kind, **every care event and date intact** |
| 9 | A window bound to the model | Today's list, search, a plant's details and history, a Water button that updates everything |
| 10 | 4 endpoints + DI + no secrets in source | `/items` — list, one, add, delete. A flatmate's phone can check what's due |
| 11 | A migration + one one-to-many | **Room → Plants.** A plant is in one room or none; deleting a room keeps its plants |
| 12 | An attribute-driven feature + your own runner | `[Show("Light")]` decides what the details panel lists. Your `[Check]`s test your schedule |

## At the end, you can show

1. The window: today's list, select a plant, water it — it leaves the list
2. Your feature working — the week's schedule, the plant in most trouble, or a balcony plant skipping a day after rain
3. Your five questions, answered on your data
4. The app closed and reopened, the history intact
5. The API answering in a browser, and refusing a POST without the key

## Stretch ideas

A watered event that resets the schedule · a morning "needs water" check · seasons (less water in winter) · photos per plant.

## Watch out for

- **"Needs water" is about time, not about an action.** Nothing happens at the moment a plant becomes thirsty. Your app finds out when it *checks* — so checking takes a date, and your tests pick it
- **Care history lives inside the plant.** It's many events per plant, saved with it; S11's graded relationship is Room → Plants
- **Dates are `DateOnly`.** You water on a day
