# Car Rental

**A fleet, its branches, bookings that never overlap, and prices with rules.**

You build the system for a small car-rental company: vehicles spread across branches, bookings for date ranges, and pricing rules that change with the car, the length and the day of the week. Then the questions: *"what's free at the airport next weekend?"*, *"which branch's cars sit idle?"*, *"what does a week in a van cost?"*

- **Your items:** vehicles — plate, make, model, kind, seats, daily rate, mileage, where it is
- **Where the data comes from:** your own fleet. Optionally the [NHTSA vPIC API](https://vpic.nhtsa.dot.gov/api/) for real makes and models (free, no key)
- **Something happens when:** a vehicle joins the fleet, a booking is confirmed, or a car comes back late
- **Parent and children:** a Branch has many vehicles
- **What you'll look at:** the fleet, filterable by branch, and a vehicle's bookings

**Good for you if** you like rules that business people actually argue about — prices, dates, and "it was supposed to be back yesterday".

*Prefer a parking garage? Same shape: spots instead of vehicles, levels instead of branches. Ask at CP1.*
