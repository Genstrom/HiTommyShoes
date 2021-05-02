using System.Dynamic;
using HelloTommy.Data;
using hiTommy.Data.Services;
using Microsoft.AspNetCore.Mvc;

namespace HelloTommy.Controllers
{
    [Route("brand")]
    public class BrandController : Controller
    {
        private readonly BrandServices BrandServices;
        private ApplicationDbContext Context;

        public BrandController(BrandServices brandServices, ApplicationDbContext context)
        {
            BrandServices = brandServices;
            Context = context;
        }

        [Route("{brandName}")]
        public IActionResult Index(string brandName)
        {
            var brandWithShoesVM = BrandServices.GetShoesByBrandName(brandName);
            return View(brandWithShoesVM);
        }
    }
}