# Gym & Workout Log — full spec

## The pitch

A training log that notices progress. Every session goes in; the app groups them into blocks, finds your records and tells you when you beat one. By January it is a desktop app, a web API and a database — the same workouts underneath all three.

## The domain

At least these, named however you like:

- **Workout** — the item. Id, date, duration, optional effort rating (RPE 1–10), notes
- **Lift** — an `enum`: Squat, Bench, Deadlift, Overhead Press, Row… (add your own)
- **TopSet** — a `record`: weight and reps. It's also where an estimated one-rep max belongs
- **Training block** — the parent: "Autumn strength", "5K plan". Name, goal, start and end
- **Your feature** — pick **one** and build it across the semester:
  - *Personal records* — best per lift, by weight and by estimated 1RM, and the day you set it
  - *Progress per block* — did the block do what its goal said? First week vs last week
  - *Consistency* — weekly volume, streaks, the longest gap

**Data:** at least **40 workouts** over at least **eight weeks** and **three blocks**, both kinds, several lifts. Your real training is ideal; a believable invented log is fine — say which.

## What each session adds

| S | Required feature | In this theme |
|---|---|---|
| 2 | 3+ types, an enum, an interface, zero nullable warnings | `Workout`, `TrainingBlock`, `TopSet` record, `Lift`. RPE is nullable — null means "didn't rate it", not zero effort. Dates as `DateOnly` |
| 3 | 5+ tests over your own domain logic | Estimated 1RM (Epley: weight × (1 + reps / 30)) — one rep gives the weight back; a PR is beaten, equalled, or not; an empty log has no records |
| 4 | Abstract base + 2 kinds, a polymorphic loop, a switch expression | `StrengthWorkout` (a lift and a top set) / `CardioWorkout` (distance and pace) — `Mobility` is a good third. The switch labels — a strength session at RPE 9+ is "a grinder", a run under 5:00/km is "fast" |
| 5 | Custom collection with indexer + iterator; a generic type | The log: `log[0]`, `log["wo-2026-10-03"]`, `foreach`. Your generic type holds *recently used lifts* — the quick-pick on a real app |
| 6 | An event with 2 subscribers; a function as a parameter | "Workout logged" — heard by the weekly counter and by a log. *PR beaten* is the stretch — and the best one to build |
| 7 | 5 queries answering non-trivial questions | e.g. best estimated 1RM per lift · volume per week · the longest gap between sessions · kilometres per month · average RPE per block |
| 8 | Async save/load that survives a restart | The log to JSON, each kind back as its own kind, **weights exact and dates unshifted** |
| 9 | A window bound to the model | Your log with search, details per workout, your current records beside it |
| 10 | 4 endpoints + DI + no secrets in source | `/items` — list, one, add, delete. Log a session from your phone's HTTP client |
| 11 | A migration + one one-to-many | **Training block → Workouts.** A workout is in one block or none; deleting a block keeps its workouts |
| 12 | An attribute-driven feature + your own runner | `[Show("Top set")]` decides what the details panel lists. Your `[Check]`s test your records |

## At the end, you can show

1. The window: search "squat", select a workout, see its top set
2. Your feature working — a new PR detected, a block's progress, or your streak
3. Your five questions, answered on your data
4. The app closed and reopened, the records the same
5. The API answering in a browser, and refusing a POST without the key

## Stretch ideas

A PR-beaten event with a banner · every set of a workout, not only the top one · kg/lb switching · a chart of one lift over time.

## Watch out for

- **Weights are `decimal`.** 102.5 kg is a real plate combination; `double` will eventually print 102.49999
- **Dates are `DateOnly`.** A workout is on a day, not at a moment — and a `DateTime` saved in one time zone and read in another can move to the day before
- **Records are computed, never stored.** A stored "best squat" goes stale the day you delete a mistyped workout
