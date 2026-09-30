# Car Rental — full spec

## The pitch

A small rental company's system. Vehicles live at branches; customers book them for date ranges; prices follow rules. The app refuses double bookings, prices a rental correctly, and knows what's idle. By January it is a desktop app, a web API and a database — the same fleet underneath all three.

## The domain

At least these, named however you like:

- **Vehicle** — the item. Id (the plate), make, model, year, seats, daily rate, mileage
- **Transmission** or **Fuel** — an `enum`
- **Booking** — a `record`: customer name, start date, end date. Kept inside the vehicle — bookings are your feature, not your items
- **Branch** — the parent: city, address, opening hours
- **Your feature** — pick **one** and build it across the semester:
  - *Availability* — no overlapping bookings, and "what's free between these dates, here?"
  - *Pricing* — weekend rates, long-rental discounts, a young-driver surcharge, a one-way fee
  - *Utilisation* — days booked vs days available, per vehicle, per branch, per month

**Data:** at least **25 vehicles** at at least **four branches**, every kind, each with a few bookings over the next two months.

*Parking-garage variant:* spots are the items (size, EV charger, covered), levels are the parents, bookings are the same. Everything below applies.

## What each session adds

| S | Required feature | In this theme |
|---|---|---|
| 2 | 3+ types, an enum, an interface, zero nullable warnings | `Vehicle`, `Branch`, `Booking` record, an enum. A return date is nullable — a car that's out has none yet. Optionally look up makes with `HttpClient` |
| 3 | 5+ tests over your own domain logic | Overlap: back-to-back bookings (one ends the day another starts) are fine; one day of overlap is not. A price for 7 days vs 6 |
| 4 | Abstract base + 2 kinds, a polymorphic loop, a switch expression | `Car` / `Van` / `ElectricCar` (battery range), each describing itself. **The switch is your pricing**: `(vehicle, days) switch { (Van, >= 7) => …, (ElectricCar, _) => …, _ => … }` |
| 5 | Custom collection with indexer + iterator; a generic type | The fleet: `fleet[0]`, `fleet["B-123-XYZ"]`, `foreach`. Your generic type holds *recently booked* |
| 6 | An event with 2 subscribers; a function as a parameter | "Vehicle added" — heard by the branch's count and by a log. *Booking confirmed* and *returned late* are the stretch |
| 7 | 5 queries answering non-trivial questions | e.g. free vehicles at a branch for a date range · utilisation per branch · revenue per kind · the most-booked model · vehicles due for service by mileage |
| 8 | Async save/load that survives a restart | The fleet to JSON, each kind back as its own kind, **every booking and date intact** |
| 9 | A window bound to the model | The fleet with search and a branch filter, a vehicle's details and bookings |
| 10 | 4 endpoints + DI + no secrets in source | `/items` — list, one, add, delete. A partner website lists your cars |
| 11 | A migration + one one-to-many | **Branch → Vehicles.** A vehicle is at one branch; deleting a branch keeps the vehicles, unassigned |
| 12 | An attribute-driven feature + your own runner | `[Show("Seats")]` decides what the details panel lists. Your `[Check]`s test your pricing or overlaps |

## At the end, you can show

1. The window: filter to a branch, select a van, see its bookings
2. Your feature working — a double booking refused, a week priced with its discount, or the idlest branch
3. Your five questions, answered on your data
4. The app closed and reopened, every booking the same
5. The API answering in a browser, and refusing a POST without the key

## Stretch ideas

A booking-confirmed event · one-way rentals moving the car to another branch · a calendar view of one vehicle · a returned-late event with a fee.

## Watch out for

- **Decide whether end dates are inclusive** on day one, write it in a comment, and test it. Half the bugs in this theme are that
- **Bookings live inside the vehicle.** A customer table and a bookings table are a stretch after S11
- **Dates are `DateOnly`**, prices `decimal`
