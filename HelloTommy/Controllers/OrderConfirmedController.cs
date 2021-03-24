using System;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
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

        public OrderConfirmedController(IConfiguration config)
        {
            _config = config;
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