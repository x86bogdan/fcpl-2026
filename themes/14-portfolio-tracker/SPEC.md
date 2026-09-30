# Portfolio Tracker — full spec

## The pitch

A tracker for a pretend portfolio. You record transactions; the app works out what you hold, what you paid, and what it's worth now. By January it is a desktop app, a web API and a database — the same transactions underneath all three. **Invented money only.**

## The domain

At least these, named however you like:

- **Transaction** — the item. Id, asset symbol, date, quantity, price per unit, fee
- **Asset class** — an `enum`: Crypto, Stock, ETF, Cash
- **Money** — a `record` of amount and currency; `decimal`, always
- **Portfolio** — the parent: an account. Name, base currency, opened date
- **Holdings are computed, never stored.** They are what the transactions add up to
- **Your feature** — pick **one** and build it across the semester:
  - *Profit and loss* — average cost per asset, unrealised gain now, realised gain on sells
  - *Allocation* — what share of the portfolio each asset and class is, and a warning when one is too big
  - *Price alerts* — thresholds per asset, checked when prices refresh

**Data:** at least **40 transactions** across at least **eight assets** and **two portfolios**, over several months, including sells and at least one dividend.

## What each session adds

| S | Required feature | In this theme |
|---|---|---|
| 2 | 3+ types, an enum, an interface, zero nullable warnings | `Transaction`, `Portfolio`, `Money` record, `AssetClass`. A fee is nullable — "not recorded" is different from zero. Optionally fetch current prices from CoinGecko with `HttpClient` |
| 3 | 5+ tests over your own domain logic | Average cost after two buys, a sell reduces the holding, selling more than you hold is refused, fees included, rounding to the cent |
| 4 | Abstract base + 2 kinds, a polymorphic loop, a switch expression | `Buy` / `Sell` / `Dividend`, each describing itself and each saying how it changes a holding (+, −, cash only). The switch labels — a buy over 1,000 is "large", a sell at a loss is "cut losses" |
| 5 | Custom collection with indexer + iterator; a generic type | The ledger: `ledger[0]`, `ledger["tx-0031"]`, `foreach`. Your generic type holds *recently viewed assets* |
| 6 | An event with 2 subscribers; a function as a parameter | "Transaction recorded" — heard by the holdings view and by a log. *Price alert* is the stretch |
| 7 | 5 queries answering non-trivial questions | e.g. **holdings** (`GroupBy` asset → net quantity) · average cost per asset · allocation by class · dividends per year · the best and worst trade |
| 8 | Async save/load that survives a restart | The ledger to JSON, each kind back as its own kind, **every amount exact to the smallest unit** |
| 9 | A window bound to the model | Holdings with profit/loss, a search box, the transactions behind the selected asset |
| 10 | 4 endpoints + DI + no secrets in source | `/items` — list, one, add, delete. **Your CoinGecko key is the second secret** — it follows the same rules as the API key |
| 11 | A migration + one one-to-many | **Portfolio → Transactions.** A transaction is in one portfolio; deleting a portfolio keeps its transactions |
| 12 | An attribute-driven feature + your own runner | `[Show("Price")]` decides what the details panel lists. Your `[Check]`s test your maths |

## At the end, you can show

1. The window: your holdings, select one, see its transactions and profit
2. Your feature working — P&L on a real price, an allocation warning, or an alert firing
3. Your five questions, answered on your data
4. The app closed and reopened, every amount identical
5. The API answering in a browser, and refusing a POST without the key

## Stretch ideas

A price-alert event · refreshing prices in the background while the window stays responsive · currency conversion for a second base currency · a CSV import in your broker's format.

## Watch out for

- **CoinGecko needs a free demo key** sent as a header. Keep it in an environment variable from day one; the labs and CI run without it, from saved prices
- **Crypto quantities have many decimals** (0.00012345 BTC). `decimal` holds them; `double` loses them
- **Holdings are not a table.** If you store "I have 2.5 ETH" next to the transactions, one day they'll disagree
- **Pretend money.** Nothing here connects to a real account
