using System;
using System.Net;
using System.Net.Mail;
using hiTommy.Data.Services;
using hiTommy.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace HelloTommy.Controllers
{
    [Route("Contact")]
    public class ContactController : Controller
    {
        private readonly IConfiguration _config;
        private readonly MailServices _mailservices;

        public ContactController(IConfiguration config,MailServices mailServices)
        {
            _config = config;
            _mailservices = mailServices;
        }

        public IActionResult Index()
        {
            
            return View(new EmailViewModel());
        }

        [HttpPost]
        public ActionResult SendEmail(EmailViewModel _emailVM)
        {
            if (ModelState.IsValid)
            {
                _mailservices.ContactEmail(_emailVM);
            }
   
            return View("Index");
        }
    }
}