# .NET 9 or .NET 10: making your repository build on both

**The lab PCs have .NET 9** (Linux, inside Rider: run every command in **Rider's terminal**, **Alt+F12**). Your laptop may have .NET 9 or .NET 10. Both are supported: the course builds everything for **.NET 9 with C# 13**, which a .NET 10 SDK builds just as well.

Repositories created **from session 2 on** are already set up this way. Nothing to do.

**Created your repository in session 1?** It still asks for .NET 10. On your own laptop with .NET 10 that doesn't matter: `sq update` fixes it. On a lab PC, `sq` and every `dotnet` command in that folder stop with:

```
A compatible .NET SDK was not found.
Requested SDK version: 10.0.100
```

Fix it once with **one** of the three ways below, in this order of preference, then save it to GitHub (last step of each way).

---

## 1. One command (recommended)

In your repository folder (the one with `SharpQuest.slnx`):

**Lab PC, Linux or macOS** (on a lab PC: Rider's terminal):

```
curl -fsSL https://raw.githubusercontent.com/x86bogdan/fcpl-2026/main/fix-net9.sh | sh
```

**Windows**, in PowerShell:

```
irm https://raw.githubusercontent.com/x86bogdan/fcpl-2026/main/fix-net9.ps1 | iex
```

It downloads the course's official files and puts them in place, exactly what `sq update` does, and touches nothing you wrote. Then `sh sq.sh test` (Windows: `.\sq test`), and save it:

- **with git:** `git add . && git commit -m "Build on .NET 9" && git push`
- **without git** (a lab PC, browser only): `sh sq.sh upload`, then upload what it gathered. The steps are in [LAB-PC.md](LAB-PC.md).

## 2. Copy the files by hand

For when the command can't download (no `curl`, a blocked site).

1. Open the course template, <https://github.com/x86bogdan/sharpquest-2026>, then **Code → Download ZIP**, and extract it somewhere.
2. Copy these from the extracted folder **into your repository folder**, replacing what's there:

   | Copy | What it is |
   |---|---|
   | `global.json` | which SDK to use: 9 or newer |
   | `Directory.Build.props` | .NET 9, C# 13 |
   | `Directory.Packages.props` | package versions for .NET 9 |
   | `Directory.Build.targets` | the build rules |
   | `sq.cs`, `sq.cmd`, `sq.sh` | the `sq` helper |
   | the whole `tools/` folder | how `sq` gets built on .NET 9 |
   | the whole `contracts/` folder | the tests |
   | `.config/dotnet-tools.json` | tool versions, needed from session 11 (a hidden folder: **Ctrl+H** shows it in the Linux file manager) |

3. Run `sh sq.sh test` (Windows: `.\sq test`), then save it to GitHub as in way 1. Without git, upload the files from this table except `contracts/`, or just use `sh sq.sh upload`.

**Don't copy** `src/`, `tests/`, `README.md` or `sharpquest.json`. Those are yours.

## 3. Last resort: edit three files

Only if neither of the above works. Open each file in Rider and change exactly these lines.

**`global.json`**: the version and the roll-forward rule:

```json
{
  "sdk": {
    "version": "9.0.100",
    "rollForward": "latestMajor"
  }
}
```

**`Directory.Build.props`**: the framework and the C# version, plus two new lines:

```xml
<TargetFramework>net9.0</TargetFramework>      <!-- was net10.0 -->
<LangVersion>13</LangVersion>                   <!-- was 14 -->
<RollForward>Major</RollForward>                <!-- new: lets it run on a PC that only has .NET 10 -->
<PublishAot>false</PublishAot>                  <!-- new -->
```

**`Directory.Packages.props`**: the three lines that say `10.0.12` become `9.0.20`:

```xml
<PackageVersion Include="Microsoft.EntityFrameworkCore.Sqlite" Version="9.0.20" />
<PackageVersion Include="Microsoft.EntityFrameworkCore.Design" Version="9.0.20" />
<PackageVersion Include="Microsoft.Data.Sqlite" Version="9.0.20" />
```

The framework is set **once**, in `Directory.Build.props`, for every project. Your `.csproj` files don't name one, so leave them alone. **If one of your `.csproj` files does have a `<TargetFramework>` line** (Visual Studio, Rider or `dotnet new` adds one when you create a project), delete that line, or change `net10.0` to `net9.0` in it.

`sq` itself won't start after this edit on .NET 9 (it needs the `tools/` folder from way 1 or 2), so run the tests directly:

```
dotnet test contracts/SharpQuest.Contracts.Tests
dotnet test tests/Capstone.Tests
```

Then save it to GitHub as in way 1, and do way 1 or 2 the next time you can: on your laptop, `sq update` does it too.

---

## Writing code that works on both

- **C# 13 is the limit, on every machine.** Newer C# features (C# 14's `field` keyword, `?.` on the left of `=`, `extension` blocks) are rejected by the build even on a .NET 10 laptop: `field` with *"The name 'field' does not exist in the current context"*, the others with *"Feature '…' is not available in C# 13.0"*. That's deliberate: code that builds on your laptop must also build on a lab PC.
- **The GitHub check builds the same way**, for .NET 9 with C# 13. Green there means it builds on a lab PC.
- **New project?** On a .NET 10 laptop, add `-f net9.0` to `dotnet new` (or delete the `<TargetFramework>` line it writes).
- **A laptop with only .NET 10 installed** runs everything on .NET 10 automatically. The first build downloads a few .NET 9 reference files from NuGet (once, about a minute).
