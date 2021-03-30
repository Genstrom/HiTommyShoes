using hiTommy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hiTommy.Data.HelperClasses
{
    public class MailHelper
    {
        public string OrderId { get; set; }
        public string Orderdate { get; set; }
        public List<Shoe> OrderList { get; set; }
        public decimal ShippingPrice { get; set; }
        public decimal ShippingVat { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAdress { get; set; }
        public string CustomerPostal { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerShppingAdress { get; set; }
        public string CustomerShippingPostal { get; set; }
        public string CustomerShippingCity { get; set; }
        public int ProductSize { get; set; }

    }
}
