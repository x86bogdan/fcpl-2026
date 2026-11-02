# Lab 09 — Done means

## Automatic (Contract, 40%)

`sq test` shows:

```
 OK   Lab 02       36/36    contract ≈ 4.0 / 4
 …
 OK   Lab 08       38/38    contract ≈ 4.0 / 4
 OK   Lab 09       43/43    contract ≈ 4.0 / 4
```

…and the latest run in your repository's **Actions** tab is green. Only the CI run counts.

What the 43 checks cover — all of them on your **view model**, none on your window:

| Group | Checks |
|---|---|
| The list (11) | starts with what the store holds, in order · **is observable** · **is the same object every time** · **something added to the store appears in it** · announces exactly one Add, not a Reset · isn't replaced when an item arrives · Count matches · Count announces itself · an empty store · two view models share nothing · store order is kept |
| Telling the window (9) | a real `PropertyChanged` event · setting Filter announces it · setting Selected announces it · Details follow the selection · **and announce themselves** · setting Filter to what it already is announces nothing · same for Selected · Details are never blank · nothing can be selected again |
| The search box (9) | narrows the list · ignores case · blank shows everything · no match shows nothing · clearing restores · **filtering doesn't touch the store** · doesn't replace the collection · a null filter is a blank one · an arrival while filtered obeys the filter |
| The buttons (11) | the commands are the same objects every time · Add is always available · Add goes through the store · and reaches the list · **Delete is greyed out with nothing selected** · available when something is · **the button is told to ask again** · Delete removes from the store · and from the list · and clears the selection · Delete with nothing selected does nothing quietly |
| MVVM (3) | the view model lives in `Capstone.Core` · **your domain library has never heard of a Button** · the view model never blocks on a task |

## By hand (this is where the window is graded)

- [ ] **The window runs, and shows your items.** This is required feature row 9
- [ ] Type in the search box — the list narrows as you type
- [ ] Select a row — the details panel fills in, the Delete button comes alive
- [ ] Delete it — the row goes, the count drops, the details panel says nothing is selected
- [ ] Your code-behind file, and how little is in it
- [ ] *(stretch)* saving from the window, or a sortable column

## Example (Pokédex reference)

The window: a search box and a Delete button across the top, the list on the left, the details panel on the right, "4 showing" along the bottom. `App.axaml.cs` loads the Lab 08 file before the window opens, so it starts with whatever was saved last time — and an empty Pokédex the very first run, which is exactly what Lab 08's missing-file rule was for.

`MainWindow.axaml.cs` is four lines, and two of them are braces.
