namespace SimpleStockMonitorPWA.Models;

public class CryptoValues
{
    private CryptoValues() {}

    public static CryptoValues Empty => new();

    public double OpenUSD { get; private set; }
    public double HighUSD { get; private set; }
    public double LowUSD { get; private set; }
    public double CloseUSD { get; private set; }
    public double OpenOther { get; private set; }
    public double HighOther { get; private set; }
    public double LowOther { get; private set; }
    public double CloseOther { get; private set; }
    public double Volume { get; private set; }
    public double MarketCap { get; private set; }

    public class Builder
    {
        private readonly CryptoValues _cryptoTrend;

        public Builder()
        {
            _cryptoTrend = new CryptoValues();
        }

        public Builder AddOpenUSD(double openUSD)
        {
            _cryptoTrend.OpenUSD = openUSD;
            return this;
        }
        public Builder AddCloseUSD(double closeUSD)
        {
            _cryptoTrend.CloseUSD = closeUSD;
            return this;
        }
        public Builder AddHighUSD(double highUSD)
        {
            _cryptoTrend.HighUSD = highUSD;
            return this;
        }
        public Builder AddLowUSD(double lowUSD)
        {
            _cryptoTrend.LowUSD = lowUSD;
            return this;
        }

        public Builder AddOpenOther(double openOther)
        {
            _cryptoTrend.OpenOther = openOther;
            return this;
        }
        public Builder AddCloseOther(double closeOther)
        {
            _cryptoTrend.CloseOther = closeOther;
            return this;
        }
        public Builder AddHighOther(double highOther)
        {
            _cryptoTrend.HighOther = highOther;
            return this;
        }

        public Builder AddLowOther(double lowOther)
        {
            _cryptoTrend.LowOther = lowOther;
            return this;
        }

        public Builder AddVolume(double volume)
        {
            _cryptoTrend.Volume = volume;
            return this;
        }

        public Builder AddMarketCapUSD(double marketCap)
        {
            _cryptoTrend.MarketCap = marketCap;
            return this;
        }

        public CryptoValues Build()
        {
            return _cryptoTrend;
        }
    }
}