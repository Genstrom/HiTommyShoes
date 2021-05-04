using HelloTommy.Models;
using hiTommy.Data.Models;
using hiTommy.Data.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace HelloTommy.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly IConfiguration _config;
        private readonly Klarna _klarna;
        private readonly OrderService _orderService;
        private readonly ShoppingCart _shoppingCart;
        private readonly ShoeServices shoeService;


        public CheckoutController(ShoppingCart shoppingCart, ShoeServices shoeService, OrderService orderService,
            IConfiguration config)
        {
            _shoppingCart = shoppingCart;
            this.shoeService = shoeService;
            _config = config;
            _orderService = orderService;
            _klarna = new Klarna(orderService, shoppingCart, config,shoeService);
        }

        public IActionResult Index()
        {
            return Redirect("/");
        }

        [Route("Checkout")]
        [HttpPost]
        public ActionResult CheckoutKlarna(int size, int shoeId)
        {
            var klarna = _klarna.ConsumeKlarnaApi(size, shoeId, _shoppingCart);

            return View("Klarna", klarna);
        }
    }
}