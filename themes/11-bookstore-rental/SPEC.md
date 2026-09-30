# Bookstore Rental — full spec

## The pitch

Emma runs *BookSmart* and tracks rentals in a notebook. You build her system: the shelf, who has what until when, what they owe if they're late, and orders for books she doesn't have. By January it is a desktop app, a web API and a database — the same books underneath all three.

## The domain

At least these, named however you like:

- **Book** — the item: one physical copy. Id, title, author, year, shelf price, optional due date
- **Genre** or **Condition** — an `enum`
- **Money** — a `record` of amount and currency, `decimal` always
- **Customer** — the parent: name, phone, member since
- **Your feature** — pick **one** and build it across the semester:
  - *Late fees* — a daily rate per kind of book, capped, calculated on return
  - *Special orders* — a book she doesn't stock, an estimated arrival, and a list of what's coming
  - *Rental history* — every rental remembered: who reads what, who's always late

**Data:** at least **30 copies** of at least **20 titles**, some out, some overdue, and at least **six customers**. Pull titles and authors from Open Library once and save them.

## What each session adds

| S | Required feature | In this theme |
|---|---|---|
| 2 | 3+ types, an enum, an interface, zero nullable warnings | `Book`, `Customer`, `Money` record, an enum. Due date is nullable — null means "on the shelf", and that's the most important null in the theme. Optionally look a book up by ISBN on Open Library with `HttpClient` |
| 3 | 5+ tests over your own domain logic | Fees: returned on the due date is free, one day late costs the rate, the fee never exceeds the book's price, a book on the shelf owes nothing |
| 4 | Abstract base + 2 kinds, a polymorphic loop, a switch expression | By kind of book: `Novel` / `Textbook` / `Comic`, each with its own rental period and rate, each describing itself differently. The switch labels — anything 30+ days overdue is "presumed lost", due today is "due today" |
| 5 | Custom collection with indexer + iterator; a generic type | The shelf: `shelf[0]`, `shelf["bk-0142"]`, `foreach`. Your generic type holds *recently returned* — the trolley by the counter |
| 6 | An event with 2 subscribers; a function as a parameter | "Book added" — heard by the stock count and by a log. *Rented*, *returned* and *overdue* are the stretch |
| 7 | 5 queries answering non-trivial questions | e.g. overdue books, most overdue first · fees owed per customer · titles with every copy out · the customer who's late most often · the most-rented author |
| 8 | Async save/load that survives a restart | The shelf to JSON, each kind back as its own kind, **due dates unshifted and fees exact** |
| 9 | A window bound to the model | The shelf with search, a details panel, overdue books marked, a Return button |
| 10 | 4 endpoints + DI + no secrets in source | `/items` — list, one, add, delete. The shop's website can show what's on the shelf |
| 11 | A migration + one one-to-many | **Customer → Books they have.** A copy is with one customer or on the shelf; deleting a customer returns their books |
| 12 | An attribute-driven feature + your own runner | `[Show("Due")]` decides what the details panel lists. Your `[Check]`s test your fees |

## At the end, you can show

1. The window: search a title, see which copies are out and when they're due
2. Your feature working — a late return and its fee, a special order arriving, or a customer's history
3. Your five questions, answered on your data
4. The app closed and reopened, every due date the same
5. The API answering in a browser, and refusing a POST without the key

## Stretch ideas

An overdue event checked each morning · renewals · reservations for a copy that's out · a receipt printed on return.

## Watch out for

- **A book and a copy are not the same.** Three copies of *Dune* are three items with the same title. Decide that on day one, not in week eight
- **"Today" is a parameter.** A fee calculated with `DateTime.Now` inside can't be tested, and changes while you look at it
- **Due dates are `DateOnly`.** A book is due on a day; a `DateTime` can slide to the day before when it's saved in one time zone and read in another
