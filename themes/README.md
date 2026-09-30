# The project catalogue

Choose one in session 1. It's yours for the whole semester: every lab adds one layer to it.

Each folder has two files:

- **`CARD.md`**: one page. Read the cards first.
- **`SPEC.md`**: the full spec. Read it once you have two or three favourites.

| # | Theme | In one line | Data | API key? |
|---|---|---|---|---|
| 01 | [Pokédex](01-pokedex/CARD.md) | Catch them, store them, compare them | PokéAPI | no |
| 02 | [Music Library](02-music-library/CARD.md) | Your music, organised, and finally explained | your own, MusicBrainz | no |
| 03 | [Café POS](03-cafe-pos/CARD.md) | The till for a small café: orders in, totals right, nothing lost | your own, Frankfurter | no |
| 04 | [Game Library](04-game-library/CARD.md) | Every game you own, what you actually played, and the backlog | your own, RAWG | **yes (free)** |
| 05 | [Film & Series Watchlist](05-watchlist/CARD.md) | What to watch, what you watched, and what you dropped | TMDB or Jikan | **TMDB yes**, Jikan no |
| 06 | [TCG Deck Builder](06-tcg-deck-builder/CARD.md) | Your card collection, and decks that are actually legal | Scryfall | no |
| 07 | [Formula 1 Season](07-f1-season/CARD.md) | Every race result, and the championship computed from scratch | Jolpica-F1 | no |
| 08 | [Gym & Workout Log](08-gym-log/CARD.md) | Log it, track it, watch the numbers go up | your own, wger | no |
| 09 | [Recipe Book & Meal Planner](09-recipe-planner/CARD.md) | Recipes, a week of meals, and the shopping list that writes itself | TheMealDB | public test key |
| 10 | [Helpdesk / Ticket Tracker](10-helpdesk/CARD.md) | A small Jira of your own: tickets, states, and rules | your own | — |
| 11 | [Bookstore Rental](11-bookstore-rental/CARD.md) | Who has which book, when it's due back, and what they owe | Open Library | no |
| 12 | [Plant Care Tracker](12-plant-care/CARD.md) | Every plant in the flat, and which one you're slowly killing | your own, Open-Meteo | no |
| 13 | [Weather Station](13-weather-station/CARD.md) | Real weather for real places, and how good the forecast was | Open-Meteo | no |
| 14 | [Portfolio Tracker](14-portfolio-tracker/CARD.md) | Every buy and sell, what you hold now, and whether you're up | your own, CoinGecko | **yes (free demo)** |
| 15 | [Dungeon Bestiary](15-dungeon-bestiary/CARD.md) | Monsters, their stats, what they drop, and encounters | D&D 5e SRD API | no |
| 16 | [Car Rental](16-car-rental/CARD.md) | A fleet, its branches, bookings that never overlap | your own, NHTSA vPIC | no |
| 17 | [Listening Stats ("Wrapped")](17-listening-stats/CARD.md) | Your own year in review, from your real listening history | your own export | — |
| 18 | [Airline Flight Board](18-flight-board/CARD.md) | One airport, one day, and what a delay does next | your own, OpenSky | no |
| 19 | [University Course Planner](19-course-planner/CARD.md) | Your own degree, with clashes and missing prerequisites found | your faculty's | — |
| 20 | [Slot Machine Arcade](20-slot-machine/CARD.md) | Reels, bets, payouts, and the statistics of who really wins | simulated | — |
| 21 | [Build a Game](21-build-a-game/CARD.md) | A game of your choice, as long as it runs on data | your own design | — |

**"Your own"** means you write the data yourself (a JSON file you make once), optionally topped up from the API named.
**API keys** go in an environment variable, never in your code. The labs and the tests run from saved JSON, so a missing key never breaks your build.

## Your own idea instead?

Allowed, if it passes four questions:

1. Does it have **20+ items with varied attributes**? (Otherwise LINQ has nothing to work with.)
2. Is there a moment where **something happens**? (For events.)
3. Do your items have a **natural parent**? (For the database: a Trainer has Pokémon, an Album has tracks.)
4. Is there **something worth looking at**? (For the user interface.)

Bring it to the instructor in session 1.

## Two people, one theme?

Fine. Same theme, separate repositories, separate verification. You'll each be asked different questions.
