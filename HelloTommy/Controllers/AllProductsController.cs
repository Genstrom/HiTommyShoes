using hiTommy.Data.Services;
using hiTommy.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HelloTommy.Controllers
{
    [Route("AllProducts")]
    public class AllProductsController : Controller
    {
        private readonly ShoeServices _shoesService;

        public AllProductsController(ShoeServices shoeService)
        {
            _shoesService = shoeService;
        }

        public IActionResult Index()
        {
            var allShoesVm = new ShoeListViewModel
            {
                Shoes = _shoesService.GetAllShoes()
            };


            var AllShoes = allShoesVm.Shoes;


            return View(AllShoes);
        }
    }
}