# Café POS — full spec

## The pitch

Emma runs *The Cozy Cup* and still writes sales on paper. You build her till: the menu, orders with correct totals, and a sales log she can query. By January it is a desktop app, a web API and a database — the same menu underneath all three.

## The domain

At least these, named however you like:

- **Menu item** — the item. Id, name, category, price, optional size, allergens, available or not
- **Category** — the parent: Coffee, Tea, Pastries, Sandwiches…
- **Money** — a `record` of amount and currency, using **`decimal`, never `double`**
- **An `enum`** — Size, or Allergen, or Payment method
- **Order** — lines (item + quantity), a total, a time. Orders are not the items in your store; they are what your feature is built from
- **Your feature** — pick **one** and build it across the semester:
  - *Orders and totals* — build an order, apply a discount, complete it, log it
  - *Daily report* — revenue, best-sellers, busiest hour, from the day's orders
  - *Stock* — pastries run out; the menu greys them out and says so

**Data:** at least **25 menu items** in at least **five categories**, with varied prices, sizes and allergens. For the report, at least **50 orders** — generate them with a seeded `Random` so the numbers are the same every run.

## What each session adds

| S | Required feature | In this theme |
|---|---|---|
| 2 | 3+ types, an enum, an interface, zero nullable warnings | `MenuItem`, `Category`, `Money` record, an enum. Size is nullable — a croissant has no size, and null says so. Optionally fetch a EUR rate with `HttpClient` |
| 3 | 5+ tests over your own domain logic | Order totals, a discount, rounding to the cent, an empty order, a quantity of zero |
| 4 | Abstract base + 2 kinds, a polymorphic loop, a switch expression | `Drink` / `Food` (a `Combo` is a good third). Each describes itself its own way. The switch prices or labels — e.g. a large drink costs 20% more, a food item after 18:00 is half price |
| 5 | Custom collection with indexer + iterator; a generic type | The menu: `menu[0]`, `menu["cof-003"]`, `foreach`. Your generic type holds the *recently ordered* items — the quick-pick buttons on a real till |
| 6 | An event with 2 subscribers; a function as a parameter | "Added to the menu" — heard by the menu board and by a log. *Order completed* and *sold out* are the stretch |
| 7 | 5 queries answering non-trivial questions | e.g. best-seller per category · revenue by hour · items never ordered · average order value · everything vegan under 15 lei |
| 8 | Async save/load that survives a restart | The menu to JSON, each kind back as its own kind, **prices exact to the cent** |
| 9 | A window bound to the model | The menu with search, details, and — your feature — a running order total |
| 10 | 4 endpoints + DI + no secrets in source | `/items` — list, one, add, delete. The kitchen screen and the menu board read the same API |
| 11 | A migration + one one-to-many | **Category → Menu items.** An item is in one category; deleting a category keeps its items, uncategorised |
| 12 | An attribute-driven feature + your own runner | `[Show("Price")]` decides what the details panel lists. Your `[Check]`s test totals and discounts |

## At the end, you can show

1. The window: search "latte", select it, see its price and category
2. Your feature working — an order totalled and completed, a daily report, or an item selling out
3. Your five questions, answered on your data
4. The app closed and reopened, prices unchanged to the cent
5. The API answering in a browser, and refusing a POST without the key

## Stretch ideas

An order-completed event feeding a live "today's takings" counter · a receipt printed to a text file · payment methods with a switch expression · the daily report in the window.

## Watch out for

- **Money is `decimal`.** `0.1 + 0.2` in `double` is not `0.3`, and a till that's off by a cent is a till that's wrong. The tests will catch it; better that you do first
- **Orders → lines is not the S11 relationship.** A menu item appears in thousands of orders. Category → items is the graded one-to-many
- **No real payments, no real printers.** Pretend the card was accepted
