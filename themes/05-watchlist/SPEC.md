# Film & Series Watchlist — full spec

## The pitch

A watchlist that knows the difference between a two-hour film and a nine-season series. It tracks what you plan, watch, finish and drop, and turns it into numbers. By January it is a desktop app, a web API and a database — the same titles underneath all three.

## The domain

At least these, named however you like:

- **Title** — the item. Id, name, release year, genres, status, optional rating
- **Status** — an `enum`: Planned, Watching, Finished, Dropped
- **Progress** — a `record` for a series: episodes seen and episodes in total
- **Franchise** — the parent: *The Lord of the Rings*, *Studio Ghibli*, *Marvel*. Name, and one thing of your own
- **Your feature** — pick **one** and build it across the semester:
  - *Episode tracker* — watch an episode, progress moves, the last one finishes the series
  - *Time budget* — hours left on the whole list, and how many evenings that is
  - *Taste profile* — ratings by genre, what you drop and when, a "watch next" pick from your own data

**Data:** at least **30 titles**, films and series both, across at least **five franchises** (a title can also have none), every status used.

## What each session adds

| S | Required feature | In this theme |
|---|---|---|
| 2 | 3+ types, an enum, an interface, zero nullable warnings | `Title`, `Franchise`, `Progress` record, `Status`. Rating is nullable — null means "not rated", not zero. Optionally fetch one title from TMDB or Jikan with `HttpClient` |
| 3 | 5+ tests over your own domain logic | Test your feature: watching the last episode finishes the series, hours left on a half-watched series, a dropped series counting as zero hours left |
| 4 | Abstract base + 2 kinds, a polymorphic loop, a switch expression | `Movie` / `Series` (`Anime` or `Documentary` is a good third). A film has a runtime, a series has episodes — they describe themselves differently. The switch labels — e.g. a series with two episodes left is "almost done", a film over three hours is "block an evening" |
| 5 | Custom collection with indexer + iterator; a generic type | The list: `list[0]`, `list["tt-0012"]`, `foreach`. Your generic type holds *recently watched* |
| 6 | An event with 2 subscribers; a function as a parameter | "Title added" — heard by the hours-left counter and by a log. *Episode watched* and *series finished* are the stretch |
| 7 | 5 queries answering non-trivial questions | e.g. hours left on the list · the genre you drop most · average rating per franchise · series you stopped before the halfway point · the longest film you've finished |
| 8 | Async save/load that survives a restart | The list to JSON, each kind back as its own kind, **progress intact to the episode** |
| 9 | A window bound to the model | Your list with search and a status filter, a details panel, a progress bar for series |
| 10 | 4 endpoints + DI + no secrets in source | `/items` — list, one, add, delete. A friend can see what you're watching |
| 11 | A migration + one one-to-many | **Franchise → Titles.** A title belongs to one franchise or none; deleting a franchise keeps its titles |
| 12 | An attribute-driven feature + your own runner | `[Show("Runtime")]` decides what the details panel lists. Your `[Check]`s test your feature |

## At the end, you can show

1. The window: filter to Watching, select a series, see its progress
2. Your feature working — an episode watched, the hours left, or a "watch next" pick
3. Your five questions, answered on your data
4. The app closed and reopened, progress unchanged
5. The API answering in a browser, and refusing a POST without the key

## Stretch ideas

Episode-watched and series-finished events · seasons inside a series · posters in the window · a "friends' ratings" import from another student's API.

## Watch out for

- **TMDB needs a free account and key.** Keep it in an environment variable, never in code or in your seed file. The labs and the CI run from your saved JSON. Jikan needs no key but allows only a few requests a second — fetch once, save
- **Episodes are not your items.** Titles are. Episodes are numbers inside a series (the Progress record); a table of every episode is a stretch
- **A series still airing has no final episode count.** Decide now what that looks like — it's a nullable waiting to happen
