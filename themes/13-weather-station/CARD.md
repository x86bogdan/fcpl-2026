# Weather Station Dashboard

**Real weather for real places — and a check on how good the forecast was.**

You build a weather dashboard: a few stations (cities you care about), their daily readings, forecasts next to what actually happened, and alerts when something crosses a line. Then the questions: *"which city had the most rain this month?"*, *"how wrong was the forecast three days out?"*, *"when did it last drop below zero?"*

- **Your items:** readings — station, day, min and max temperature, rain, wind, conditions
- **Where the data comes from:** [Open-Meteo](https://open-meteo.com) — free, no key, forecasts and history
- **Something happens when:** a reading arrives, or a threshold is crossed (frost, storm, heat)
- **Parent and children:** a Station has many readings
- **What you'll look at:** a dashboard per station, with alerts

**Good for you if** you like real data that changes every day — and want `async` to feel necessary, not academic.
