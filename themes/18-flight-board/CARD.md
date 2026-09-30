# Airline Flight Board

**One airport, one day: every departure and arrival, and what a delay does next.**

You build the flight board for an airport: departures and arrivals, gates, statuses, and the aircraft that fly them. Then the questions an operations desk asks: *"what's leaving in the next hour?"*, *"if this plane is late, which later flights does it drag down?"*, *"are two flights on the same gate?"*

- **Your items:** flights — number, route, scheduled and actual times, gate, status
- **Where the data comes from:** your own timetable for one airport and one day. Optionally [OpenSky](https://openskynetwork.github.io/opensky-api/) for real aircraft overhead (free, limited without an account)
- **Something happens when:** a flight is added, delayed, boarding, or cancelled
- **Parent and children:** an Aircraft flies many flights
- **What you'll look at:** the departure board — the best-looking window in the catalogue

**Good for you if** you like airports, timetables, and a UI that looks like the real thing. Keep it to one airport and one day.
