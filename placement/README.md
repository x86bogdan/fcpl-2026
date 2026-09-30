# Placement task

**20 minutes. Not graded. Nobody finishes.**

This tells you which route fits you. That's all it's for.

## Run it

```
cd placement
dotnet run
```

You'll see eleven checks, all marked `TODO`.

It works with **.NET 8 or newer**. There's nothing to change for your version.

## Do it

Open **`Tasks.cs`**. It's the only file you need to touch. Replace each
`throw new NotImplementedException();` with a real implementation, then
run `dotnet run` again to see how you're doing.

The tasks get harder as you go down the file. Get as far as you get.

## What it means

The program tells you at the end. Roughly:

| Checks passing | What it suggests |
|---|---|
| 1–3 | Guided route: you'll be glad of the scaffolding |
| 4–5 | Guided route: you have the basics, and the labs fill in the rest |
| 6–8 | Either route works |
| 9–11 | The project route is worth a conversation |

It's advice. You choose your own route, and you can change it until session 3.

## If `dotnet run` fails

Put your hand up. Don't lose the twenty minutes to it: you can work next to someone
whose machine works, and we'll fix yours afterwards.

## Notes

- No packages, no internet. If `dotnet run` works, your setup is correct.
- `Check.cs` is a forty-line stand-in for a real test framework. You'll meet
  the real one (xUnit) in session 3, and build one of your own in session 12.
