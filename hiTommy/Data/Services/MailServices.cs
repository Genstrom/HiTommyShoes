using hiTommy.Data.Models;
using hiTommy.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hiTommy.Data.Services
{
    public class MailServices
    {
        private readonly IConfiguration _config;

        public MailServices(IConfiguration config)
        {
            _config = config;
        }

        public void OrderConfirmationMail(Shoe shoe, Customers customer)
        {

        }
    }
}
