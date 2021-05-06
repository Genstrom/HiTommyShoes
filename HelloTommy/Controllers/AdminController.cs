using hiTommy.Data.Repositories;
using hiTommy.Data.Services;
using hiTommy.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using hiTommy.Interfaces;
using System.Threading.Tasks;

namespace HelloTommy.Controllers
{
    [Route("admin")]
    public class AdminController : Controller
    {
        private readonly AdminViewModel _adminVm;
        private IUnitOfWork _unitOfWork;
        private ShoeServices _shoesService;

        public AdminController(IUnitOfWork unitOfWork, ShoeServices shoesService)
        {
            _unitOfWork = unitOfWork;
            _shoesService = shoesService;
            _adminVm = new AdminViewModel
            {
                Brands = _unitOfWork.BrandService.GetAllBrands(),
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
        public async Task<ActionResult> AddBrandView(AdminViewModel adminVM)
        {
            var brand = new BrandVm
            {
                Name = adminVM.Brand.Name
            };
            _unitOfWork.BrandService.AddBrand(brand);
            if(await _unitOfWork.Complete()) return RedirectToAction("Index");

            return View("Error");
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
            _unitOfWork.BrandService.DeleteBrandById(id);
            _unitOfWork.Complete();

            return RedirectToAction("Index");
        }
    }
}