# Lab 08 — Done means

## Automatic (Contract, 40%)

`sq test` shows:

```
 OK   Lab 02       36/36    contract ≈ 4.0 / 4
 OK   Lab 03       26/26    contract ≈ 4.0 / 4
 OK   Lab 04       41/41    contract ≈ 4.0 / 4
 OK   Lab 05       38/38    contract ≈ 4.0 / 4
 OK   Lab 06       28/28    contract ≈ 4.0 / 4
 OK   Lab 07       47/47    contract ≈ 4.0 / 4
 OK   Lab 08       38/38    contract ≈ 4.0 / 4
```

…and the latest run in your repository's **Actions** tab is green. Only the CI run counts.

What the 38 checks cover:

| Group | Checks |
|---|---|
| The round trip (14) | everything saved comes back · in the order it was saved · **every value survives, property by property** · an empty store · a store of one · saving again replaces rather than appends · the file is a snapshot, not a live view · what comes back is a working store (Lab 02's rules still apply) · loading twice gives two separate stores · a file written "before the restart" reads afterwards · 50 items · an empty store saved over a full file empties it · saving doesn't change the store · the file isn't empty |
| Your kinds (4) | **each kind comes back as its own kind** · each keeps its own values · a mix keeps its order · the mix is still the same mix |
| Files (14) | a file that isn't there is an empty store · loading doesn't create it · a folder that isn't there is the same · saving makes the folder it needs · nonsense is `InvalidDataException` · so is valid JSON of the wrong shape · so is half a file · a null store, a null path and a blank path are rejected, for both methods |
| Really asynchronous (6) | save and load each reach a real `…Async` call in `System.IO` or `System.Text.Json` · neither blocks on a task with `.Result` or `.Wait()` · neither routes through the catch-up kit |

The last group is read from your compiled code, not from what it returns. A save that does synchronous I/O and hands back a finished `Task` satisfies the interface and fails those checks.

## By hand (you'll be asked to show these)

- [ ] The line where your save decides *what shape* to write, and why it isn't the store object
- [ ] Where a kind is recorded in the file, and what happens to a file that doesn't record it
- [ ] An `await` in your code, and what the thread is doing while it waits
- [ ] The difference between your missing-file path and your corrupt-file path
- [ ] *(stretch)* the write-to-temp-then-move, or a second format behind the same two methods

## Example output (Pokédex reference)

```
──────────── SharpQuest ────────────
 OK   Lab 02       36/36    contract ≈ 4.0 / 4
 OK   Lab 03       26/26    contract ≈ 4.0 / 4
 OK   Lab 04       41/41    contract ≈ 4.0 / 4
 OK   Lab 05       38/38    contract ≈ 4.0 / 4
 OK   Lab 06       28/28    contract ≈ 4.0 / 4
 OK   Lab 07       47/47    contract ≈ 4.0 / 4
 OK   Lab 08       38/38    contract ≈ 4.0 / 4
 OK   Your tests   70/70
────────────────────────────────────
```

```
dotnet run --project src/Capstone.App -- --save
Saved 4 entries to saved-pokedex.json.
Loaded 4 entries back:
  #pkmn-001 Bulbasaur · Grass · Lv 5 · wild, in the tall grass
  #pkmn-004 Ember the Charmander · Fire · Lv 7 · wild, in the tall grass
  #pkmn-006 Charizard · Fire/Dragon · Lv 36 · wild, in the tall grass
  #pkmn-025 Pikachu · Electric · Lv 12 · wild, in the tall grass · owned by Ash
```

Run it again with `--load` and nothing else: the second run knows only what's in the file. The nickname, the level, the second type and the trainer are all still there — which is the whole lab in one line of output.
