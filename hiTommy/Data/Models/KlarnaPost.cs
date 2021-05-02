using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloTommy.Models
{
    public class KlarnaPost
    {

        public Rootobject root;

        public class Rootobject
        {
            public string purchase_country { get; set; }
            public string purchase_currency { get; set; }
            public string locale { get; set; }
            public decimal order_amount { get; set; }
            public decimal order_tax_amount { get; set; }
            public Order_Lines[] order_lines { get; set; }
            public Merchant_Urls merchant_urls { get; set; }
            public string merchant_reference1 { get; set; }
        }

        public class Merchant_Urls
        {
            public string terms { get; set; }
            public string checkout { get; set; }
            public string confirmation { get; set; }
            public string push { get; set; }
        }
        

        public class Order_Lines
        {
            public string type { get; set; }
            public string reference { get; set; }
            public string name { get; set; }
            public int quantity { get; set; }
            public string quantity_unit { get; set; }
            public decimal unit_price { get; set; }
            public decimal tax_rate { get; set; }
            public decimal total_amount { get; set; }
            public int total_discount_amount { get; set; }
            public decimal total_tax_amount { get; set; }
        }

    }
}
