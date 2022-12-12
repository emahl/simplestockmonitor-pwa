namespace SimpleStockMonitorPWA.Models;

public class CryptoTrendValuesContainer
{
    public DateOnly Date { get; }
    public CryptoValues Values { get; }

    public CryptoTrendValuesContainer(DateOnly date, CryptoValues values)
    {
        Date = date;
        Values = values;
    }

    public static CryptoTrendValuesContainer Empty => new(DateOnly.MinValue, CryptoValues.Empty);
}