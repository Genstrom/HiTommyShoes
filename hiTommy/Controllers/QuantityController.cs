using hiTommy.Data.Services;
using hiTommy.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace hiTommy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuantityController : Controller
    {
        public QuantityService _quantityService;

        public QuantityController(QuantityService quantityService)
        {
            _quantityService = quantityService;
        }

        [HttpGet("get-all-shoes-by-size/{size}")]
        public IActionResult GetAllShoesBySize(double size)
        {
            var shoes = _quantityService.GetAllShoesBySize(size);
            return Ok();
        }

        [HttpGet("get-all-shoes-by-shoe-id/{id}")]
        public IActionResult GetAllSisesById(int id)
        {
            var shoes = _quantityService.GetAllSizesById(id);
            return Ok();
        }

        [HttpPost("add-quantity-to-shoe-by-size-and-shoeId/{size, id}")]
        public IActionResult AddQuantityToShoeBySizeAndShoeId([FromBody] QuantityVm quantity, double size, int id)
        {
            _quantityService.AddQuantityToShoeBySizeAndShoeId(size, id, quantity);
            return Ok();
        }
    }
}