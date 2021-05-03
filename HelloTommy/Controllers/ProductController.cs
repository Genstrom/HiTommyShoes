using System.Dynamic;
using hiTommy.Data.Services;
using hiTommy.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HelloTommy.Controllers
{
    //[Authorize]
    [Route("product")]
    public class ProductController : Controller
    {
        private readonly BrandServices _brandServices;
        private readonly QuantityService _quantityService;
        private readonly ShoeServices _shoesService;

        public ProductController(ShoeServices shoeServices, BrandServices brandServices,
            QuantityService quantityService)
        {
            _shoesService = shoeServices;
            _brandServices = brandServices;
            _quantityService = quantityService;
        }

        [Route("{productId:int}")]
        public IActionResult Index(int productId)
        {
            var shoeWithAllSizesViewModel = _shoesService.getNewShoeWithAllSizesVm(productId,_quantityService);

            return View(shoeWithAllSizesViewModel);
        }
    }
}