# Lab 02 — Types & modelling

**Today:** your capstone gets a real domain model. You leave with types that describe your world, a store that holds them, and JSON going in and out.

**Required feature (matrix row 2):** 3+ types of your own · 1 enum · 1 interface · zero nullable warnings

> **In your project:** open your theme's spec ([the 21 themes](https://github.com/x86bogdan/fcpl-2026/blob/main/themes/README.md)) → *What each session adds* → row **2** (your own idea: the required feature above, said for your app). Before you write code, put three lines in your README: which of your classes change today, what they do in your app, and where you'll see it working.

---

## 0. Get your repository onto this machine (15 min)

**On a lab PC (browser only, no git):** your repository → **Code → Download ZIP** → extract it → open the folder in Rider. Then, in **Rider's terminal** (**Alt+F12**; `dotnet` only works there on the lab PCs):

```
sh sq.sh update
sh sq.sh test
```

**On your laptop, with git:**

```
git clone <your repository URL>
cd <your repository>
sq update          (Linux/macOS: sh sq.sh update)
sq test
```

`sq test` shows **`Not started: Lab 02`**. That's success: you haven't written anything yet.

> **Made your repository in session 1, and you're on a lab PC?** `sq` stops with *"A compatible .NET SDK was not found … 10.0.100"*. The lab PCs have .NET 9. In Rider's terminal, once:
>
> ```
> curl -fsSL https://raw.githubusercontent.com/x86bogdan/fcpl-2026/main/fix-net9.sh | sh
> ```
>
> then `sh sq.sh test`. Other ways, and the Windows command: [.NET 9 or .NET 10](https://github.com/x86bogdan/fcpl-2026/blob/main/DOTNET-9.md).

**Saving your work without git:** at the end, `sh sq.sh upload`, then **Add file → Upload files** on your repository's page. Step by step, and exactly which files: [working on a lab PC](https://github.com/x86bogdan/fcpl-2026/blob/main/LAB-PC.md).

## 1. Model your world (35 min)

In `src/Capstone.Core/`, write the types your capstone needs. At least:

- **one type implementing `IItem`**: the thing your app is about. Read `contracts/SharpQuest.Contracts/Lab02/Lab02.cs` first.
- **one enum** for a property with a fixed set of values
- **one interface of your own**: a capability some of your types share
- **one record**, most naturally the DTO in step 3

Use `required` and `init` for things that must exist and never change. To validate a value, give the property a private backing field and check in the setter (`init => _id = … ? throw … : value;`). Make a property nullable (`string?`) only if "no value" is a real state in your domain.

The build uses **C# 13** on every machine, so newer features (C# 14's `field` keyword, for one) don't compile here, even on a .NET 10 laptop.

## 2. A store (15 min)

Implement `IItemStore`. The doc comments in the contract spell out every rule (duplicates, ordering, snapshots). Most of the tests exist to check those rules.

## 3. JSON in and out (15 min)

Implement `IItemMapper` with `System.Text.Json`. Deserialize into a **DTO record** whose properties are all nullable, validate it, and only then build your real type. Bad input returns `null`. It never throws.

## 4. Wire it up (5 min)

Add one class to `src/Capstone.Core/Wiring/` implementing `ILab02Wiring` (see the README there). Then:

```
sq test
git add . && git commit -m "Lab 02: domain model" && git push
```

No git (lab PC)? `sh sq.sh upload`, then **Add file → Upload files** on your repository's page, drag in what it gathered, **Commit changes**.

Open your repository's **Actions** tab. A green run is your Contract score.

## Done means

See `ACCEPTANCE.md`. In short: `sq test` is green, CI is green, and `src/Capstone.App` loads some of your items from a JSON file and prints them.

## Stretch

Get real data from a public web API that fits your theme, using `HttpClient` *properly*. Finding out what "properly" means is part of the task.

---

**Deck:** *Types & modelling* · **Reference solution:** shown after the lab · **Next session:** Testing, and you'll read the tests that just graded you.
