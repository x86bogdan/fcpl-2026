# The project route

You build the whole project on your own, at your own pace, and come to **four checkpoints** instead of every lab. Everything is graded exactly as on the guided route: the same eleven labs, the same contract tests, the same 10 points per lab (Contract 40% · Change 30% · Comprehension 30%). Only the timing differs: a checkpoint scores several labs in one sitting.

| Checkpoint | Session | Scores | Worth up to | Takes |
|---|---|---|---|---|
| **1** | 3 | nothing: it's the booking | 0 | ~5 min |
| **2** | 6 | Labs 02, 03, 04, 05, 06 | 50 (+ stretch) | ~15 min |
| **3** | 9 | Labs 07, 08, 09 | 30 (+ stretch) | ~15 min |
| **4** | 12 | Labs 10, 11, 12 | 30 (+ stretch) | ~15 min |

Each row in a checkpoint is scored like a lab: the **Contract** part comes from your latest green run in GitHub's **Actions** tab, then **one live change** in that lab's area and **two "why" questions**. The change can cross labs: *"add a kind, make the loop print it, and have a subscriber count it."*

---

## Checkpoint 1 (session 3): the booking

Bring [`CHECKPOINT-1.md`](checkpoints/CHECKPOINT-1.md) filled in: your theme, two sentences on what it does by the end, your repository, and a plan with one row per lab saying how that feature will look **in your project** and by which checkpoint. I look at whether the plan is concrete ("which five questions will your LINQ answer?") and whether your riskiest part has a fallback.

**Booked:** you're on the project route from session 4. **Not booked, or not there:** you're on the guided route. That's a safety net, not a penalty.

## What must be ready by session 6 (checkpoint 2)

**Five labs, green on GitHub, and code you can talk about.**

| Lab | Required feature | The contract checks, among others |
|---|---|---|
| 02 | 3+ types of your own, 1 enum, 1 interface, zero nullable warnings | a store that keeps insertion order, refuses duplicates and nulls; JSON in and out through a DTO |
| 03 | 5+ xUnit tests over your own logic | a `[Theory]`, an `Assert.Throws`, SearchMatcher tests that catch 8 of 8 broken matchers |
| 04 | an abstract base with 2+ kinds, a polymorphic loop, a switch expression | `out` in a Try method, `params`; **and Labs 02–03 still green** |
| 05 | your store as a real collection (indexer + iterator), one generic type of your own | `yield`, a lazy query, a type that works for any `T` |
| 06 | one publisher, two subscribers, a function passed as a parameter | an `event`, raised after a successful add, never on a rejected one |

The checklist for the day:

- [ ] `sq update` run, so you have the contracts for Labs 02–06, and `sq test` shows all five
- [ ] The latest run in the **Actions** tab is green, or you know exactly which checks fail and why
- [ ] You can open the code for each row above in under a minute
- [ ] [`CHECKPOINT-2.md`](checkpoints/CHECKPOINT-2.md) filled in: where each feature lives, what changed since your plan, what you're least sure of
- [ ] Your own tests (Lab 03) actually test **your** rules, not only the wiring

**Where you are is fine.** A checkpoint where two labs are honestly unfinished is a useful conversation; those rows score what's there. A checkpoint where everything is green and you can't explain any of it scores low.

Contracts go out ahead of time so you can build against them: **Labs 02–06 now**, 07–09 before checkpoint 2, 10–12 before checkpoint 3. `sq update`, then `sq test`. A lab you haven't started shows as *not started*, not as a failure. Reference solutions appear only after each lab's Friday deadline, as for everyone.

## Checkpoints 3 and 4

The same, for Labs 07–09 in session 9 ([`CHECKPOINT-3.md`](checkpoints/CHECKPOINT-3.md)) and Labs 10–12 in session 12 ([`CHECKPOINT-4.md`](checkpoints/CHECKPOINT-4.md)). Lab 09's window and Lab 12's runner are shown running, on your machine.

## If you miss one

- **Checkpoint 2 or 3:** recovered at the next checkpoint at **80%**, alongside that checkpoint's own labs. It's a long sitting; coming on time is much cheaper.
- **Checkpoint 4:** recovered in session 13 or 14.
- **Fifteen minutes each means about two of you per build block.** If I can't reach you in that session, you're first at the start of the next one, at full value. I'll say so in the room.

## Building a game in Unity?

There's a **game track** for it: its own template, the same checkpoints, nine graded rows (sessions 2–9 and 12; the web API and the database are optional) and extra points for the final demo of the game. How it works and how to set it up: [theme 21, *Build a Game*](themes/21-build-a-game/SPEC.md#two-ways-to-build-it).

## Switching

To the guided route: any time, tell me, and from then on you come to every lab. To the project route after session 3: only if there's room, at my discretion.
