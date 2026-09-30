# Game Library — full spec

## The pitch

A personal game library that keeps you honest. It tracks what you own, what you've played and for how long, and turns that into numbers you might not want to see. By January it is a desktop app, a web API and a database — the same games underneath all three.

## The domain

At least these, named however you like:

- **Game** — the item. Id, title, platform, genre, release year, hours played, price paid, status, optional rating
- **Status** — an `enum`: Backlog, Playing, Completed, Abandoned (add your own)
- **Platform** — a second `enum`, or a `record` if you want more than a name
- **Developer** — the parent. Name, country, founded year
- **Your feature** — pick **one** and build it across the semester:
  - *Backlog shame* — estimated hours to clear the backlog, and how many months that is at your real play rate
  - *Value for money* — cost per hour played, best and worst purchases, what subscriptions actually cost you
  - *Completion tracker* — finishing a game records a date; the app shows your completion rate by genre and year

**Data:** at least **30 games** from at least **six developers**, with every status used and at least three platforms. Your real library is best — the questions are more fun when the answers are about you.

## What each session adds

| S | Required feature | In this theme |
|---|---|---|
| 2 | 3+ types, an enum, an interface, zero nullable warnings | `Game`, `Developer`, `Status`, `Platform`. Rating is nullable — null means "haven't rated it", not zero. Optionally fetch one game from RAWG with `HttpClient` |
| 3 | 5+ tests over your own domain logic | Test your feature: cost per hour, backlog hours, a game with zero hours played (what *should* that return?) |
| 4 | Abstract base + 2 kinds, a polymorphic loop, a switch expression | Kinds by how you own it: `PhysicalGame` / `DigitalGame` / `SubscriptionGame` (it leaves the service one day). The switch labels — e.g. a subscription game leaving within 30 days is "play it now" |
| 5 | Custom collection with indexer + iterator; a generic type | The library: `library[0]`, `library["gm-017"]`, `foreach`. Your generic type holds *recently played* |
| 6 | An event with 2 subscribers; a function as a parameter | "Game added" — heard by the backlog counter and by a log. *Completed* and *abandoned* are the stretch |
| 7 | 5 queries answering non-trivial questions | e.g. hours per genre · the genre you abandon most · cost per hour, worst first · backlog in hours · highest-rated game you never finished |
| 8 | Async save/load that survives a restart | The library to JSON, each kind back as its own kind, **statuses and prices intact** |
| 9 | A window bound to the model | Your shelf with a search box and a status filter, a details panel, the backlog total updating live |
| 10 | 4 endpoints + DI + no secrets in source | `/items` — list, one, add, delete. A friend's app can see what you're playing |
| 11 | A migration + one one-to-many | **Developer → Games.** A game has one developer; deleting a developer keeps their games |
| 12 | An attribute-driven feature + your own runner | `[Show("Hours")]` decides what the details panel lists. Your `[Check]`s test your feature |

## At the end, you can show

1. The window: filter to Backlog, search a title, select it, see its details
2. Your feature working — backlog hours, the worst-value purchase, or a completion rate
3. Your five questions, answered on your data
4. The app closed and reopened, the data still there
5. The API answering in a browser, and refusing a POST without the key

## Stretch ideas

A completed event that stamps the date and updates stats · achievements per game · a "what should I play next" rule · cover art in the window.

## Watch out for

- **RAWG needs a free key.** Keep it in an environment variable, never in code or in your seed file. The labs and the CI run from your saved JSON, so nothing breaks without it
- **Hours are yours, not the API's.** Type them in; nothing public knows how long *you* played
- **Don't import your whole Steam account.** Thirty games you can reason about beat four hundred you can't
