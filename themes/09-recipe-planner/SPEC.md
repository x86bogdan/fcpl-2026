# Recipe Book & Meal Planner — full spec

## The pitch

A recipe book that does the arithmetic. Recipes have real quantities; plan a week, and the app adds every ingredient up into one shopping list. By January it is a desktop app, a web API and a database — the same recipes underneath all three.

## The domain

At least these, named however you like:

- **Recipe** — the item. Id, name, cuisine, total time, servings, ingredients, steps, optional rating
- **Ingredient line** — a `record`: name, quantity, unit
- **Unit** — an `enum`: Gram, Millilitre, Piece, Teaspoon… (and a `Diet` enum is useful too)
- **Category** — the parent: Breakfast, Pasta, Dessert, Soup. Name and one thing of your own
- **Your feature** — pick **one** and build it across the semester:
  - *Shopping list* — a week's plan becomes one list, same ingredients added together
  - *Fridge mode* — what can I make with what I have, and what's the one thing I'm missing
  - *Scaling* — any recipe for any number of servings, units that stay sensible (1,500 g → 1.5 kg)

**Data:** at least **30 recipes** in at least **five categories**, each with at least four ingredient lines, and ingredients that repeat across recipes (onions, eggs, flour) — the shopping list needs overlap to be interesting.

## What each session adds

| S | Required feature | In this theme |
|---|---|---|
| 2 | 3+ types, an enum, an interface, zero nullable warnings | `Recipe`, `Category`, `IngredientLine` record, `Unit`. Rating is nullable — null means "haven't cooked it yet". Optionally fetch one meal from TheMealDB with `HttpClient` (its quantities are free text — parsing them is part of the fun) |
| 3 | 5+ tests over your own domain logic | Scaling 4 servings to 6, adding 200 g + 300 g of the same thing, two different units of the same thing, an empty plan |
| 4 | Abstract base + 2 kinds, a polymorphic loop, a switch expression | By how it's cooked: `BakedRecipe` (oven temperature) / `StovetopRecipe` / `NoCookRecipe`, each describing itself differently. The switch labels — anything under 20 minutes is "weeknight", baked over 2 hours is "Sunday project" |
| 5 | Custom collection with indexer + iterator; a generic type | The book: `book[0]`, `book["rc-carbonara"]`, `foreach`. Your generic type holds *recently cooked* |
| 6 | An event with 2 subscribers; a function as a parameter | "Recipe added" — heard by the category counts and by a log. *Plan changed* (and the list updating itself) is the stretch |
| 7 | 5 queries answering non-trivial questions | e.g. **the shopping list** (`SelectMany` + `GroupBy` + `Sum`) · vegetarian under 30 minutes · the most-used ingredient · recipes one ingredient away from what's in the fridge · average time per category |
| 8 | Async save/load that survives a restart | The book to JSON, each kind back as its own kind, **every ingredient line intact** |
| 9 | A window bound to the model | The book with search, a recipe's ingredients and steps, and — your feature — the list or the plan |
| 10 | 4 endpoints + DI + no secrets in source | `/items` — list, one, add, delete. Your flatmate adds a recipe from their laptop |
| 11 | A migration + one one-to-many | **Category → Recipes.** A recipe is in one category; deleting a category keeps its recipes |
| 12 | An attribute-driven feature + your own runner | `[Show("Time")]` decides what the details panel lists. Your `[Check]`s test your feature |

## At the end, you can show

1. The window: search "chicken", select a recipe, see its ingredients
2. Your feature working — a week's plan turned into one shopping list, or fridge mode finding a dinner
3. Your five questions, answered on your data
4. The app closed and reopened, every quantity the same
5. The API answering in a browser, and refusing a POST without the key

## Stretch ideas

A plan-changed event that rebuilds the list live · unit conversion (tablespoons to millilitres) · photos in the window · export the list to a text file for your phone.

## Watch out for

- **Recipes → ingredients is many-to-many.** Onions are in half your recipes. S11 asks for one-to-many: Category → Recipes is the graded one; ingredient lines stay inside the recipe
- **TheMealDB's quantities are text** ("1 1/2 cups", "pinch"). Parse what you can, keep the rest as text, and write it down as a decision
- **Quantities are `decimal`.** A third of a teaspoon, scaled, then added — `double` will show you why not
