using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using hiTommy.Data.Models;
using hiTommy.Data.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using static HelloTommy.Models.Klarna;

namespace HelloTommy.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly IConfiguration _config;
        private readonly OrderService _orderService;
        private readonly ShoppingCart _shoppingCart;
        private readonly ShoeServices shoeService;
        private string baseURL = "https://api.playground.klarna.com/";

        public CheckoutController(ShoppingCart shoppingCart, ShoeServices shoeService, OrderService orderService,
            IConfiguration config)
        {
            _shoppingCart = shoppingCart;
            this.shoeService = shoeService;
            _config = config;
            _orderService = orderService;
        }
        public IActionResult Index()
        {
            return Redirect("/");
        }
        
        [Route("Checkout")]
        [HttpPost]
        public ActionResult CheckoutKlarna(int size, int shoeId, string email)
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            var order = _orderService.AddEmptyOrderAndReturnEmptyOrder();

            using var client = new HttpClient();
            client.BaseAddress = new Uri(baseURL);
            client.DefaultRequestHeaders.Add("Authorization", "Basic " + _config["KlarnaAuth"]);
            var request = new HttpRequestMessage(HttpMethod.Post, "checkout/v3/orders");
            var root = CreateKlarnaRootObject(_shoppingCart, _orderService, items, order.OrderId);
            var jsonContent = JsonConvert.SerializeObject(root, Formatting.Indented);
            request.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            Debug.WriteLine(jsonContent);

            var result = client.SendAsync(request);

            var resultString = result.Result.Content.ReadAsStringAsync();

            Debug.WriteLine(resultString.Result);

            var klarna = JsonConvert.DeserializeObject<Rootobject>(resultString.Result);

            Debug.WriteLine(klarna);

            klarna.merchant_urls.confirmation = $"https://localhost:44383/OrderConfirmed/{klarna.order_id}";

            return View("Klarna", klarna);
        }
    }
}