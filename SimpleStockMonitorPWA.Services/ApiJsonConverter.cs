using SimpleStockMonitorPWA.Models;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SimpleStockMonitorPWA.Services;

public class ApiJsonConverter
{
    private string _responseString;
    private readonly TrendInterval _trendInterval;

    public ApiJsonConverter(string jsonResponseString, TrendInterval trendInterval)
    {
        _responseString = jsonResponseString;
        _trendInterval = trendInterval;
    }

    public IEnumerable<CryptoTrendValuesContainer> ConvertToCryptoTrends()
    {
        try
        {
            var timeSeries = ParseTimeSeriesInResponse();
            var cryptoTrends = timeSeries
                .Select(kvp => new CryptoTrendValuesContainer(kvp.Key, ConvertToCryptoTrend(kvp.Value)));
            return cryptoTrends;
        }
        catch (Exception e)
        {
            throw new Exception("Failed to convert response to crypto trends.", e);
        }
    }

    private Dictionary<DateOnly, Dictionary<string, string>> ParseTimeSeriesInResponse()
    {
        var rootConverted = JsonSerializer.Deserialize<CryptoTrendRootResponseObject>(_responseString);
        return _trendInterval switch
        {
            TrendInterval.Daily => rootConverted!.DailyTimeSeries!,
            TrendInterval.Weekly => rootConverted!.WeeklyTimeSeries!,
            TrendInterval.Monthly => rootConverted!.MonthlyTimeSeries!,
            _ => throw new NotImplementedException(),
        };
    }

    private CryptoValues ConvertToCryptoTrend(Dictionary<string, string> dictionary)
    {
        var cryptoTrendBuilder = new CryptoValues.Builder();
        foreach (var key in dictionary.Keys)
        {
            var value = double.Parse(dictionary[key], CultureInfo.InvariantCulture);

            if (key.StartsWith("1a."))
                cryptoTrendBuilder.AddOpenOther(value);
            if (key.StartsWith("1b."))
                cryptoTrendBuilder.AddOpenUSD(value);
            if (key.StartsWith("2a."))
                cryptoTrendBuilder.AddHighOther(value);
            if (key.StartsWith("2b."))
                cryptoTrendBuilder.AddHighUSD(value);
            if (key.StartsWith("3a."))
                cryptoTrendBuilder.AddLowOther(value);
            if (key.StartsWith("3b."))
                cryptoTrendBuilder.AddLowUSD(value);
            if (key.StartsWith("4a."))
                cryptoTrendBuilder.AddCloseOther(value);
            if (key.StartsWith("4b."))
                cryptoTrendBuilder.AddCloseUSD(value);
            if (key.StartsWith("5."))
                cryptoTrendBuilder.AddVolume(value);
            if (key.StartsWith("6."))
                cryptoTrendBuilder.AddMarketCapUSD(value);
        }
        return cryptoTrendBuilder.Build();
    }

    /// <summary>
    /// One of the TimeSeries fields are set in the response root depending on the trend interval
    /// </summary>
    private class CryptoTrendRootResponseObject
    {
        [JsonPropertyName("Time Series (Digital Currency Daily)")]
        public Dictionary<DateOnly, Dictionary<string, string>>? DailyTimeSeries { get; set; }

        [JsonPropertyName("Time Series (Digital Currency Weekly)")]
        public Dictionary<DateOnly, Dictionary<string, string>>? WeeklyTimeSeries { get; set; }

        [JsonPropertyName("Time Series (Digital Currency Monthly)")]
        public Dictionary<DateOnly, Dictionary<string, string>>? MonthlyTimeSeries { get; set; }
    }
}
