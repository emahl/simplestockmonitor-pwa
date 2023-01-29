using SimpleStockMonitorPWA.Models;

namespace SimpleStockMonitorPWA.Services;

public interface ICurrencyFormattingService
{
    string GetCurrencyUnitSymbol(Currency currency);
    string FormatValueWithCurrency(double value, Currency currency);
    string GetFormattedValue(double value);
}

public class CurrencyFormattingService : ICurrencyFormattingService
{
    public string GetCurrencyUnitSymbol(Currency currency)
    {
        return currency switch
        {
            Currency.USD => "$",
            Currency.EUR => "€",
            Currency.SEK => "kr",
            _ => throw new NotImplementedException()
        };
    }

    public string FormatValueWithCurrency(double value, Currency currency)
    {
        var formattedValue = GetFormattedValue(value);
        var currencySign = GetCurrencyUnitSymbol(currency);

        if (IsCurrencySignAfterValue(currency))
        {
            return $"{formattedValue} {currencySign}";
        }

        return $"{currencySign} {formattedValue}";
    }

    public string GetFormattedValue(double value)
    {
        if (value == 0)
        {
            return "0";
        }
        if (value < 10)
        {
            return $"{value:N3}";
        }
        if (value < 1000)
        {
            return $"{value:N2}";
        }

        return $"{value:N0}";
    }

    private static bool IsCurrencySignAfterValue(Currency currency) =>
        currency is Currency.SEK;
}
