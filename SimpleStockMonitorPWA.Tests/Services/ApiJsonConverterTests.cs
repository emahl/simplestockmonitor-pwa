namespace SimpleStockMonitorPWA.Tests.Services;

public class ApiJsonConverterTests
{
    [TestCase(TrendInterval.Daily)]
    [TestCase(TrendInterval.Weekly)]
    [TestCase(TrendInterval.Monthly)]
    public void Should_convert_time_interval_json_to_crypto_trend_model(TrendInterval interval)
    {
        // Arrange
        var json = GetJsonForInterval(interval);
        var sut = new ApiJsonConverter(json, interval);

        // Act
        var cryptoTrends = sut.ConvertToCryptoTrends();

        // Assert
        Assert.That(cryptoTrends.Count(), Is.EqualTo(2));
    }

    #region Helpers
    // Copied example json from https://www.alphavantage.co/query?function=DIGITAL_CURRENCY_MONTHLY&symbol=BTC&market=SEK&apikey=39OD3M30HJ0B5HWY
    private static string GetJsonForInterval(TrendInterval interval)
    {
        return interval switch
        {
            TrendInterval.Daily => @"
{
    ""Meta Data"": {
        ""1. Information"": ""Daily Prices and Volumes for Digital Currency"",
        ""2. Digital Currency Code"": ""BTC"",
        ""3. Digital Currency Name"": ""Bitcoin"",
        ""4. Market Code"": ""SEK"",
        ""5. Market Name"": ""Swedish Krona"",
        ""6. Last Refreshed"": ""2022-11-16 00:00:00"",
        ""7. Time Zone"": ""UTC""
    },
    ""Time Series (Digital Currency Daily)"": {
        ""2022-11-16"": {
            ""1a. open (SEK)"": ""176864.29604400"",
            ""1b. open (USD)"": ""16900.57000000"",
            ""2a. high (SEK)"": ""178071.43264100"",
            ""2b. high (USD)"": ""17015.92000000"",
            ""3a. low (SEK)"": ""175452.25494400"",
            ""3b. low (USD)"": ""16765.64000000"",
            ""4a. close (SEK)"": ""177033.61958300"",
            ""4b. close (USD)"": ""16916.75000000"",
            ""5. volume"": ""53187.57454000"",
            ""6. market cap (USD)"": ""53187.57454000""
        },
        ""2022-11-15"": {
            ""1a. open (SEK)"": ""173904.27362300"",
            ""1b. open (USD)"": ""16617.72000000"",
            ""2a. high (SEK)"": ""179314.35950300"",
            ""2b. high (USD)"": ""17134.69000000"",
            ""3a. low (SEK)"": ""172962.42452300"",
            ""3b. low (USD)"": ""16527.72000000"",
            ""4a. close (SEK)"": ""176864.29604400"",
            ""4b. close (USD)"": ""16900.57000000"",
            ""5. volume"": ""282461.84391000"",
            ""6. market cap (USD)"": ""282461.84391000""
        }
    }
}",
            TrendInterval.Weekly => @"{
    ""Meta Data"": {
        ""1. Information"": ""Weekly Prices and Volumes for Digital Currency"",
        ""2. Digital Currency Code"": ""BTC"",
        ""3. Digital Currency Name"": ""Bitcoin"",
        ""4. Market Code"": ""SEK"",
        ""5. Market Name"": ""Swedish Krona"",
        ""6. Last Refreshed"": ""2022-11-16 00:00:00"",
        ""7. Time Zone"": ""UTC""
    },
    ""Time Series (Digital Currency Weekly)"": {
        ""2022-11-16"": {
            ""1a. open (SEK)"": ""170911.91438200"",
            ""1b. open (USD)"": ""16331.78000000"",
            ""2a. high (SEK)"": ""179893.17810000"",
            ""2b. high (USD)"": ""17190.00000000"",
            ""3a. low (SEK)"": ""165506.01449800"",
            ""3b. low (USD)"": ""15815.21000000"",
            ""4a. close (SEK)"": ""177033.61958300"",
            ""4b. close (USD)"": ""16916.75000000"",
            ""5. volume"": ""715860.19595000"",
            ""6. market cap (USD)"": ""715860.19595000""
        },
        ""2022-11-13"": {
            ""1a. open (SEK)"": ""218776.68564400"",
            ""1b. open (USD)"": ""20905.58000000"",
            ""2a. high (SEK)"": ""220494.93235200"",
            ""2b. high (USD)"": ""21069.77000000"",
            ""3a. low (SEK)"": ""163128.26412000"",
            ""3b. low (USD)"": ""15588.00000000"",
            ""4a. close (SEK)"": ""170891.71695200"",
            ""4b. close (USD)"": ""16329.85000000"",
            ""5. volume"": ""3234391.87393000"",
            ""6. market cap (USD)"": ""3234391.87393000""
        }
    }
}",
            TrendInterval.Monthly => @"{
    ""Meta Data"": {
        ""1. Information"": ""Monthly Prices and Volumes for Digital Currency"",
        ""2. Digital Currency Code"": ""BTC"",
        ""3. Digital Currency Name"": ""Bitcoin"",
        ""4. Market Code"": ""SEK"",
        ""5. Market Name"": ""Swedish Krona"",
        ""6. Last Refreshed"": ""2022-11-16 00:00:00"",
        ""7. Time Zone"": ""UTC""
    },
    ""Time Series (Digital Currency Monthly)"": {
        ""2022-11-16"": {
            ""1a. open (SEK)"": ""214435.38919300"",
            ""1b. open (USD)"": ""20490.74000000"",
            ""2a. high (SEK)"": ""224794.78744400"",
            ""2b. high (USD)"": ""21480.65000000"",
            ""3a. low (SEK)"": ""163128.26412000"",
            ""3b. low (USD)"": ""15588.00000000"",
            ""4a. close (SEK)"": ""177033.61958300"",
            ""4b. close (USD)"": ""16916.75000000"",
            ""5. volume"": ""5852439.28405000"",
            ""6. market cap (USD)"": ""5852439.28405000""
        },
        ""2022-10-31"": {
            ""1a. open (SEK)"": ""203257.41942400"",
            ""1b. open (USD)"": ""19422.61000000"",
            ""2a. high (SEK)"": ""220654.31415000"",
            ""2b. high (USD)"": ""21085.00000000"",
            ""3a. low (SEK)"": ""190358.16810000"",
            ""3b. low (USD)"": ""18190.00000000"",
            ""4a. close (SEK)"": ""214435.38919300"",
            ""4b. close (USD)"": ""20490.74000000"",
            ""5. volume"": ""7499121.81542000"",
            ""6. market cap (USD)"": ""7499121.81542000""
        }
    }
}",
            _ => ""
        };
    }
    #endregion
}