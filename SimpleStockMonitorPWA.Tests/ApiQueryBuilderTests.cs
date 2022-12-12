namespace SimpleStockMonitorPWA.Tests;

public class ApiQueryBuilderTests
{
    private ApiQueryBuilder _sut;
    private const string ApiKey = "APIKEY_TEST";
    private const string Market = "MARKET_TEST";
    private const string Symbol = "SYMBOL_TEST";
    private readonly (string Function, TrendInterval Interval) DailyFunctionInterval = ("DIGITAL_CURRENCY_DAILY", TrendInterval.Daily);

    [SetUp]
    public void Setup()
    {
        _sut = new ApiQueryBuilder();
    }

    [Test]
    public void Should_build_without_errors_when_all_required_fields_are_set()
    {
        // Arrange, Act
        var query = _sut
            .SetApiKey(ApiKey)
            .SetSymbol(Symbol)
            .SetMarket(Market)
            .SetInterval(DailyFunctionInterval.Interval)
            .BuildQuery();

        // Assert
        Assert.That(query, Is.Not.Null.Or.Empty);
    }

    [Test]
    public void Should_throw_exception_when_apikey_is_missing()
    {
        // Arrange, Act
        var exception = Assert.Throws<Exception>(() => _sut
            .SetInterval(DailyFunctionInterval.Interval)
            .SetMarket(Market)
            .SetSymbol(Symbol)
            .BuildQuery());

        // Assert
        Assert.That(exception.Message, Does.Contain("apikey"));
    }

    [Test]
    public void Should_throw_exception_when_function_interval_missing()
    {
        // Arrange, Act
        var exception = Assert.Throws<Exception>(() => _sut
            .SetApiKey(ApiKey)
            .SetMarket(Market)
            .SetSymbol(Symbol)
            .BuildQuery());

        // Assert
        Assert.That(exception.Message, Does.Contain("function"));
    }

    [Test]
    public void Should_throw_exception_when_market_missing()
    {
        // Arrange, Act
        var exception = Assert.Throws<Exception>(() => _sut
            .SetApiKey(ApiKey)
            .SetInterval(DailyFunctionInterval.Interval)
            .SetSymbol(Symbol)
            .BuildQuery());

        // Assert
        Assert.That(exception.Message, Does.Contain("market"));
    }

    [Test]
    public void Should_throw_exception_when_symbol_missing()
    {
        // Arrange, Act
        var exception = Assert.Throws<Exception>(() => _sut
            .SetApiKey(ApiKey)
            .SetInterval(DailyFunctionInterval.Interval)
            .SetMarket(Market)
            .BuildQuery());

        // Assert
        Assert.That(exception.Message, Does.Contain("symbol"));
    }

    [TestCase(TrendInterval.Daily, "DIGITAL_CURRENCY_DAILY")]
    [TestCase(TrendInterval.Weekly, "DIGITAL_CURRENCY_WEEKLY")]
    [TestCase(TrendInterval.Monthly, "DIGITAL_CURRENCY_MONTHLY")]
    public void Should_convert_interval_to_correct_function(TrendInterval interval, string expectedFunction)
    {
        // Arrange, Act
        var query = _sut
            .SetApiKey(ApiKey)
            .SetSymbol(Symbol)
            .SetMarket(Market)
            .SetInterval(interval)
            .BuildQuery();

        // Assert
        Assert.That(query, Does.Contain(expectedFunction));
    }
}
