# Portfolio Tracker

**Every buy and sell, what you hold now, and whether you're up or down.**

You build a portfolio tracker for pretend money: every transaction recorded, holdings worked out from them, and live prices to tell you where you stand. Then the questions: *"what's my profit on each coin?"*, *"how much of my money is in one asset?"*, *"what did I pay on average?"*

- **Your items:** transactions — buy, sell or dividend; asset, quantity, price, date, fee
- **Where the data comes from:** your own invented trades. Live prices from [CoinGecko](https://docs.coingecko.com) (free, with a demo key) — or a price file you write
- **Something happens when:** a transaction is recorded, or a price crosses your alert
- **Parent and children:** a Portfolio (an account) has many transactions
- **What you'll look at:** your holdings with profit and loss, and the transactions behind them

**Good for you if** you like numbers that must be exactly right, and want `async` that's really needed. Pretend money only — this is not investing advice.
