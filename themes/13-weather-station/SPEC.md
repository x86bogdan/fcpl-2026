# Weather Station Dashboard — full spec

## The pitch

A dashboard for the places you care about — home, your university city, somewhere you'd rather be. It pulls real readings and forecasts, keeps them, raises alerts, and checks the forecasts against what happened. By January it is a desktop app, a web API and a database — the same readings underneath all three.

## The domain

At least these, named however you like:

- **Reading** — the item: one station, one day. Id (`buc-2026-10-03`), date, min and max temperature, rain, wind, conditions
- **Conditions** — an `enum` mapped from Open-Meteo's weather codes: Clear, Cloudy, Fog, Rain, Snow, Storm…
- **Coordinates** — a `record`: latitude and longitude
- **Station** — the parent: name, coordinates, elevation
- **Your feature** — pick **one** and build it across the semester:
  - *Alerts* — thresholds per station (frost, heat, storm wind), and the readings that crossed them
  - *Forecast accuracy* — the forecast for a day next to what happened; how wrong, on average, by lead time
  - *Records* — hottest, coldest, wettest per station and month, and when a record breaks

**Data:** at least **three stations**, at least **three weeks** of daily readings each — 60+ items. Fetch once from Open-Meteo, save, and add new days when you want.

## What each session adds

| S | Required feature | In this theme |
|---|---|---|
| 2 | 3+ types, an enum, an interface, zero nullable warnings | `Reading`, `Station`, `Coordinates` record, `Conditions`. Rain is nullable — a day with no measurement is not a dry day. **Fetch real readings with `HttpClient`** — this theme's S2 is the most natural in the catalogue |
| 3 | 5+ tests over your own domain logic | Mapping weather codes (an unknown code too), a frost threshold at exactly 0 °C, the average of an empty week |
| 4 | Abstract base + 2 kinds, a polymorphic loop, a switch expression | `ObservedReading` / `ForecastReading` (made on a day, for a day). They describe themselves differently. The switch labels — max above 35 °C is "heat", wind over 60 km/h is "storm", below 0 is "frost" |
| 5 | Custom collection with indexer + iterator; a generic type | The readings: `readings[0]`, `readings["buc-2026-10-03"]`, `foreach`. Your generic type holds *recently viewed stations* |
| 6 | An event with 2 subscribers; a function as a parameter | "Reading added" — heard by the dashboard and by a log. *Threshold crossed* is the stretch |
| 7 | 5 queries answering non-trivial questions | e.g. rain per station per week · the hottest day per station · days below zero · average forecast error by lead time · the station with the widest daily range |
| 8 | Async save/load that survives a restart | The readings to JSON, each kind back as its own kind, **decimals and dates intact** |
| 9 | A window bound to the model | Readings with search (a station, a condition), details, and today's alerts |
| 10 | 4 endpoints + DI + no secrets in source | `/items` — list, one, add, delete. A small sensor on a balcony could POST its own readings |
| 11 | A migration + one one-to-many | **Station → Readings.** A reading belongs to one station; deleting a station keeps its readings |
| 12 | An attribute-driven feature + your own runner | `[Show("Max °C")]` decides what the details panel lists. Your `[Check]`s test your thresholds |

## At the end, you can show

1. The window: pick a station, search "rain", select a day, see its details
2. Your feature working — an alert fired, the forecast's error, or a broken record
3. Your five questions, answered on your data
4. The app closed and reopened, the readings the same
5. The API answering in a browser, and refusing a POST without the key

## Stretch ideas

A threshold-crossed event · fetching new days in the background while the window stays responsive · hourly readings · a small chart per station.

## Watch out for

- **Open-Meteo is free for non-commercial use**, with fair-use limits. Fetch a range in one call, not one call per day
- **Save what you fetch.** The labs and CI run from your JSON, not the network — and history doesn't change, so there's no reason to fetch it twice
- **Forecast and observation for the same day are two readings.** Give them different Ids, or the store's duplicate rule will refuse the second
