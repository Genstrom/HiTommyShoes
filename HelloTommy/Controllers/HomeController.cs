using System.Diagnostics;
using HelloTommy.Models;
using hiTommy.Data.Services;
using hiTommy.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HelloTommy.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ShoeServices _shoesService;

        public HomeController(ILogger<HomeController> logger, ShoeServices shoeService, BrandServices brandServices)
        {
            _logger = logger;
            _shoesService = shoeService;
        }

        public IActionResult Index()
        {
            var allShoesVm = new ShoeListViewModel
            {
                Shoes = _shoesService.GetAllShoes()
            };

            var allShoes = allShoesVm.Shoes;

            return View(allShoes);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}