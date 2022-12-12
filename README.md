# SimpleStockMonitorPWA
An app built in Blazor WASM as a PWA.
A simple stock monitor app that only shows crypto currencies in its current version.

## Details
- [AlphaVantage API](https://www.alphavantage.co/documentation/) is used to fetch the stock data.
    - The API responses are cached now for a minute, since the API used is throttled to 5 per minute.
- [Mudblazor](https://mudblazor.com/docs/overview) is used for UI.

## Todos
- Offline support through service-worker.
    - Which should add a cache indirectly?
- Implement the other tabs
- Add more filters
    - Daily, Weekly, Monthly
    - More crypto currencies
    - Toggle between SEK and USD