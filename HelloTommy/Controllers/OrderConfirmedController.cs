using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
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
        private readonly ShoeServices _shoesService;
        private readonly QuantityService _quantityService;
        private readonly OrderService _orderService;
        private readonly CustomerService _customerService;
        private readonly IConfiguration _config;
        private readonly MailServices _mailHelper;

        public OrderConfirmedController(MailServices mailServices,CustomerService customerService, OrderService orderService,IConfiguration config, ShoeServices shoesService,  QuantityService quantityService)
        {
            _shoesService = shoesService;
            _config = config;
            _quantityService = quantityService;
            _orderService = orderService;
            _customerService = customerService;
            _mailHelper = mailServices;
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
            var shoeName = shoeArray[0];
            var size = int.Parse(shoeArray[1]);
            var _shoe = _shoesService.GetShoeByName(shoeName);
             //_quantityService.RemoveQuantityOnShoeByIdAndSize(_shoe.Id, size);
            
           
         

            var customer = new CustomerVm()
            {
                
                FirstName = klarna.billing_address.given_name,
                LastName = klarna.billing_address.family_name,
                Email = klarna.billing_address.email,
                PostalCode = klarna.billing_address.postal_code,
                Address = klarna.billing_address.street_address,
                City = klarna.billing_address.city,
                TelephoneNumber = klarna.billing_address.phone,
                
                
            };
            var _orderList = new List<Shoe>();
            _orderList.Add(_shoe);
            _customerService.AddCustomer(customer);
            var _customer = _customerService.GetCustomerByEmail(klarna.billing_address.email);
            var order = new OrderVm()
            {
                OrderDateTime = DateTime.Now,
                CustomerId = _customer.Id,
                OrderList = _orderList
            };
            _orderService.AddOrder(order);
            _mailHelper.OrderConfirmationMail(_shoe, _customer);
       


            return View(klarna);
        }

        [HttpPost]
        public IActionResult Index(Shoe Shoe, string name, string email, string message, string subject)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var senderEmail = new MailAddress(_config["SenderEmail"], "HiTommy Order");
                    var receiverEmail = new MailAddress(_config["EmailName"], "Receiver");
                    var password = _config["EmailPasswword"];
                    var sub = subject;
                    var body = $"From Name: {name} Email:{email} \n{message}";
                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(senderEmail.Address, password)
                    };
                    using (var mess = new MailMessage(senderEmail, receiverEmail)
                    {
                        Subject = sub,
                        Body = body
                    })
                    {
                        smtp.Send(mess);
                    }

                    return View();
                }
            }
            catch (Exception)
            {
                ViewBag.Error = "Some Error";
            }

            return View();
        }
    }
}