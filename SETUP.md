# Setup: .NET and GitHub

**Two things to have before session 1.** Both are quick, and doing them at home saves the slowest twenty minutes of the evening.

1. **A GitHub account.** See Part 2. Three minutes.
2. **The .NET SDK.** The course uses **.NET 10**. Session 1 works on **.NET 8 or newer**.

---

## Part 1: .NET

**Goal: one command works.** Open a terminal, run

```
dotnet --version
```

and see a number starting with `10.` (or `8.` or `9.`, which is fine for session 1).

That's it. Everything else we do in the lab.

> If it fights you, stop and bring the laptop. Don't lose an hour to it.

### Windows · Visual Studio

1. Install **Visual Studio 2026** (Community is free) with the **".NET desktop development"** and **"ASP.NET and web development"** workloads. That includes the .NET 10 SDK.
2. Or just the SDK: <https://dotnet.microsoft.com/download/dotnet/10.0> → "SDK x64" installer.
3. Open a **new** terminal and run `dotnet --version`.

### Linux · JetBrains Rider

1. Install the SDK. On Ubuntu:

   ```
   sudo apt update
   sudo apt install dotnet-sdk-10.0
   ```

   If your distribution doesn't have 10 yet, use the official instructions at <https://dotnet.microsoft.com/download/dotnet/10.0>.
2. Install **Rider** (free for non-commercial use). It finds the SDK automatically.
3. Run `dotnet --version`.

### Either OS · VS Code

1. Install the .NET 10 SDK as above.
2. Install **VS Code**, then the **C# Dev Kit** extension.
3. Run `dotnet --version` in the integrated terminal.

### Installing without admin rights

Installs into your own home folder, with no elevation.

**Windows (PowerShell):**

```
Invoke-WebRequest https://dot.net/v1/dotnet-install.ps1 -OutFile dotnet-install.ps1
./dotnet-install.ps1 -Channel 10.0
```

**Linux / macOS (bash):**

```
curl -sSL https://dot.net/v1/dotnet-install.sh | bash -s -- --channel 10.0
```

Then add the install folder to your `PATH` for that terminal. The script prints where it put things.

---

## Part 2: a GitHub account

It needs an email confirmation, which is exactly what goes wrong when fifty people do it at once on the lab wifi.

1. Go to <https://github.com> and sign up.
2. Confirm your email address.
3. That's it. You don't need to create a repository, install git, or know what a commit is. We do that in the lab, from the browser.

**Already have an account?** Use it.

**Pick a username you'd put on a CV.** This account will probably outlive the course.

---

## If it doesn't work

Bring it to session 1. Time is set aside for exactly this, and nobody is left sitting it out.
