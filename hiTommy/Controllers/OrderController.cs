using hiTommy.Data.Services;
using hiTommy.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace hiTommy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        public OrderService _orderService;

        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("get-all-orders")]
        public IActionResult GetAllOrders()
        {
            var order = _orderService.GetAllOrders();
            return Ok(order);
        }

        [HttpGet("get-order-by-id/{id}")]
        public IActionResult GetShoeById(int id)
        {
            var order = _orderService.GetOrderById(id);
            return Ok(order);
        }

        [HttpPost("add-order")]
        public IActionResult AddOrder([FromBody] OrderVm order)
        {
            //_orderService.AddOrder(order);
            return Ok();
        }

        [HttpDelete("delete-order-by-id/{id}")]
        public IActionResult DeleteOrderById(int id)
        {
            _orderService.DeleteOrderById(id);
            return Ok();
        }
    }
}