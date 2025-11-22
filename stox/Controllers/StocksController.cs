using Microsoft.AspNetCore.Mvc;

namespace stox.Controllers;

public class StocksController: Controller
{
    [Route("/")]
    public IActionResult Index()
    {
        return View();
    }
}