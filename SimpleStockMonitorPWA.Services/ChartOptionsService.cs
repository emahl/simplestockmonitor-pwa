using SimpleStockMonitorPWA.Models;
using ApexCharts;

namespace SimpleStockMonitorPWA.Services;

public interface IChartOptionsService
{
    ApexChartOptions<CryptoTrendValuesContainer> BuildOptions(Currency currency);
}

public class ChartOptionsService : IChartOptionsService
{
    private readonly ICurrencyFormattingService _currencyFormattingService;

    public ChartOptionsService(ICurrencyFormattingService currencyFormattingService)
    {
        this._currencyFormattingService = currencyFormattingService;
    }

    public ApexChartOptions<CryptoTrendValuesContainer> BuildOptions(Currency currency)
    {
        var options = new ApexChartOptions<CryptoTrendValuesContainer>
        {
            Chart = new() {
                Toolbar = new() 
                {
                    Show = false 
                }, 
                Zoom = new() 
                {
                    Enabled = false 
                } 
            },
            Theme = new()
            {
                Mode = Mode.Light,
                Palette = PaletteType.Palette9
            },
            Yaxis = new()
            {
                new()
                {
                    Labels = new()
                    {
                        Formatter = @"function (value) {
                        return Number(value).toLocaleString() + '" + _currencyFormattingService.GetCurrencyUnitSymbol(currency) + "';}"
                    }
                }
            }
        };
        return options;
    }
}
