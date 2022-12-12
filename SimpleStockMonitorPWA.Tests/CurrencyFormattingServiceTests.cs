namespace SimpleStockMonitorPWA.Tests;

public class CurrencyFormattingServiceTests
{
    private CurrencyFormattingService _sut;

    public CurrencyFormattingServiceTests()
    {
        _sut = new CurrencyFormattingService();
    }

    [Test]
    public void Zero_should_format_without_decimals()
    {
        // Arrange
        var value = 0;

        // Act
        var formatted = _sut.GetFormattedValue(value);

        // Assert
        Assert.That(formatted, Is.EqualTo("0"));
    }

    [Test]
    public void Small_values_should_format_with_3_decimals()
    {
        // Arrange
        var value = 9.3258;

        // Act
        var formatted = _sut.GetFormattedValue(value);

        // Assert
        Assert.That(formatted, Is.EqualTo("9,326"));
    }

    [Test]
    public void Fairly_small_values_should_format_with_2_decimals()
    {
        // Arrange
        var value = 998.8392;

        // Act
        var formatted = _sut.GetFormattedValue(value);

        // Assert
        Assert.That(formatted, Is.EqualTo("998,84"));
    }

    [Test]
    public void Large_values_should_format_without_decimals_and_thousand_separator()
    {
        // Arrange
        var value = 1234567.89;

        // Act
        var formatted = _sut.GetFormattedValue(value);

        // Assert
        Assert.That(formatted, Is.EqualTo("1 234 568"));
    }

    [TestCase(Currency.USD, "$")]
    [TestCase(Currency.EUR, "€")]
    [TestCase(Currency.SEK, "kr")]
    public void Currency_should_get_correct_unit_symbol(Currency currency, string expectedSymbol)
    {
        // Arrange, Act
        var symbol = _sut.GetCurrencyUnitSymbol(currency);

        // Assert
        Assert.That(symbol, Is.EqualTo(expectedSymbol));
    }

    [Test]
    public void Format_value_USD_should_place_currency_sign_after()
    {
        // Arrange
        var value = 123.456;
        var currency = Currency.USD;

        // Act
        var formatted = _sut.FormatValueWithCurrency(value, currency);

        Assert.That(formatted, Is.EqualTo("123,46 $"));
    }

    [Test]
    public void Format_value_SEK_should_place_currency_sign_after()
    {
        // Arrange
        var value = 123.456;
        var currency = Currency.SEK;

        // Act
        var formatted = _sut.FormatValueWithCurrency(value, currency);

        Assert.That(formatted, Is.EqualTo("123,46 kr"));
    }

    [Test]
    public void Format_value_EUR_should_place_currency_sign_before()
    {
        // Arrange
        var value = 123.456;
        var currency = Currency.EUR;

        // Act
        var formatted = _sut.FormatValueWithCurrency(value, currency);

        Assert.That(formatted, Is.EqualTo("€ 123,46"));
    }
}
