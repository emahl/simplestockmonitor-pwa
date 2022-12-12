using SimpleStockMonitorPWA.Models;

namespace SimpleStockMonitorPWA.Services;

public interface ICryptoTrendValueService
{
    Trend GetCurrentTrend(CryptoTrendValuesContainer currentValues, CryptoTrendValuesContainer previousValues, Currency currency);
    double GetMainValue(CryptoTrendValuesContainer valueContainer, Currency currency);
}

public class CryptoTrendValueService : ICryptoTrendValueService
{
    public Trend GetCurrentTrend(
        CryptoTrendValuesContainer currentValues,
        CryptoTrendValuesContainer previousValues,
        Currency currency)
    {
        var currentValue = GetMainValue(currentValues, currency);
        var previousValue = GetMainValue(previousValues, currency);

        if (currentValue < previousValue)
        {
            return Trend.Down;
        }
        if (currentValue > previousValue)
        {
            return Trend.Up;
        }

        return Trend.Flat;
    }

    public double GetMainValue(
        CryptoTrendValuesContainer valueContainer,
        Currency currency)
    {
        if (valueContainer?.Values is null)
        {
            return 0;
        }

        if (currency is Currency.USD)
        {
            return valueContainer.Values.CloseUSD != 0 ?
               valueContainer.Values.CloseUSD :
               valueContainer.Values.OpenUSD;
        }

        return valueContainer.Values.CloseOther != 0 ?
                valueContainer.Values.CloseOther :
                valueContainer.Values.OpenOther;
    }
}
