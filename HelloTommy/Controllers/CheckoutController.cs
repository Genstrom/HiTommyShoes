using HelloTommy.Models;
using hiTommy.Data.Models;
using hiTommy.Data.Services;
using hiTommy.Data.ViewModels;
using hiTommy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using static HelloTommy.Models.Klarna;

namespace HelloTommy.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly OrderService _orderService;
        private readonly ShoeServices shoeService;
        private readonly ShoppingCart _shoppingCart;
        private readonly IConfiguration _config;
        private string baseURL = "https://api.playground.klarna.com/";

        public CheckoutController(ShoppingCart shoppingCart ,ShoeServices shoeService, OrderService orderService, IConfiguration config)
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
            dynamic myModel = new ExpandoObject();
            var _shoe = shoeService.GetShoeById(shoeId);
            myModel.Shoe = _shoe;
            myModel.Size = size;

            var order = new Order();
            order = _orderService.AddEmptyOrderAndReturnEmptyOrder(order);
            var id = order.OrderId;

            using var client = new HttpClient();
            client.BaseAddress = new Uri(baseURL);
            client.DefaultRequestHeaders.Add("Authorization", "Basic " + _config["KlarnaAuth"]);
            var request = new HttpRequestMessage(HttpMethod.Post, $"checkout/v3/orders");
            //KlarnaPost.Order_Lines[] order_lines_array = new KlarnaPost.Order_Lines[items.Count];
            List<KlarnaPost.Order_Lines> order_lines_array = new List<KlarnaPost.Order_Lines>();
            foreach (var item in _shoppingCart.ShoppingCartItems)
            {

                var price = item.Shoe.Price * 100;
                

                var order_Lines = new KlarnaPost.Order_Lines()
                {
                    type = "physical",
                    reference = "1337-GBG",
                    name = item.Shoe.Name + "," + item.Size,
                    quantity = 1,
                    quantity_unit = "pcs",
                    unit_price = price,
                    tax_rate = 2500,
                    total_amount = price,
                    total_discount_amount = 0,
                    total_tax_amount = 0.2m * (price)
                };
                order_lines_array.Add(order_Lines);
            }
       

            
    

            var merchant = new KlarnaPost.Merchant_Urls()
            {
                terms = @"https://www.example.com/terms.html",
                checkout = @"https://www.example.com/checkout.html",
                confirmation = @"https://localhost:44383/OrderConfirmed/{checkout.order.id}",
                push = @"https://www.example.com/api/push"
            };



            var root = new KlarnaPost.Rootobject()
            {
                purchase_country = "SE",
                purchase_currency = "SEK",
                locale = "en-GB",
                order_amount = _shoppingCart.GetShoppingCartTotal()*100,
                order_tax_amount = _shoppingCart.GetShoppingCartTotalTax()*100,
                order_lines = order_lines_array.ToArray(),
                merchant_urls = merchant,
                merchant_reference1 = id.ToString() 

            };

            var jsonContent = JsonConvert.SerializeObject(root, Formatting.Indented);
            request.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            Debug.WriteLine(jsonContent);

            var result =  client.SendAsync(request);

            var resultString = result.Result.Content.ReadAsStringAsync();

            Debug.WriteLine(resultString.Result);

            var klarna = JsonConvert.DeserializeObject<Rootobject>(resultString.Result);

            Debug.WriteLine(klarna);

            klarna.merchant_urls.confirmation = $"https://localhost:44383/OrderConfirmed/{klarna.order_id}";
            myModel.Klarna = klarna;
            
            return View("Klarna", klarna);
        }


    }
}

