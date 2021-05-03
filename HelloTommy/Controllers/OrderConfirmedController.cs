using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using hiTommy.Data.Models;
using hiTommy.Data.Services;
using hiTommy.Data.ViewModels;
using hiTommy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using static HelloTommy.Models.Klarna;

namespace HelloTommy.Controllers
{
    [Route("OrderConfirmed")]
    public class OrderConfirmedController : Controller
    {
        private readonly IConfiguration _config;
        private readonly CustomerService _customerService;
        private readonly MailServices _mailHelper;
        private readonly OrderService _orderService;
        private readonly QuantityService _quantityService;
        private readonly ShoeServices _shoesService;
        private readonly MailCreator _mailCreator;

        public OrderConfirmedController(MailServices mailServices, CustomerService customerService,
            OrderService orderService, IConfiguration config, ShoeServices shoesService,
            QuantityService quantityService)
        {
            _shoesService = shoesService;
            _config = config;
            _quantityService = quantityService;
            _orderService = orderService;
            _customerService = customerService;
            _mailHelper = mailServices;
            _mailCreator = new MailCreator(orderService);
            
        }

        [Route("{order_id?}")]
        [HttpGet]
        public ActionResult Index(string order_id)
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri("https://api.playground.klarna.com/");

            var request = new HttpRequestMessage(HttpMethod.Get, $"checkout/v3/orders/{order_id}");
            request.Content = new StringContent("", Encoding.UTF8, "application/json");
            client.DefaultRequestHeaders.Add("Authorization", "Basic " + _config["KlarnaAuth"]);
            var result = client.SendAsync(request);
            var resultString = result.Result.Content.ReadAsStringAsync();
            var klarna = JsonConvert.DeserializeObject<Rootobject>(resultString.Result);
            var shoeArray = klarna.order_lines[0].name.Split(',');
            var shoeId = int.Parse(shoeArray[0]);
            var size = int.Parse(shoeArray[1]);
            var _shoe = _shoesService.GetShoeById(shoeId);
            var orderList = new List<Shoe>();
            orderList.Add(_shoe);
            //_quantityService.RemoveQuantityOnShoeByIdAndSize(_shoe.Id, size);

            var customer = _customerService.GetCustomerByEmail(klarna.billing_address.email);
            if (customer == null)
            {
                _customerService.CreateCustomerVm(klarna);
            }
          
            customer = _customerService.GetCustomerByEmail(klarna.billing_address.email);
            
            _orderService.AddOrderRows(orderList, int.Parse(klarna.merchant_reference1));
            _orderService.UpdateOrder(klarna.merchant_reference1, customer,orderList);
            var mailhelper = _mailCreator.MailInfoCreator(klarna, size);
            _mailHelper.OrderConfirmationMail(mailhelper);
            _mailHelper.OrderReceivedEmail(mailhelper);

            return View(klarna);
        }

       
    }
}