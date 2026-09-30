# Working on a lab PC

> Treat a lab PC like a hotel room. Take your stuff when you go, and don't leave the key in the door.

## Best to worst

1. **Your own laptop.** Nothing to clean up.
2. **Lab PC, browser only.** Sign in to GitHub in a **private/incognito window**, work in the browser, close the window. Closing it ends the session.
3. **Lab PC with git.** Fine, but git stores your password, so you have to clean up.

## Before you leave a lab PC where you used git

- [ ] **Remove the stored credential.**
  Windows: *Control Panel → Credential Manager → Windows Credentials* → the `git:https://github.com` entry → **Remove**.
  Linux: check `~/.git-credentials` and your keyring.
- [ ] **Delete your working folder.** Your code is in your repository. The copy on that disk is just a copy.
- [ ] **Sign out of GitHub in the browser**, and close it.
- [ ] **Created a Personal Access Token on that machine? Revoke it** at *github.com → Settings → Developer settings → Personal access tokens*. A token you leave behind is a password you left behind.

The two habits that cover most of it: **incognito window**, and **delete your folder**.
