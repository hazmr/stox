using System.Text.Json;
using Stox.Models;
using stox.ServiceContracts;

namespace stox.Services;

public class FinnhubService: IFinnhubService
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly IConfiguration _configuration;
    
    public FinnhubService(IHttpClientFactory clientFactory, IConfiguration configuration)
    {
        _clientFactory = clientFactory;
        _configuration = configuration;
    }

    public async Task<Dictionary<string, object>> GetStockPriceAsync(string symbol)
    {
        var requestUrl = $"{_configuration["Finnhub:BaseUrl"]}/quote?symbol={symbol}&token={_configuration["Finnhub:ApiKey"]}";

        var client = _clientFactory.CreateClient();
        
        var response = await client.GetAsync(requestUrl);
        
        response.EnsureSuccessStatusCode();
        
        var content = await response.Content.ReadAsStringAsync();
        
        var result = JsonSerializer.Deserialize<Dictionary<string, object>>(content);
        
        return result ?? new Dictionary<string, object>();
    }


    public async Task<List<Stock>> GetStocksListAsync()
    {
        var requestUrl = $"{_configuration["Finnhub:BaseUrl"]}stock/symbol?exchange=US&token={_configuration["Finnhub:ApiKey"]}";
        var client = _clientFactory.CreateClient();
        var response = await client.GetAsync(requestUrl);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();

        // Deserialize directly to List<Stock>
        var stocks = JsonSerializer.Deserialize<List<Stock>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return stocks ?? new List<Stock>();
    }

    public async Task<StockProfile> GetStockProfilesAsync(string symbol)
    {
        var requestUrl = $"{_configuration["Finnhub:BaseUrl"]}stock/profile2?symbol={symbol}&token={_configuration["Finnhub:ApiKey"]}";
        var client = _clientFactory.CreateClient();
        var response = await client.GetAsync(requestUrl);
        response.EnsureSuccessStatusCode();
        
        var content = await response.Content.ReadAsStringAsync();
        var profiles = JsonSerializer.Deserialize<StockProfile>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
        return profiles ?? new StockProfile();
    }
}