using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using SimpleStockMonitorPWA.App;
using SimpleStockMonitorPWA.Models;
using SimpleStockMonitorPWA.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMudServices();

builder.Services.Configure<AlphaVantageOptions>(builder.Configuration.GetSection("AlphaVantage"));
builder.Services.AddHttpClient<IApiService, ApiService>(client =>
{
    var url = builder.Configuration.GetValue<string>("AlphaVantage:Url");
    client.BaseAddress = new Uri(url!);
});

// Register simple services
builder.Services.AddSingleton<IApiQueryBuilder, ApiQueryBuilder>();
builder.Services.AddSingleton<IChartOptionsService, ChartOptionsService>();
builder.Services.AddSingleton<ICurrencyFormattingService, CurrencyFormattingService>();
builder.Services.AddSingleton<ICryptoTrendValueService, CryptoTrendValueService>();
builder.Services.AddSingleton(new PersistentStateService(new Dictionary<string, (DateTime, object)>()));

await builder.Build().RunAsync();
