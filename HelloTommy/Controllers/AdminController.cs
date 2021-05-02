using hiTommy.Data.Services;
using hiTommy.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HelloTommy.Controllers
{
    [Route("admin")]
    public class AdminController : Controller
    {
        private readonly AdminViewModel _adminVm;
        private BrandServices _brandServices;
        private ShoeServices _shoesService;

        public AdminController(BrandServices brandServices, ShoeServices shoesService)
        {
            _brandServices = brandServices;
            _shoesService = shoesService;
            _adminVm = new AdminViewModel
            {
                Brands = brandServices.GetAllBrands(),
                Shoes = shoesService.GetAllShoes()
            };
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_adminVm);
        }

        [Route("add-shoe")]
        [HttpGet]
        public IActionResult AddShoeView()
        {
            return View(_adminVm);
        }

        [Route("add-shoe")]
        [HttpPost]
        public ActionResult AddShoeView(ShoeViewModel shoe)
        {
            if (ModelState.IsValid)
            {
                
             
                _shoesService.AddShoe(shoe);
            }

            return RedirectToAction("Index", _adminVm);
        }

        [Route("delete-shoe")]
        [HttpGet]
        public IActionResult DeleteShoeView()
        {
            return View(_adminVm);
        }

        [Route("delete-shoe")]
        [HttpPost]
        public ActionResult DeleteShoeView(int id)
        {
            _shoesService.DeleteShoeById(id);
            return RedirectToAction("Index", _adminVm);
        }

        [Route("add-brand")]
        [HttpGet]
        public IActionResult AddBrandView()
        {
            return View(_adminVm);
        }

        [Route("add-brand")]
        [HttpPost]
        public ActionResult AddBrandView(string name)
        {
            var brand = new BrandVm
            {
                Name = name
            };
            _brandServices.AddBrand(brand);

            return RedirectToAction("Index");
        }

        [Route("delete-brand")]
        [HttpGet]
        public IActionResult DeleteBrandView()
        {
            return View(_adminVm);
        }

        [Route("delete-brand")]
        [HttpPost]
        public ActionResult DeleteBrandView(int id)
        {
            _brandServices.DeleteBrandById(id);

            return RedirectToAction("Index");
        }
    }
}