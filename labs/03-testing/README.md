# Lab 03 — Testing & the contract

**Today:** for two weeks, tests have been grading you. Today you write tests, and a grader checks whether *your* tests are any good: it runs them against eight deliberately broken versions of the code and counts how many they catch.

**Required feature (matrix row 3):** 5+ xUnit tests over your own domain logic

> **In your project:** open your theme's spec ([the 21 themes](https://github.com/x86bogdan/fcpl-2026/blob/main/themes/README.md)) → *What each session adds* → row **3** (your own idea: the required feature above, said for your app). Before you write code, put three lines in your README: which of your classes change today, what they do in your app, and where you'll see it working.

---

## 0. Start (5 min)

**On a lab PC (browser only, no git):** your repository on GitHub → **Code → Download ZIP** → extract it → open the folder in Rider. Then, in **Rider's terminal** (**Alt+F12**):

```
sh sq.sh update
sh sq.sh test
```

**On your laptop, with git:** in your repository folder,

```
git pull
sq update          (Linux/macOS: sh sq.sh update)
sq test
```

> **`sq` stops with *"The provided file path does not exist: tools/sq"*?** Your repository is missing the `tools/` folder (repositories made in session 1 never got it). Fix it once, in your repository folder:
>
> ```
> curl -fsSL https://raw.githubusercontent.com/x86bogdan/fcpl-2026/main/fix-net9.sh | sh
> ```
>
> Windows (PowerShell): `irm https://raw.githubusercontent.com/x86bogdan/fcpl-2026/main/fix-net9.ps1 | iex`. Then save it **with the `tools/` folder**: `git add -A` before you commit, or `sh sq.sh upload`. From today `sq` puts that folder back by itself if it ever goes missing again.

Lab 03 shows as *not started*, and `sq test` now takes a little longer: it runs your tests against the broken versions before the normal checks. When nothing has changed, it reuses the last result.

**Missed Lab 02?** Lab 03 needs a store. See `reference/Lab02/` and the recovery rules.

## 1. Read the tests that graded you (10 min)

Open `contracts/SharpQuest.Contracts.Tests/Lab02/StoreTests.cs`. Pick one test and answer, in a comment at the top of `tests/Capstone.Tests/SmokeTests.cs`:

- What does it arrange, what does it act on, what does it assert?
- What bug in *your* store would make it fail?

## 2. Test a specification you didn't write (30 min)

`contracts/SharpQuest.Contracts/Lab03/Lab03.cs` contains `SearchMatcher` and its rules, **R1–R8**. Read them carefully: they are the whole spec.

In `tests/Capstone.Tests/`, create a class whose name contains **`SearchMatcher`** (e.g. `SearchMatcherTests`) and test every rule:

```csharp
public class SearchMatcherTests
{
    private readonly SearchMatcher _matcher = SearchMatcher.Create();

    [Fact]
    public void R2_case_is_ignored()
    {
        Assert.True(_matcher.Matches("Pikachu", "PIKA"));
    }
}
```

**How this is graded.** Your SearchMatcher tests must all pass on the real matcher. Then `sq` runs them against **eight broken matchers**, each of which gets exactly one rule wrong. A broken matcher is *caught* when at least one of your tests fails on it. Each one caught is worth points. The failure messages tell you which rule each uncaught one breaks.

**Test behaviour only.** Tests that try to detect which matcher is running (environment variables, reflection) are spotted and earn nothing.

## 3. Test your own code (30 min)

Now the rules that only *your* domain has. At least **5 tests** that use your own types directly (not only through a Wiring class), including:

- **one `[Theory]`** with two or more `[InlineData]` rows
- **one test that proves your code rejects bad input** (`Assert.Throws<...>`)

What's worth testing: validation in your setters, calculated properties (like `DisplayName`), anything your store or mapper does beyond the Lab 02 contract. Name each test as a sentence: `Level_outside_1_to_100_is_rejected`, not `Test3`.

## 4. Search your own store (10 min)

Implement `ILab03Wiring` in `src/Capstone.Core/Wiring/`. `Search` returns the items whose `DisplayName` matches the query, using `SearchMatcher.Create()`, in store order. Matching more fields is fine. Then:

```
sq test
git add . && git commit -m "Lab 03: tests" && git push
```

**No git (lab PC)?** `sh sq.sh test`, then `sh sq.sh upload`, then **Add file → Upload files** on your repository's page: drag in what it gathered, **Commit changes**. Step by step: [working on a lab PC](https://github.com/x86bogdan/fcpl-2026/blob/main/LAB-PC.md).

**On time** means green in your repository's **Actions** tab by **Friday 23:59** this week, whichever group you're in. After that it's late work, worth up to 8.

## Done means

See `ACCEPTANCE.md`. In short: `sq test` shows Lab 03 at full marks, including *broken versions caught 8/8*, and CI is green.

## Stretch

**Break your own code.** Change one line in your domain so it's wrong in a way a user would notice. If your tests stay green, you've found a hole: write the test that catches it, then fix the line. Show me the test.

---

**Project route:** today is **checkpoint 1**. Bring `CHECKPOINT-1.md`, filled in.

**Deck:** *Testing & the contract* · **Next session:** OOP & pattern matching.
