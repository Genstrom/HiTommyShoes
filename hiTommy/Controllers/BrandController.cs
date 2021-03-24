using hiTommy.Data.Services;
using hiTommy.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace hiTommy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        public BrandServices _brandService;

        public BrandController(BrandServices brandsService)
        {
            _brandService = brandsService;
        }

        [HttpGet("get-all-brands")]
        public IActionResult GetAllBrands()
        {
            var allBrands = _brandService.GetAllBrands();
            return Ok(allBrands);
        }

        [HttpGet("get-shoes-by-brand-id/{id}")]
        public IActionResult GetShoesByBrandId(int id)
        {
            var shoesByBrand = _brandService.GetShoesByBrandId(id);
            return Ok(shoesByBrand);
        }

        [HttpPost("add-brand")]
        public IActionResult AddBrand([FromBody] BrandVm brand)
        {
            _brandService.AddBrand(brand);
            return Ok();
        }

        [HttpDelete("delete-brand-by-id/{id}")]
        public IActionResult DeleteBrandById(int id)
        {
            _brandService.DeleteBrandById(id);
            return Ok();
        }
    }
}