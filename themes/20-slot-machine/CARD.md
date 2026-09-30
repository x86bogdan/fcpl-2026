# Slot Machine Arcade

**Reels, bets, payouts — and the statistics that show who really wins.**

You build *LuckySpin* for Sam's arcade: three reels, bets in credits, a paytable, and a history of every spin. Then the part that makes it a C# project, not just a toy: *"what does the machine pay back over 10,000 spins?"*, *"what's the longest losing streak?"*, *"how often does the jackpot really land?"*

- **Your items:** spins — the bet, the three symbols, the payout, credits after, free or paid
- **Where the data comes from:** your own machine, with a seeded random generator
- **Something happens when:** a spin completes, or the jackpot lands
- **Parent and children:** a Session (one player's visit) has many spins
- **What you'll look at:** the machine, and a statistics screen beside it

**Good for you if** you want something playful with animation. **Heads up:** a slot machine has thin data, so the statistics screen isn't optional — it's how this theme passes the LINQ and collections rows. Credits only, never money.
