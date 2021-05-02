using hiTommy.Data.HelperClasses;
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
using hiTommy.Data.ViewModels;
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

        public void OrderConfirmationMail(MailHelper mailHelper)
        {
            var senderEmail = new MailAddress(_config["EmailName"], "HelloTommyShoes");
            var receiverEmail = new MailAddress(mailHelper.CustomerEmail, "Receiver");
            var password = _config["EmailPassword"];
            var sub = $"Order: {mailHelper.OrderId} Confirmed";
            var body = PopulateBody(mailHelper);
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

        public void OrderReceivedEmail(MailHelper mailHelper)
        {
           
                var senderEmail = new MailAddress(_config["SenderEmail"], "HelloTommyShoes");
                var receiverEmail = new MailAddress(_config["EmailName"], "Receiver");
                var password = _config["EmailPassword"];
                var sub = $"New Order: #{mailHelper.OrderId} Received";
                var body = PopulateBody(mailHelper);
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

        private string PopulateBody(MailHelper helper)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(@"C:\Users\tommy\Desktop\Projekt\hiTommyMain\hiTommyShoes\HelloTommy\EmailTemplates\OrderReceived.html"))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{ProductName}", helper.OrderList[0].Name);
            body = body.Replace("{ProductQuantity}", helper.OrderList.Count().ToString());
            body = body.Replace("{ProductPrice}", helper.OrderList[0].Price.ToString());
            body = body.Replace("{OrderDate}", helper.Orderdate);
            body = body.Replace("{ImageUrl}", helper.OrderList[0].PictureUrl);
            body = body.Replace("{ProductSize}", helper.ProductSize.ToString());
            return body;
        }

        public void ContactEmail(EmailViewModel emailVM)
        {
            
            var senderEmail = new MailAddress(_config["EmailName"], "HelloTommyShoes");
            var receiverEmail = new MailAddress(_config["SenderEmail"], "HelloTommyShoes");
            var password = _config["EmailPassword"];
            var sub = emailVM.Subject;
            var body = $"From Name: {emailVM.Name} Email:{emailVM.Email} \n{emailVM.Message}";
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
        }

    }
}
