# Airline Flight Board — full spec

## The pitch

One airport's operations for one day. Flights arrive and depart, get gates, get delayed; aircraft fly several flights in a row, so one delay can ripple. By January it is a desktop board, a web API and a database — the same flights underneath all three.

## The domain

At least these, named however you like:

- **Flight** — the item. Id (`RO301-2026-10-14`), flight number, other airport, scheduled time, optional actual time, optional gate, status
- **Status** — an `enum`: Scheduled, Boarding, Delayed, Departed, Landed, Cancelled
- **Airport** — a `record`: code (`OTP`), city
- **Aircraft** — the parent: registration (`YR-ASA`), type, seats
- **Your feature** — pick **one** and build it across the semester:
  - *The board* — next departures and arrivals from a given time, sorted, with statuses that change
  - *Knock-on delays* — an aircraft's late arrival delays its next departure (minimum turnaround), and the app shows the chain
  - *Gates* — no two flights at the same gate within 30 minutes of each other; conflicts listed

**Data:** one airport, one day: at least **40 flights** flown by at least **eight aircraft**, both directions, every status used, some aircraft flying three or more legs.

## What each session adds

| S | Required feature | In this theme |
|---|---|---|
| 2 | 3+ types, an enum, an interface, zero nullable warnings | `Flight`, `Aircraft`, `Airport` record, `Status`. Gate is nullable — "not assigned yet" is a real board state. Actual time is nullable too. Optionally fetch aircraft overhead from OpenSky with `HttpClient` |
| 3 | 5+ tests over your own domain logic | Delay in minutes, a flight past midnight, a turnaround shorter than the minimum, a cancelled flight with no delay |
| 4 | Abstract base + 2 kinds, a polymorphic loop, a switch expression | `Departure` / `Arrival` ("to Paris" vs "from Paris") — `Cargo` is a good third. The switch labels — delayed over 3 hours is "compensation due", boarding with no gate is "check the board" |
| 5 | Custom collection with indexer + iterator; a generic type | The schedule: `schedule[0]`, `schedule["RO301-2026-10-14"]`, `foreach`. Your generic type holds *recently viewed* flights |
| 6 | An event with 2 subscribers; a function as a parameter | "Flight added" — heard by the board and by a log. *Delay announced* is the stretch |
| 7 | 5 queries answering non-trivial questions | e.g. next 10 departures after a time · average delay per destination · each aircraft's legs in order · gates used most · flights at risk from a late aircraft |
| 8 | Async save/load that survives a restart | The schedule to JSON, each kind back as its own kind, **times and statuses intact** |
| 9 | A window bound to the model | **A departure board**: rows by time, status coloured, search by flight or city, details per flight |
| 10 | 4 endpoints + DI + no secrets in source | `/items` — list, one, add, delete. The screens around the terminal read the same API |
| 11 | A migration + one one-to-many | **Aircraft → Flights.** A flight is flown by one aircraft (or none assigned yet); deleting an aircraft keeps its flights |
| 12 | An attribute-driven feature + your own runner | `[Show("Gate")]` decides what the details panel lists. Your `[Check]`s test your feature |

## At the end, you can show

1. The board: search "Paris", select a flight, see its gate, aircraft and delay
2. Your feature working — the next hour's departures, a delay's knock-on chain, or a gate conflict
3. Your five questions, answered on your data
4. The app closed and reopened, the day unchanged
5. The API answering in a browser, and refusing a POST without the key

## Stretch ideas

A delay-announced event updating the board live · a simulated clock that moves statuses forward · arrivals board beside departures · a second airport.

## Watch out for

- **The domain is bigger than it looks.** One airport, one day, no passengers, no bookings, no crews. That's already a real system
- **OpenSky gives positions, not timetables** — and anonymous access is limited. Your timetable is your own; OpenSky is decoration
- **Times cross midnight.** A 23:50 departure delayed by 30 minutes leaves tomorrow. `TimeOnly` breaks there; use a date with the time
