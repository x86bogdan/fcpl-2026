# University Course Planner — full spec

## The pitch

A planner for a real degree. It holds the curriculum, checks that each semester adds up, finds timetable clashes and tells you what you can't take yet. By January it is a desktop app, a web API and a database — the same courses underneath all three.

## The domain

At least these, named however you like:

- **Course** — the item. Id (the course code), name, credits (ECTS), assessment, timetable slots, prerequisite codes
- **Assessment** — an `enum`: Exam, Colloquium, Project…; and `DayOfWeek` already exists
- **Time slot** — a `record`: day, start, end, room
- **Semester** — the parent: year, term, the credits it should add up to
- **Prerequisites are a list of course codes inside the course.** Not a table, not a relationship
- **Your feature** — pick **one** and build it across the semester:
  - *Credit check* — every semester's credits, which ones miss 30, and what's optional on top
  - *Clashes* — two courses in the same semester whose slots overlap
  - *Can I take it?* — given what you've passed, what's open to you now (direct prerequisites only)

**Data:** your programme: at least **30 courses** over at least **four semesters**, every kind, each with at least one slot, some with prerequisites.

## What each session adds

| S | Required feature | In this theme |
|---|---|---|
| 2 | 3+ types, an enum, an interface, zero nullable warnings | `Course`, `Semester`, `TimeSlot` record, `Assessment`. The room is nullable — "not announced yet" is how most timetables start |
| 3 | 5+ tests over your own domain logic | Slots that touch (10:00–12:00 and 12:00–14:00) don't clash, slots that overlap by a minute do; a course with no prerequisites is always open; credits of an empty semester |
| 4 | Abstract base + 2 kinds, a polymorphic loop, a switch expression | `Mandatory` / `Elective` (part of a group you choose from) / `Optional` (extra credits, not toward the degree), each describing itself differently. The switch labels — anything 6+ credits is "heavy", an elective with no slot yet is "not scheduled" |
| 5 | Custom collection with indexer + iterator; a generic type | The curriculum: `courses[0]`, `courses["CS201"]`, `foreach`. Your generic type holds *recently viewed* courses |
| 6 | An event with 2 subscribers; a function as a parameter | "Course added" — heard by the semester's credit total and by a log. *Plan broke a rule* is the stretch |
| 7 | 5 queries answering non-trivial questions | e.g. credits per semester · clashing pairs · courses open to you now · courses whose prerequisite is in a later semester (a catalogue mistake!) · busiest day of the week |
| 8 | Async save/load that survives a restart | The curriculum to JSON, each kind back as its own kind, **slots and prerequisites intact** |
| 9 | A window bound to the model | Courses with search and a semester filter, details with prerequisites, a weekly grid as the stretch |
| 10 | 4 endpoints + DI + no secrets in source | `/items` — list, one, add, delete. Your year group's planner reads the same API |
| 11 | A migration + one one-to-many | **Semester → Courses.** A course is in one semester; deleting a semester keeps its courses, unplanned |
| 12 | An attribute-driven feature + your own runner | `[Show("Credits")]` decides what the details panel lists. Your `[Check]`s test clashes and credits |

## At the end, you can show

1. The window: filter to a semester, select a course, see its slots and prerequisites
2. Your feature working — a semester short of 30 credits, a clash found, or what you can take next
3. Your five questions, answered on your data
4. The app closed and reopened, the plan intact
5. The API answering in a browser, and refusing a POST without the key

## Stretch ideas

Transitive prerequisites (what you need before what you need) — with a cycle check · a weekly timetable grid · a rule-broken event · a second student's passed-courses file.

## Watch out for

- **Prerequisites are a graph.** Checking *direct* prerequisites is a `Contains`; checking the whole chain is recursion, and a cycle makes it loop forever. Direct only, unless it's your stretch
- **Prerequisites are many-to-many**, and S11 asks for one-to-many. Keep them as a list of codes inside the course; Semester → Courses is the graded relationship
- **Your faculty's real curriculum is the best data** — and the least work to invent
