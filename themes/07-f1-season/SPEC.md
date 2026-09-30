# Formula 1 Season Tracker — full spec

## The pitch

A season, race by race. Every driver's result in every race goes in; the driver and team standings come out, computed by your code. By January it is a desktop app, a web API and a database — the same results underneath all three.

## The domain

At least these, named however you like:

- **Result** — the item. Id (e.g. `2025-monza-VER`), driver, team, race, grid position, points
- **Driver** and **Team** — `record`s: code, name, nationality / name, colour
- **An `enum`** — race type (Grand Prix, Sprint), or retirement reason
- **Race** — the parent. Round, name, circuit, date
- **Your feature** — pick **one** and build it across the semester:
  - *Standings* — driver and team championships from the results, ties broken the way F1 breaks them (most wins, then most seconds…)
  - *Teammate battles* — head-to-head in qualifying and races, per team
  - *Title maths* — points still available, and who can mathematically still win

**Data:** at least **five races** of one season — that's already 100+ results. Fetch them from Jolpica once and save them.

## What each session adds

| S | Required feature | In this theme |
|---|---|---|
| 2 | 3+ types, an enum, an interface, zero nullable warnings | `Result`, `Race`, `Driver` and `Team` records, an enum. A fastest-lap time is nullable — a driver out on lap 1 never set one. Fetch one race's results from Jolpica with `HttpClient` |
| 3 | 5+ tests over your own domain logic | Your points table, a sprint scoring differently, a retirement scoring zero, a tie broken by wins |
| 4 | Abstract base + 2 kinds, a polymorphic loop, a switch expression | By how the race ended: `Finished` / `Retired` (`Disqualified` is a good third). A finisher describes "P3 from P7 (+4)"; a retirement "out on lap 34 — gearbox". The switch labels — a finish that gained 5+ places is a "charge" |
| 5 | Custom collection with indexer + iterator; a generic type | The results: `results[0]`, `results["2025-monza-VER"]`, `foreach`. Your generic type holds *recently viewed* |
| 6 | An event with 2 subscribers; a function as a parameter | "Result posted" — heard by the standings and by a log. *Leader changed* is the stretch |
| 7 | 5 queries answering non-trivial questions | e.g. driver standings · team standings (summed from drivers) · most places gained over the season · retirements per team · each driver's average finish, finishers only |
| 8 | Async save/load that survives a restart | The results to JSON, each kind back as its own kind, **grid, position and points intact** |
| 9 | A window bound to the model | Results with search (a driver, a team), details per result, the standings beside it |
| 10 | 4 endpoints + DI + no secrets in source | `/items` — list, one, add, delete. Post a result on Sunday evening from any client |
| 11 | A migration + one one-to-many | **Race → Results.** A result belongs to one race; deleting a race keeps its results, unassigned |
| 12 | An attribute-driven feature + your own runner | `[Show("Grid")]` decides what the details panel lists. Your `[Check]`s test your scoring |

## At the end, you can show

1. The window: search a driver, see every result, select one for details
2. Your feature working — the standings, a teammate battle, or who can still win
3. Your five questions, answered on your data
4. The app closed and reopened, the standings the same
5. The API answering in a browser, and refusing a POST without the key

## Stretch ideas

A leader-changed event · a standings chart over the season · a sprint weekend handled end to end · a second season, and comparing them.

## Watch out for

- **Ergast is gone** (shut down at the end of 2024). Jolpica serves the same shape of data. It limits how often you can call it — fetch your races once, save them, never call it on every start
- **Standings are computed, never stored.** If a number in your table can go out of date when a result changes, it shouldn't be a field
- **Scoring rules change between seasons.** Pick one season and put its rules in one place
