using Microsoft.Extensions.Options;
using SimpleStockMonitorPWA.Models;

namespace SimpleStockMonitorPWA.Services;

public interface IApiService
{
     Task<IEnumerable<CryptoTrendValuesContainer>> GetCryptoCurrencyTrendAsync(
         string cryptoCurrencyCode, TrendInterval trendInterval, Currency marketCurrency);
}

public class ApiService : IApiService
{
    private readonly HttpClient _httpClient;
    private readonly IApiQueryBuilder _queryBuilder;
    private readonly PersistentStateService _cache;
    private readonly AlphaVantageOptions _alphaVantageOptions;

    public ApiService(
        HttpClient httpClient,
        IApiQueryBuilder queryBuilder,
        IOptions<AlphaVantageOptions> options,
        PersistentStateService cache)
    {
        _httpClient = httpClient;
        _queryBuilder = queryBuilder;
        _cache = cache;
        _alphaVantageOptions = options.Value;
    }

    public async Task<IEnumerable<CryptoTrendValuesContainer>> GetCryptoCurrencyTrendAsync(
        string cryptoCurrencyCode, TrendInterval trendInterval, Currency marketCurrency)
    {
        var query = _queryBuilder
            .SetApiKey(_alphaVantageOptions.ApiKey!)
            .SetInterval(trendInterval)
            .SetSymbol(cryptoCurrencyCode)
            .SetMarketCurrency(marketCurrency)
            .BuildQuery();

        var responseString = await _cache.GetOrCreateAsync(query, () => CallApi(query));

        return new ApiJsonConverter(responseString, trendInterval).ConvertToCryptoTrends();
    }

    private async Task<string> CallApi(string query) => await _httpClient.GetStringAsync(query);
}