namespace SimpleStockMonitorPWA.Tests;

public class CryptoTrendValueServiceTests
{
    private CryptoTrendValueService _sut;

    public CryptoTrendValueServiceTests()
    {
        _sut = new CryptoTrendValueService();
    }

    [Test]
    public void Current_larger_than_previous_should_return_trend_up()
    {
        // Arrange
        var currency = Currency.USD;
        var current = new CryptoTrendValuesContainer(
            new DateOnly(2022, 12, 24),
            new CryptoValues.Builder().AddCloseUSD(2).Build());
        var previous = new CryptoTrendValuesContainer(
            new DateOnly(2022, 12, 23),
            new CryptoValues.Builder().AddCloseUSD(1).Build());

        // Act
        var trend = _sut.GetCurrentTrend(current, previous, currency);

        // Assert
        Assert.That(trend, Is.EqualTo(Trend.Up));
    }

    [Test]
    public void Current_smaller_than_previous_should_return_trend_down()
    {
        // Arrange
        var currency = Currency.USD;
        var current = new CryptoTrendValuesContainer(
            new DateOnly(2022, 12, 24),
            new CryptoValues.Builder().AddCloseUSD(1).Build());
        var previous = new CryptoTrendValuesContainer(
            new DateOnly(2022, 12, 23),
            new CryptoValues.Builder().AddCloseUSD(2).Build());

        // Act
        var trend = _sut.GetCurrentTrend(current, previous, currency);

        // Assert
        Assert.That(trend, Is.EqualTo(Trend.Down));
    }

    [Test]
    public void Current_same_as_previous_should_return_trend_flat()
    {
        // Arrange
        var currency = Currency.USD;
        var current = new CryptoTrendValuesContainer(
            new DateOnly(2022, 12, 24),
            new CryptoValues.Builder().AddCloseUSD(1).Build());
        var previous = new CryptoTrendValuesContainer(
            new DateOnly(2022, 12, 23),
            new CryptoValues.Builder().AddCloseUSD(1).Build());

        // Act
        var trend = _sut.GetCurrentTrend(current, previous, currency);

        // Assert
        Assert.That(trend, Is.EqualTo(Trend.Flat));
    }

    [Test]
    public void Other_currency_current_larger_than_previous_should_return_trend_up()
    {
        // Arrange
        var currency = Currency.SEK;
        var current = new CryptoTrendValuesContainer(
            new DateOnly(2022, 12, 24),
            new CryptoValues.Builder().AddCloseOther(2).Build());
        var previous = new CryptoTrendValuesContainer(
            new DateOnly(2022, 12, 23),
            new CryptoValues.Builder().AddCloseOther(1).Build());

        // Act
        var trend = _sut.GetCurrentTrend(current, previous, currency);

        // Assert
        Assert.That(trend, Is.EqualTo(Trend.Up));
    }

    [Test]
    public void Other_currency_current_smaller_than_previous_should_return_trend_down()
    {
        // Arrange
        var currency = Currency.SEK;
        var current = new CryptoTrendValuesContainer(
            new DateOnly(2022, 12, 24),
            new CryptoValues.Builder().AddCloseOther(1).Build());
        var previous = new CryptoTrendValuesContainer(
            new DateOnly(2022, 12, 23),
            new CryptoValues.Builder().AddCloseOther(2).Build());

        // Act
        var trend = _sut.GetCurrentTrend(current, previous, currency);

        // Assert
        Assert.That(trend, Is.EqualTo(Trend.Down));
    }

    [Test]
    public void Other_currency_current_same_as_previous_should_return_trend_flat()
    {
        // Arrange
        var currency = Currency.SEK;
        var current = new CryptoTrendValuesContainer(
            new DateOnly(2022, 12, 24),
            new CryptoValues.Builder().AddCloseOther(1).Build());
        var previous = new CryptoTrendValuesContainer(
            new DateOnly(2022, 12, 23),
            new CryptoValues.Builder().AddCloseOther(1).Build());

        // Act
        var trend = _sut.GetCurrentTrend(current, previous, currency);

        // Assert
        Assert.That(trend, Is.EqualTo(Trend.Flat));
    }
}
