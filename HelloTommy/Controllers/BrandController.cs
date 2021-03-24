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
            var shoeList = BrandServices.GetShoesByBrandName(brandName);
            var allBrandsVm = BrandServices.GetAllBrands();

            dynamic mymodel = new ExpandoObject();
            mymodel.Shoes = shoeList.Shoes;
            mymodel.Brand = allBrandsVm;

            return View(mymodel);
        }
    }
}