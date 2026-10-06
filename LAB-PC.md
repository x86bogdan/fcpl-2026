# Working on a lab PC

> Treat a lab PC like a hotel room. Take your stuff when you go, and don't leave the key in the door.

**The lab PCs:** Linux, with Rider and .NET 9. **`dotnet` works only in Rider's terminal** (*View → Tool Windows → Terminal*, or **Alt+F12**), not in a terminal window opened from the desktop. So every command on this page goes into Rider's terminal.

## Best to worst

1. **Your own laptop.** Nothing to clean up.
2. **Lab PC, browser only.** No git: download your repository as a ZIP, work in Rider, upload your work through the GitHub website. Sign in in a **private window** and close it when you leave. Step by step below.
3. **Lab PC with git.** Fine, but git stores your password, so you have to clean up (last section).

---

## Browser only, every lab

### At the start (5 minutes)

1. Open a **private window** (Firefox: **Ctrl+Shift+P**, Chrome/Chromium: **Ctrl+Shift+N**) and sign in to GitHub.
2. Your repository → **Code → Download ZIP**.
3. In the file manager, *Downloads* → right-click the ZIP → **Extract Here**.
4. Rider → **Open** → the extracted folder (the one with `SharpQuest.slnx` in it) → **Trust**.
5. Rider's terminal (**Alt+F12**):

   ```
   sh sq.sh update
   sh sq.sh test
   ```

   The first run takes a minute: `sq` builds itself and the packages download.

> **Repository made in session 1?** The first `sh sq.sh` stops with *"A compatible .NET SDK was not found … 10.0.100"*. Your copy still asks for .NET 10. In the same terminal, once:
>
> ```
> curl -fsSL https://raw.githubusercontent.com/x86bogdan/fcpl-2026/main/fix-net9.sh | sh
> ```
>
> then `sh sq.sh test`. The upload at the end saves the fix in your repository, so your next ZIP won't need it. More in [DOTNET-9.md](DOTNET-9.md).

### While you work

Edit in Rider. Run `sh sq.sh test` as often as you like.

### At the end: save your work to GitHub (5 minutes)

1. Rider's terminal:

   ```
   sh sq.sh upload
   ```

   It copies everything that belongs in your repository into one folder and opens that folder for you. Your code is in there, without the build output.

2. In the browser: your repository's main page → **Add file → Upload files**.
3. In the folder that opened, select everything (**Ctrl+A**) and drag it onto the GitHub page. Wait until the file list appears.
4. Type a message (e.g. *Lab 02*), keep **Commit directly to the main branch**, then **Commit changes**.
5. Open the **Actions** tab. A run starts within a minute; a green ✓ is your result, recorded. That's where your Contract score comes from.
6. Clean up: delete the extracted folder and the ZIP, sign out, close the private window.

**Good to know:**

- **Uploading adds and replaces files. It never deletes.** Deleted or renamed a file in Rider? Delete the old one on GitHub too: open it → **⋯ → Delete file**.
- **Upload on the repository's main page**, not inside a folder, or you'll get `src/src/…`.
- GitHub takes **100 files** per upload. If you have more, `sq upload` splits them into `part1`, `part2`…: upload and commit each one.
- Changed a file on the GitHub website during the lab? The upload replaces it with your ZIP copy. Pick one place to edit.

### What to upload, if you do it by hand

`sq upload` does this for you. If it can't run:

| Upload | Why |
|---|---|
| **`src/`** | Your code. **Without the `bin/` and `obj/` folders** inside each project: delete those first. |
| **`tests/`** | Your tests, also without `bin/` and `obj/` |
| `SharpQuest.slnx` | Only if you added a project (Lab 09's window, Lab 10's API) |
| `Directory.Packages.local.props` | Only if you created it (Lab 09) |
| `README.md` | Only if you changed it |
| `global.json`, `Directory.Build.props`, `Directory.Build.targets`, `Directory.Packages.props`, `sq.cs`, `sq.cmd`, `sq.sh` and the **`tools/`** folder | Once, if your repository was made in session 1: it keeps your next ZIP building on a lab PC |

**Never upload** `bin/`, `obj/`, `.sq/`, `*.db` files, `.env` or `http-client.private.env.json`: build output, data and secrets. **No need to upload** `contracts/`, `reference/` or `catchup/`. GitHub uses the course's own copies when it grades.

---

## Before you leave a lab PC where you used git

- [ ] **Remove the stored credential.**
  Linux: check `~/.git-credentials` and your keyring.
  Windows: *Control Panel → Credential Manager → Windows Credentials* → the `git:https://github.com` entry → **Remove**.
- [ ] **Delete your working folder.** Your code is in your repository. The copy on that disk is just a copy.
- [ ] **Sign out of GitHub in the browser**, and close it.
- [ ] **Created a Personal Access Token on that machine? Revoke it** at *github.com → Settings → Developer settings → Personal access tokens*. A token you leave behind is a password you left behind.

The two habits that cover most of it: **private window**, and **delete your folder**.
