using Bunit;
using Microsoft.Extensions.DependencyInjection;
using SimpleStockMonitorPWA.App.Components;

namespace SimpleStockMonitorPWA.Tests.Blazor;

public class ValueTrendComponentTests
{
    private Bunit.TestContext _ctx;

    [SetUp]
    public void Setup()
    {
        _ctx = new Bunit.TestContext();
        _ctx.Services.AddSingleton<ICurrencyFormattingService, CurrencyFormattingService>();
        _ctx.Services.AddSingleton<ICryptoTrendValueService, CryptoTrendValueService>();
    }

    [Test]
    public void Should_display_no_value_as_zero_USD()
    {
        // Arrange, Act
        var cut = _ctx.RenderComponent<ValueTrendComponent>();

        // Assert
        var valueText = cut.Find(".main-value").TextContent;
        Assert.That(valueText, Is.EqualTo("0 $"));
    }

    [Test]
    public void Should_display_no_value_as_zero_SEK()
    {
        // Arrange, Act
        var cut = _ctx.RenderComponent<ValueTrendComponent>(parameters => parameters
            .Add(p => p.Currency, Currency.SEK));

        // Assert
        var valueText = cut.Find(".main-value").TextContent;
        Assert.That(valueText, Is.EqualTo("0 kr"));
    }

    [Test]
    public void Should_display_the_current_main_value_formatted_in_USD()
    {
        // Arrange
        var currentValues = CreateMockValuesContainer("2022-12-31", 100);

        // Act
        var cut = _ctx.RenderComponent<ValueTrendComponent>(parameters => parameters
            .Add(p => p.CurrentValues, currentValues)
            .Add(p => p.Currency, Currency.USD));

        // Assert
        var valueText = cut.Find(".main-value").TextContent;
        Assert.That(valueText, Is.EqualTo("100,00 $"));
    }

    [Test]
    public void Should_display_the_current_main_value_formatted_in_SEK()
    {
        // Arrange
        var currentValues = CreateMockValuesContainer("2022-12-31", 100);

        // Act
        var cut = _ctx.RenderComponent<ValueTrendComponent>(parameters => parameters
            .Add(p => p.CurrentValues, currentValues)
            .Add(p => p.Currency, Currency.SEK));

        // Assert
        var valueText = cut.Find(".main-value").TextContent;
        Assert.That(valueText, Is.EqualTo("1 000 kr"));
    }

    [Test]
    public void Should_display_flat_trend_if_no_values()
    {
        // Arrange, Act
        var cut = _ctx.RenderComponent<ValueTrendComponent>();

        // Assert
        var trendTitle = cut.Find(".trend").GetElementsByTagName("title")[0].TextContent;
        Assert.That(trendTitle, Is.EqualTo("trend-flat"));
    }

    [Test]
    public void Should_display_flat_trend_if_current_value_is_same_as_previous()
    {
        // Arrange
        var currentValues = CreateMockValuesContainer("2022-12-31", 100);
        var previousValues = CreateMockValuesContainer("2022-12-30", 100);

        // Act
        var cut = _ctx.RenderComponent<ValueTrendComponent>(parameters => parameters
            .Add(p => p.CurrentValues, currentValues)
            .Add(p => p.PreviousValues, previousValues));

        // Assert
        var trendTitle = cut.Find(".trend").GetElementsByTagName("title")[0].TextContent;
        Assert.That(trendTitle, Is.EqualTo("trend-flat"));
    }

    [Test]
    public void Should_display_down_trend_if_current_value_is_lower_than_previous()
    {
        // Arrange
        var currentValues = CreateMockValuesContainer("2022-12-31", 100);
        var previousValues = CreateMockValuesContainer("2022-12-30", 200);

        // Act
        var cut = _ctx.RenderComponent<ValueTrendComponent>(parameters => parameters
            .Add(p => p.CurrentValues, currentValues)
            .Add(p => p.PreviousValues, previousValues));

        // Assert
        var trendTitle = cut.Find(".trend").GetElementsByTagName("title")[0].TextContent;
        Assert.That(trendTitle, Is.EqualTo("trend-down"));
    }

    [Test]
    public void Should_display_up_trend_if_current_value_is_higher_than_previous()
    {
        // Arrange
        var currentValues = CreateMockValuesContainer("2022-12-31", 100);
        var previousValues = CreateMockValuesContainer("2022-12-30", 20);

        // Act
        var cut = _ctx.RenderComponent<ValueTrendComponent>(parameters => parameters
            .Add(p => p.CurrentValues, currentValues)
            .Add(p => p.PreviousValues, previousValues));

        // Assert
        var trendTitle = cut.Find(".trend").GetElementsByTagName("title")[0].TextContent;
        Assert.That(trendTitle, Is.EqualTo("trend-up"));
    }

    private static CryptoTrendValuesContainer CreateMockValuesContainer(string date, double value) =>
        new(DateOnly.Parse(date), CreateMockValues(value));

    private static CryptoValues CreateMockValues(double value) => 
        new CryptoValues.Builder().AddCloseUSD(value).AddCloseOther(value * 10).Build();
}