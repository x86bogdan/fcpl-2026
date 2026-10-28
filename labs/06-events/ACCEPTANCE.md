# Lab 06 — Done means

## Automatic (Contract, 40%)

`sq test` shows:

```
 OK   Lab 02       36/36    contract ≈ 4.0 / 4
 OK   Lab 03       26/26    contract ≈ 4.0 / 4
 OK   Lab 04       41/41    contract ≈ 4.0 / 4
 OK   Lab 05       38/38    contract ≈ 4.0 / 4
 OK   Lab 06       28/28    contract ≈ 4.0 / 4
```

…and the latest run in your repository's **Actions** tab is green. Only the CI run counts.

What the 28 checks cover:

| Group | Checks |
|---|---|
| The event (13) | the publisher declares a public, non-static `event` · adding tells the subscriber, once, with that item · two subscribers both hear it, in subscription order · the store is already updated when a subscriber runs · unsubscribing stops that one and leaves the others · unsubscribing everyone is safe · nobody subscribed is not a crash · the same handler twice is called twice, and one `-=` leaves one · a rejected duplicate announces nothing · a rejected null announces nothing · every added item is announced once, in order · one store's subscribers don't hear another store |
| Functions as parameters (15) | `Where` keeps what the function says yes to · always-no keeps nothing · always-yes keeps everything in store order · the function is asked about each item exactly once · an empty store asks nothing · `Where` doesn't change the store · `Map` uses the function for every item, in order · `Map` may return the same text twice · `Map` on an empty store · `ForEach` runs in order · `ForEach` on an empty store · a lambda sees the variables around it · a null function throws `ArgumentNullException` (×3) |

## By hand (you'll be asked to show these)

- [ ] Your two subscribers, and the fact that neither knows about the other
- [ ] Where `?.Invoke` is, and what happens without it
- [ ] Your `Subscribe`, and why the returned function can still find `handler` after `Subscribe` has returned
- [ ] *(stretch)* the second event, or the `IDisposable` subscription

## Example output (Pokédex reference)

```
──────────── SharpQuest ────────────
 OK   Lab 02       36/36    contract ≈ 4.0 / 4
 OK   Lab 03       26/26    contract ≈ 4.0 / 4
 OK   Lab 04       41/41    contract ≈ 4.0 / 4
 OK   Lab 05       38/38    contract ≈ 4.0 / 4
 OK   Lab 06       28/28    contract ≈ 4.0 / 4
 OK   Your tests   54/54
────────────────────────────────────
```

```
dotnet run --project src/Capstone.App
6 entries announced while loading; last few: Pikachu, Squirtle, Charmander
6 Pokémon loaded:
  …
```
