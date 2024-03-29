# SimpleStockMonitorPWA
An app built in Blazor WASM as a PWA.
A simple stock monitor app that only shows crypto currencies in its current version.

## Details
- [AlphaVantage API](https://www.alphavantage.co/documentation/) is used to fetch the stock data.
    - The API responses are cached now for 5 minutes, since the API is throttled to 5 calls per minute and 500 per day.
- [Mudblazor](https://mudblazor.com/docs/overview) is used for UI.
- [bUnit](https://bunit.dev/docs/getting-started/index.html) is used for Blazor component tests.
- The app is hosted in Azure to easier test it on mobile. Link can be found [here](https://simplestockmonitorpwaapp20221127224909.azurewebsites.net/)

## Todos
- Offline support through service-worker.
    - Which should add a cache indirectly?
- Implement the other tabs
- Add more filters
    - Daily, Weekly, Monthly
    - More crypto currencies
    - Toggle between SEK and USD