# Build a Game

**A game of your choice — as long as it runs on data.**

You build a game: a roguelike, a deck-builder, a tower defence, a metroidvania, an RPG with an inventory, a management sim. It has to meet the same requirements as every other theme, which means the game needs *content* — enemies, items, cards, towers, rooms — with enough variety to query, save, list and serve.

- **Your items:** your game's content — at least 20 things with at least 4 attributes each
- **Where the data comes from:** you design it, in a JSON file
- **Something happens when:** an item is acquired, a level goes up, a wave is cleared
- **Parent and children:** a Level has enemies, a Player has an inventory, a Deck has cards
- **Two ways to build it:** in plain C# (console, MonoGame or a window) with the course template, or **in Unity** with the game track's own template

**Good for you if** you'd build a game anyway. **Heads up:** Pong, Snake, Flappy Bird and Breakout don't qualify — no content to work with. In Unity, your game's rules still live in plain C#, in a folder of their own; that's what gets graded. Following a tutorial is fine; you'll be asked to change what it produced. Read [the full spec](SPEC.md) before you choose.
