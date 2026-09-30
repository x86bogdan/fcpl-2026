# FCPL lab — the contract

**14 sessions · C# 14 on .NET 10**

The FCPL lab runs as **SharpQuest**, the quest to learn C#. Where you see that name, it means this lab.

One semester. One project you choose in week one and finish in week fourteen. Everything you learn in each lab goes straight into it.

```
$ fcpl-lab --how-am-i-graded
✓ Each lab is worth up to 10 points, plus 2 for the stretch task.
✓ Finished after the lab? It counts as late, for up to [TBD].
✓ Missed one? Recover it in the last two sessions for up to 8, with at most 4 recoveries.
✓ 90 points is a 10. 45 points passes.
✓ Showing up without working code is worth about 2.
```

That last line is the whole philosophy. Attendance is worth points, but it is not what's being graded. What you built and what you understand is.

---

## What you'll build

In the first session you choose one project from [a catalogue of twenty-one](themes/README.md): a Pokédex, a café till, a deck builder, a gym log, a game, or your own proposal. **You choose once, and it's yours for the semester.**

Every lab after that adds one layer to it. By the end you have a single application with a data model, a test suite, a user interface, a web API and a database behind it. You build it yourself, one lab at a time, in a domain you actually care about.

There is no separate "lab exercise" and "final project". They are the same thing.

---

## Two routes

You pick your route in the first session. A short [placement task](placement/README.md) that evening tells you which one fits. It's advice, not an assignment, and it carries no grade. You decide.

| **Guided** · all 14 sessions | **Project** · 4 checkpoints + demo day |
|---|---|
| Build your project one layer per week | Build the whole thing independently, at your own pace |
| Starter code, interface and tests provided every lab | You get the spec and the requirements table, no scaffolding |
| Stuck halfway through a lab? Someone is standing right there | Come to four sessions instead of fourteen |
| Best if C# is new to you, or if you want the structure | Best if you already write C# and want your lab time back |

> Both routes are graded identically, out of the same points, against the same requirements. The project route is not the easy option. It is the same course with the scaffolding removed and the deadlines further apart. You still have to learn databases and UI, and nobody will teach them to you in a lab.

---

## How a lab is scored

Every lab, on both routes, is scored on the same things.

| Part | Points | What it means | Who checks |
|---|---|---|---|
| **Contract** | 4 | The lab's interface is implemented and the tests are green | You, before you call me |
| **Change** | 3 | You make one small modification to your own code, live, in the lab | Me, a few minutes |
| **Comprehension** | 3 | You answer two questions about why your code is built the way it is | Me, ~3 min |
| **Stretch** | +2 | An optional, deliberately under-specified extra task | Me |

You run the tests yourself, first. That's the deal: I never spend your lab time telling you your code doesn't compile, and I spend it on the interesting part instead.

### What 10, 8 and 6 actually look like

*Example: the LINQ lab.*

- **10**: All five queries implemented, tests green. You made the change I asked for on your own, in under ten minutes. You explained why you used `FirstOrDefault` rather than `First`, and what it means that a LINQ query hasn't actually run yet.
- **8**: Tests green. You made the change, but needed a hint or used the whole ten minutes. One question answered well, the other roughly.
- **6**: Most tests green. The change half-works. You can describe what your code does, but not why it's shaped that way.
- **under 5**: Tests failing, or you can't modify code that is supposedly yours.

---

## Late work

The exercises are meant to be finished **during the lab**. Work you complete after the lab counts as **late** and is worth up to **[TBD]** points.

You don't have to argue about this, and neither do I: your GitHub run timestamps decide it. If your tests were green before the lab ended, the lab counts in full, even if I only get to check it at the next session.

---

## Missing a lab

Life happens. Missing a lab is a setback, not a disaster.

- Do the work in your own time and push it.
- Recover it in the **recovery weeks: sessions 13 and 14**. Tests green, one change, two questions, same as always. [Pace to confirm: up to 2 labs per recovery session.]
- A recovered lab is worth up to **8 points** instead of 10, and the stretch bonus isn't available.
- You can recover up to **4 labs**. The final deadline is session 14.

The arithmetic, plainly: missing two labs and recovering them costs you nothing. Missing two and ignoring them costs you a full grade point. Missing four and ignoring them costs nearly three. Coming back is always cheaper than not.

**On the project route:** if you miss checkpoint 2 or 3 you can recover it at the next one, at 80% of its value. Checkpoint 4 is recoverable in session 13 or 14. If you miss or fail **checkpoint 1**, you move to the guided route. That checkpoint exists to catch a project that's scoped wrong in week three rather than week nine. It's a safety net, not a punishment.

---

## AI, and tutorials

You may use AI assistants: Copilot, ChatGPT, Claude, whatever you like. **Declare it in your README**: which tool, roughly what for. It does not reduce your grade.

Following a tutorial is fine too. Adapting someone's open-source project is fine.

> **You are not being graded on who wrote the code. You are being graded on whether you can navigate it.**
>
> If you can't change your own architecture when asked, you score low no matter who or what produced it. If you can, you've learned the thing, and how you got there is your business.

Two things are not allowed, and they're the obvious ones: submitting someone else's work as your own, and having someone else answer for you during a verification.

One practical consequence: **commit as you go.** A repository with a real history is the easiest way to show your work is yours. A single commit containing a finished project invites questions you'd rather not have to answer.

---

## What your project must contain

One row per lab. This table is the whole syllabus; everything else is detail. Both routes have to satisfy all eleven.

| Lab | Topic | Your project must have |
|---|---|---|
| S2 | Types & modelling | 3+ types, 1 enum, 1 interface, no nullable warnings |
| S3 | Testing | 5+ xUnit tests over your own logic |
| S4 | OOP & pattern matching | An abstract base with 2 derived types, one polymorphic loop, one switch expression |
| S5 | Collections & generics | A custom collection with an indexer and an iterator; one generic type or method |
| S6 | Functions & events | One publisher and two subscribers; one function passed as a parameter |
| S7 | LINQ | 5 queries that answer real questions about your data |
| S8 | Persistence & async | Async save and load that survives closing the app |
| S9 | User interface | A working window bound to your model |
| S10 | Web API | 4 endpoints, dependency injection, no secrets in source control |
| S11 | Database | An EF Core migration to SQLite with one relationship |
| S12 | Reflection | An attribute-driven feature, and your own miniature test runner |

If you propose your own project instead of taking one from the catalogue, it has to pass four questions. Does it have **20+ items with varied attributes** (or LINQ has nothing to work with)? Is there a moment where **something happens** (for events)? Do your items have a **natural parent** (for the database)? Is there **something worth looking at** (for the UI)? Bring it to me in session 1.

---

## The semester

| # | Session | Graded |
|---|---|---|
| S1 | Kickoff: pick your project, meet the rules | — |
| S2 | Types & modelling | 10 |
| S3 | Testing & the contract · *checkpoint 1* | 10 |
| S4 | OOP & pattern matching | 10 |
| S5 | Collections & generics | 10 |
| S6 | Functions as values & events · *checkpoint 2* | 10 |
| S7 | LINQ | 10 |
| S8 | Persistence & async | 10 |
| S9 | User interface · *checkpoint 3* | 10 |
| S10 | Web API | 10 |
| S11 | Database & ORM | 10 |
| S12 | Reflection · *checkpoint 4* | 10 |
| S13 | Recovery, plus a talk on something outside the syllabus | — |
| S14 | Recovery, demos, and one last talk | — |

Eleven graded labs. 110 points on the table before stretch bonuses, and 90 of them make a 10. The slack is deliberate, so one bad lab doesn't follow you around.

---

## What you need

- **.NET.** The course uses **.NET 10**. For session 1, **.NET 8 or newer** is enough. We'll get it working in the lab, on the lab machines, your laptop, or both. See [SETUP.md](SETUP.md), and don't fight it alone for more than a few minutes.
- **An editor:** Visual Studio, Rider, or VS Code with the C# Dev Kit. All three are supported. If something only works on one of them, that's my bug, so tell me.
- **A GitHub account.** Your repository is created from the course template in session 1. Private is fine. Commit as you go.
- From session 2, your repository has a `global.json` so that everyone in the room is on the same SDK. Please don't delete it.

---

## Feedback, and the scoreboard

Every two labs there's a short feedback form. It takes two minutes and asks concrete things: what took you longest, what you had to ask about, what you didn't finish. Your running total on the scoreboard updates when you submit it.

The scoreboard is public but anonymous. In session 1 you claim an [avatar](AVATARS.md), one of fifteen animals in one of twelve colours, and that's your identity on it for the rest of the semester. You'll know you're the Blue Fox; nobody else will unless you tell them. You'll be able to see where you stand at any point in the semester, which is the thing last year's students most wanted and least had.

I read every response, and I change things because of them. This document exists because last year's cohort said, clearly and repeatedly, that they wanted to know how they were being graded.

---

*Questions about any of this are welcome at any point in the semester, including "is this fair", which is a reasonable thing to ask about a set of rules you didn't write.*
