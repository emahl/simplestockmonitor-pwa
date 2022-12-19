using Microsoft.AspNetCore.Components;
using SimpleStockMonitorPWA.Models;
using SimpleStockMonitorPWA.Services;
using SimpleStockMonitorPWA.App.ClientModels;
using ApexCharts;

namespace SimpleStockMonitorPWA.App.Pages;

public partial class CryptoTab
{
    [Inject] public IApiService? _apiService { get; set; }
    [Inject] public IChartOptionsService? _chartService { get; set; }

    private ApexChart<CryptoTrendValuesContainer>? chart;
    private ApexChartOptions<CryptoTrendValuesContainer>? options;
    private List<CryptoTrendValuesContainer>? allCryptoTrends;
    private List<CryptoTrendValuesContainer>? filteredCryptoTrends;
    
    // List of supported crypto currencies in the API that is used:
    // https://www.alphavantage.co/digital_currency_list/
    private readonly List<CryptoCurrencyOption> cryptoCurrencies = new()
    {
        new("BTC", "Bitcoin", "https://upload.wikimedia.org/wikipedia/commons/4/46/Bitcoin.svg"),
        new("ETH", "Ethereum", "https://upload.wikimedia.org/wikipedia/commons/0/05/Ethereum_logo_2014.svg"), new("DOGE", "Dogecoin", "https://upload.wikimedia.org/wikipedia/en/d/d0/Dogecoin_Logo.png")
    };
    
    private bool isLoading = true;
    private int selectedNrOfPointsToShow = 100;
    private int maxNrOfPointsToShow = 200;
    private TrendInterval trendInterval = TrendInterval.Weekly;
    private Currency currency = Currency.USD;
    private string selectedCurrency = "BTC";
    
    protected async override Task OnInitializedAsync()
    {
        await LoadDataAsync();
        options = _chartService!.BuildOptions(currency);
    }

    private async Task LoadDataAsync()
    {
        isLoading = true;
        
        var cryptoData = (await _apiService!.GetCryptoCurrencyTrendAsync(selectedCurrency, trendInterval, currency)).ToList();
        
        allCryptoTrends = cryptoData;
        maxNrOfPointsToShow = allCryptoTrends.Count();
        selectedNrOfPointsToShow = Math.Min(selectedNrOfPointsToShow, maxNrOfPointsToShow);
        
        await FilterCryptoTrendsAsync();
        
        isLoading = false;
    }

    private async Task ReloadDataAsync()
    {
        await LoadDataAsync();
    }

    private async Task FilterCryptoTrendsAsync()
    {
        filteredCryptoTrends = allCryptoTrends!.Take(selectedNrOfPointsToShow).ToList();
        
        StateHasChanged();
        
        // https://devscope.io/code/apexcharts/Blazor-ApexCharts/issues/141
        await Task.Delay(10);

        if (chart is not null)
        {
            await chart.UpdateSeriesAsync(true);
        }
    }

    private CryptoTrendValuesContainer GetLatestValuesContainer(int index)
    {
        if (filteredCryptoTrends is null || filteredCryptoTrends.Count <= index)
        {
            return CryptoTrendValuesContainer.Empty;
        }

        return filteredCryptoTrends.OrderByDescending(x => x.Date).ElementAt(index);
    }

    private CryptoTrendValuesContainer GetCurrentValuesContainer() => GetLatestValuesContainer(0);
    private CryptoTrendValuesContainer GetPreviousValuesContainer() => GetLatestValuesContainer(1);
}