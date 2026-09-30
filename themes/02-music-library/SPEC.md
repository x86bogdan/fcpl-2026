# Music Library — full spec

## The pitch

A personal music library. It holds albums and tracks, remembers what you've played, and answers questions about your taste. By January it is a desktop app, a web API and a database — the same tracks underneath all three.

## The domain

At least these, named however you like:

- **Track** — the item. Id, title, artist, genre, duration, year, play count, optional rating
- **Genre** — an `enum`, at least six values
- **Duration** — use `TimeSpan`, not an `int` of seconds. A `record` for something that belongs together (e.g. `Credit(string Artist, string? FeaturedArtist)`)
- **Album** — the parent. Title, artist, release year
- **Your feature** — pick **one** and build it across the semester:
  - *Listening stats* — a year-in-review: top artists, hours per genre, your "decade"
  - *Smart playlists* — rules like "rock, before 2000, rated 4+", evaluated live against the library
  - *Play history* — every play recorded with a time, and "you've played this 100 times" moments

**Data:** at least **40 tracks** on at least **six albums** by at least four artists, with varied genres and years. Write the seed by hand from your own music, or pull album tracklists from MusicBrainz once and save them.

## What each session adds

| S | Required feature | In this theme |
|---|---|---|
| 2 | 3+ types, an enum, an interface, zero nullable warnings | `Track`, `Album`, a record, `Genre`. Rating is nullable — null means "not rated yet", which is not the same as 0 stars. Optionally fetch one album from MusicBrainz with `HttpClient` |
| 3 | 5+ tests over your own domain logic | Test your feature: total duration of an album, a smart-playlist rule, a milestone at exactly 100 plays |
| 4 | Abstract base + 2 kinds, a polymorphic loop, a switch expression | Kinds of audio: e.g. `Song` / `PodcastEpisode` / `LiveRecording`, each describing itself differently. The switch labels by length or age — e.g. a song over 8 minutes is "an epic" |
| 5 | Custom collection with indexer + iterator; a generic type | The library: `library[0]`, `library["trk-042"]`, `foreach`. Your generic type holds *recently played* |
| 6 | An event with 2 subscribers; a function as a parameter | "Track added" — heard by the album's track count and by a log. *Played* and *milestone reached* are the stretch |
| 7 | 5 queries answering non-trivial questions | e.g. most-played artist · hours per genre · tracks never played · longest album · the decade you own most music from |
| 8 | Async save/load that survives a restart | The library to JSON, each kind back as its own kind, **durations and play counts intact** |
| 9 | A window bound to the model | Track list with search, a details panel, total library length updating as you add and delete |
| 10 | 4 endpoints + DI + no secrets in source | `/items` — list, one, add, delete. A friend's app can read what you listen to |
| 11 | A migration + one one-to-many | **Album → Tracks.** A track belongs to one album; deleting an album keeps its tracks as singles |
| 12 | An attribute-driven feature + your own runner | `[Show("Length")]` decides what the details panel lists. Your `[Check]`s test your feature |

## At the end, you can show

1. The window: search an artist, select a track, see its details and album
2. Your feature working — a year-in-review, a smart playlist, or a milestone firing
3. Your five questions, answered on your data
4. The app closed and reopened, play counts intact
5. The API answering in a browser, and refusing a POST without the key

## Stretch ideas

A played event that bumps the count and fires milestones · import a real listening export (Spotify's "download your data" gives JSON) · album art in the window · a smart playlist that updates while you add tracks.

## Watch out for

- **Playlists are many-to-many.** One track can be in many playlists, and S11 asks for one-to-many. Album → Tracks is the graded relationship; playlists are a stretch for after S11
- **MusicBrainz asks for about one request a second** and a proper User-Agent. Fetch once, save, never call it on every start
- **Don't store audio files.** Metadata only
