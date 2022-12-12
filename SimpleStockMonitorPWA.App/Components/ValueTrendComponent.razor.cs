using Microsoft.AspNetCore.Components;
using SimpleStockMonitorPWA.Models;
using SimpleStockMonitorPWA.Services;
using MudBlazor;

namespace SimpleStockMonitorPWA.App.Components;

public partial class ValueTrendComponent
{
    [Inject] public ICurrencyFormattingService? _currencyFormattingService { get; set; }
    [Inject] public ICryptoTrendValueService? _cryptoTrendValueService { get; set; }

    [Parameter] public CryptoTrendValuesContainer CurrentValues { get; set; } = CryptoTrendValuesContainer.Empty;
    [Parameter] public CryptoTrendValuesContainer PreviousValues { get; set; } = CryptoTrendValuesContainer.Empty;
    [Parameter] public Currency Currency { get; set; } = Currency.USD;
    
    private string GetMainValueFormatted()
    {
        var value = _cryptoTrendValueService!.GetMainValue(CurrentValues, Currency);
        return _currencyFormattingService!.FormatValueWithCurrency(value, Currency);
    }

    private Trend GetCurrentTrend() =>
        _cryptoTrendValueService!.GetCurrentTrend(CurrentValues, PreviousValues, Currency);

    private string GetTrendTitle() => GetCurrentTrend() switch
    {
        Trend.Flat => "trend-flat",
        Trend.Up => "trend-up",
        Trend.Down => "trend-down",
        _ => throw new NotImplementedException()
    };

    private string GetTrendIcon() => GetCurrentTrend() switch
    {
        Trend.Flat => @Icons.Filled.TrendingFlat,
        Trend.Up => @Icons.Filled.TrendingUp,
        Trend.Down => @Icons.Filled.TrendingDown,
        _ => throw new NotImplementedException()
    };

    private Color GetTrendColor() => GetCurrentTrend() switch
    {
        Trend.Flat => Color.Dark,
        Trend.Up => Color.Success,
        Trend.Down => Color.Error,
        _ => throw new NotImplementedException()
    };
}