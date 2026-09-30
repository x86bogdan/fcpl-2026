# Helpdesk / Ticket Tracker — full spec

## The pitch

A small team's ticket tracker. Tickets belong to projects, move through states by rules, and pile up on people. The app keeps the rules and answers the Monday-morning questions. By January it is a desktop app, a web API and a database — the same tickets underneath all three.

## The domain

At least these, named however you like:

- **Ticket** — the item. Id (`WEB-42`), title, priority, state, optional assignee, opened date, optional closed date
- **State** — an `enum`: Open, In Progress, In Review, Done, Reopened
- **Priority** — an `enum`: Low, Normal, High, Critical
- **Person** — a `record`: name and email
- **Project** — the parent: key (`WEB`), name, lead
- **Your feature** — pick **one** and build it across the semester:
  - *Workflow rules* — which state can move to which; an illegal move is refused with a reason
  - *SLAs* — each priority has a time limit; the app knows what's overdue and by how much
  - *Workload* — open tickets per person, weighted by priority, and who to give the next one to

**Data:** at least **40 tickets** in at least **three projects**, every state and priority used, some unassigned, dates spread over a few months.

## What each session adds

| S | Required feature | In this theme |
|---|---|---|
| 2 | 3+ types, an enum, an interface, zero nullable warnings | `Ticket`, `Project`, `Person` record, `State`, `Priority`. Assignee is nullable — null means "nobody's on it", a real state a lead cares about. Closed date is nullable too |
| 3 | 5+ tests over your own domain logic | Your feature: Done → In Progress is refused, a Critical ticket is overdue after 4 hours, an unassigned ticket counts toward nobody's workload |
| 4 | Abstract base + 2 kinds, a polymorphic loop, a switch expression | `Bug` (steps to reproduce, severity) / `FeatureRequest` (who asked) / `Question`. **The switch is your workflow**: `(state, action) switch { (Open, Start) => InProgress, … , _ => refused }` — the best pattern-matching fit in the catalogue |
| 5 | Custom collection with indexer + iterator; a generic type | The board: `board[0]`, `board["WEB-42"]`, `foreach`. Your generic type holds *recently viewed* tickets |
| 6 | An event with 2 subscribers; a function as a parameter | "Ticket opened" — heard by the project's open count and by a log. *State changed* is the stretch |
| 7 | 5 queries answering non-trivial questions | e.g. overdue tickets by priority · open tickets per person (with "unassigned") · average days open per kind · the oldest open Critical · tickets reopened more than once |
| 8 | Async save/load that survives a restart | The board to JSON, each kind back as its own kind, **states, dates and assignees intact** |
| 9 | A window bound to the model | The ticket list with search, a details panel, and buttons that only light up for legal moves |
| 10 | 4 endpoints + DI + no secrets in source | `/items` — list, one, add, delete. A monitoring script opens tickets by itself |
| 11 | A migration + one one-to-many | **Project → Tickets.** A ticket is in one project; deleting a project keeps its tickets |
| 12 | An attribute-driven feature + your own runner | `[Show("Priority")]` decides what the details panel lists. Your `[Check]`s test your workflow |

## At the end, you can show

1. The window: search a title, select a ticket, move it — and try a move that's refused
2. Your feature working — the workflow, the overdue list, or who gets the next ticket
3. Your five questions, answered on your data
4. The app closed and reopened, every state the same
5. The API answering in a browser, and refusing a POST without the key

## Stretch ideas

A state-changed event with a history per ticket · comments · a Kanban view with a column per state · a move endpoint that answers 409 for an illegal move.

## Watch out for

- **Assignee is the tempting second relationship.** A person has many tickets — but S11 grades one; Project → Tickets is it. Keep assignees as records until then
- **Time zones.** "Overdue by 3 hours" depends on when "now" is. Pass the time in; never read the clock deep inside a rule
- **Don't build Jira.** Sprints, epics, permissions and custom fields are all out
