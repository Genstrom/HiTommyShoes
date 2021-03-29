using hiTommy.Data.Models;
using hiTommy.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Web;
using Microsoft.AspNetCore.Http;

namespace hiTommy.Data.Services
{
    public class MailServices
    {
        private readonly IConfiguration _config;

        public MailServices(IConfiguration config)
        {
            _config = config;
            
        }

        public void OrderConfirmationMail(Shoe shoe, Customers customer, string orderId)
        {
            var senderEmail = new MailAddress(_config["EmailName"], "HelloTommyShoes");
            var receiverEmail = new MailAddress(_config[customer.Email], "Receiver");
            var password = _config["EmailPassword"];
            var sub = $"Order: {orderId} Recieved";
            var message = "";
            var body = PopulateBody();
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
                Body = body,
                IsBodyHtml = true,
                Priority = MailPriority.Normal


            })
            {
                smtp.Send(mess);
            }
        }

        //private string GetWebPageContent(string recipient, string customMsg)
        //{
        //    StreamReader objStreamReader = new StreamReader(Server.MapPath("~/ mailtemplate.htm"));
        //    //read html template file
        //    string bodyMsg = objStreamReader.ReadToEnd();
        //    replace the dynamic string at run - time
        //    bodyMsg = bodyMsg.Replace("##recipient##", recipient);
        //    bodyMsg = bodyMsg.Replace("##somecustommessage##", customMsg);
        //    return bodyMsg;
        //}

        private string PopulateBody(string productName, int productQuantity, string url, string description)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(@"C:\Users\tommy\Desktop\Projekt\hiTommyMain\hiTommyShoes\HelloTommy\EmailTemplates\OrderReceived.html"))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{ProductName}", productName);
            body = body.Replace("{ProductQuantity}", productQuantity);
            body = body.Replace("{}", url);
            body = body.Replace("{Description}", description);
            return body;
        }
    }
}
