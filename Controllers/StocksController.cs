using Microsoft.AspNetCore.Mvc;
using Stox.Models;
using stox.ServiceContracts;

namespace stox.Controllers;

public class StocksController: Controller
{
    private readonly IFinnhubService _finnhubService;
    
    public StocksController(IFinnhubService finnhubService)
    {
        _finnhubService = finnhubService;
    }
    
    [Route("/")]
    public async Task<IActionResult> Index()
    {
        List<Stock> stocks = await _finnhubService.GetStocksListAsync();
        return View(stocks);
    }

    [Route("/stocks/{symbol}")]
    public async Task<IActionResult> Details(string symbol)
    {
        StockProfile profiles = await _finnhubService.GetStockProfilesAsync(symbol);
        return View(profiles);
    }
}