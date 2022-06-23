using Microsoft.AspNetCore.Mvc;
using Simple_Sales_Management.Models;
using Simple_Sales_Management.ViewModels.Home;
using System.Diagnostics;

namespace Simple_Sales_Management.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SaleDbContext _saleDbContext;

        public HomeController(ILogger<HomeController> logger, SaleDbContext saleDbContext)
        {
            _logger = logger;
            _saleDbContext = saleDbContext;
        }

        public IActionResult Index()
        {
            var viewModel = new HomeIndexViewModel
            {
                NumberOfSales = _saleDbContext.Sales.Count(),
                NumberOfSellers = _saleDbContext.Sellers.Count()
            };
            //var numberOfSellers = _saleDbContext.Sellers.Count();
            //var numberOfSales = _saleDbContext.Sales.Count();


            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}