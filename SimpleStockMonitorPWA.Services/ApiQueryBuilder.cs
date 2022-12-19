using SimpleStockMonitorPWA.Models;

namespace SimpleStockMonitorPWA.Services;

public interface IApiQueryBuilder
{
    IApiQueryBuilder SetInterval(TrendInterval interval);
    IApiQueryBuilder SetSymbol(string symbol);
    IApiQueryBuilder SetMarketCurrency(Currency marketCurrency);
    IApiQueryBuilder SetApiKey(string apiKey);
    string BuildQuery();
}

public class ApiQueryBuilder : IApiQueryBuilder
{
    private string? _function;
    private string? _market;
    private string? _symbol;
    private string? _apiKey;

    public string BuildQuery()
    {
        ValidateAllRequiredFieldsSet();

        return $"query" +
            $"?function={_function}" +
            $"&symbol={_symbol}" +
            $"&market={_market}" +
            $"&apikey={_apiKey}";
    }

    private void ValidateAllRequiredFieldsSet()
    {
        _ = _apiKey ?? throw new Exception("apikey must be set");
        _ = _function ?? throw new Exception("function must be set");
        _ = _symbol ?? throw new Exception("symbol must be set");
        _ = _market ?? throw new Exception("market must be set");
    }

    public IApiQueryBuilder SetInterval(TrendInterval interval)
    {
        _function = GetFunctionFromInterval(interval);
        return this;
    }

    private static string GetFunctionFromInterval(TrendInterval trendInterval)
    {
        return trendInterval switch
        {
            TrendInterval.Daily => "DIGITAL_CURRENCY_DAILY",
            TrendInterval.Weekly => "DIGITAL_CURRENCY_WEEKLY",
            TrendInterval.Monthly => "DIGITAL_CURRENCY_MONTHLY",
            _ => throw new NotImplementedException(),
        };
    }

    public IApiQueryBuilder SetMarketCurrency(Currency marketCurrency)
    {

        _market = GetMarketFromCurrency(marketCurrency);
        return this;
    }

    private static string GetMarketFromCurrency(Currency currency)
    {
        return currency switch
        {
            Currency.USD => "USD",
            Currency.EUR => "EUR",
            Currency.SEK => "SEK",
            _ => throw new NotImplementedException()
        };
    }

    public IApiQueryBuilder SetSymbol(string symbol)
    {
        _symbol = symbol;
        return this;
    }

    public IApiQueryBuilder SetApiKey(string apiKey)
    {
        _apiKey = apiKey;
        return this;
    }
}
