using Stox.Models;

namespace stox.ServiceContracts;

public interface IFinnhubService
{
    Task<List<Stock>> GetStocksListAsync();
    Task<StockProfile> GetStockProfilesAsync(string symbol);
}