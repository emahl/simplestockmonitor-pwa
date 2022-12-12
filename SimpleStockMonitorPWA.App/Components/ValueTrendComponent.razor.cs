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

    private Trend GetCurrentTrend() => _cryptoTrendValueService!.GetCurrentTrend(CurrentValues, PreviousValues, Currency);
    
    private string GetTrendIcon() => GetCurrentTrend() switch
    {
        Trend.Flat => @Icons.Filled.TrendingFlat,
        Trend.Up => @Icons.Filled.TrendingUp,
        Trend.Down => @Icons.Filled.TrendingDown,
        _ => throw new NotImplementedException()
    };

    private MudBlazor.Color GetTrendColor() => GetCurrentTrend() switch
    {
        Trend.Flat => MudBlazor.Color.Dark,
        Trend.Up => MudBlazor.Color.Success,
        Trend.Down => MudBlazor.Color.Error,
        _ => throw new NotImplementedException()
    };
}