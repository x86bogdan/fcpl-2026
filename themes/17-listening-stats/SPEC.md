# Listening Stats ("Wrapped") — full spec

## The pitch

A year-in-review you control. It reads a listening history — thousands of plays — and turns it into top artists, habits, discoveries and milestones. By January it is a desktop report, a web API and a database — the same plays underneath all three.

*Not the same as Music Library:* that theme manages a collection. This one analyses a history. If you want both, pick this one and make the library your stretch.

## The domain

At least these, named however you like:

- **Play** — the item. Id, when (a `DateTimeOffset`), track, album, duration played, skipped or not
- **Platform** — an `enum`: Phone, Desktop, Web, Speaker
- **Track** — a `record`: title, album, artist name
- **Artist** — the parent: name, first played, and one thing of your own (genre, country)
- **Your feature** — pick **one** and build it across the semester:
  - *Year in review* — top 5 artists and tracks by minutes, total hours, your top month
  - *Habits* — listening by hour and weekday, what you play at night vs in the morning, skip rate
  - *Discovery* — new artists per month, which discoveries stuck, the artist you dropped

**Data:** at least **500 plays** over at least **three months**, from at least **20 artists**. Your real export is best. Committing your own history to a private repo is your choice — or commit a trimmed, anonymised sample and keep the full file out of git.

## What each session adds

| S | Required feature | In this theme |
|---|---|---|
| 2 | 3+ types, an enum, an interface, zero nullable warnings | `Play`, `Artist`, `Track` record, `Platform`. Album is nullable — podcast episodes and some singles don't have one. Parsing the export is your S2 JSON mapping |
| 3 | 5+ tests over your own domain logic | A skip (under 30 seconds), minutes per artist, a play just after midnight counting for the right day, an empty history |
| 4 | Abstract base + 2 kinds, a polymorphic loop, a switch expression | `SongPlay` / `PodcastPlay` (the export has both), each describing itself differently. The switch labels by time and length — a play between 00:00 and 05:00 is "night owl", under 30 seconds is "skip" |
| 5 | Custom collection with indexer + iterator; a generic type | The history: `history[0]`, `history["pl-000123"]`, `foreach`. Your generic type holds *recently played artists* |
| 6 | An event with 2 subscribers; a function as a parameter | "Play recorded" — heard by the running totals and by a log. *Milestone reached* is the stretch |
| 7 | 5 queries answering non-trivial questions | e.g. top artists by **minutes**, not plays · listening by hour of day · skip rate per artist · the month with the most new artists · your longest daily streak |
| 8 | Async save/load that survives a restart | The history to JSON, each kind back as its own kind, **timestamps with their offsets** — and it stays responsive with thousands of plays |
| 9 | A window bound to the model | **The report**: top lists, a search over plays, details per play. The UI row is only as good as your report, so make it the point |
| 10 | 4 endpoints + DI + no secrets in source | `/items` — list, one, add, delete. A scrobbler could POST each play as it happens |
| 11 | A migration + one one-to-many | **Artist → Plays.** A play belongs to one artist; deleting an artist keeps their plays |
| 12 | An attribute-driven feature + your own runner | `[Show("Played at")]` decides what the details panel lists. Your `[Check]`s test your stats |

## At the end, you can show

1. The window: your report, a search for an artist, one play's details
2. Your feature working — your year, your habits, or your discoveries
3. Your five questions, answered on your data
4. The app closed and reopened, the totals the same
5. The API answering in a browser, and refusing a POST without the key

## Stretch ideas

A milestone event · a small chart of listening per month · compare two people's exports · share-card image of your top five.

## Watch out for

- **Timestamps in exports are UTC.** "What do I play at 2 a.m." needs your local time. Use `DateTimeOffset` and convert on purpose
- **Minutes, not plays.** A 12-minute song and a skipped one are both "a play"; decide which you count, for every question
- **Your history is personal data.** Don't put it in a public repo, and think before you POST it anywhere
