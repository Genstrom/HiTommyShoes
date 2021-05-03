using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hiTommy.Data.Models;
using hiTommy.Data.Services;

namespace HelloTommy.Models
{
    public class Klarna
    {

        public class Rootobject
        {
            public string order_id { get; set; }
            public string status { get; set; }
            public string purchase_country { get; set; }
            public string purchase_currency { get; set; }
            public string locale { get; set; }
            public Billing_Address billing_address { get; set; }
            public Customer customer { get; set; }
            public Shipping_Address shipping_address { get; set; }
            public int order_amount { get; set; }
            public int order_tax_amount { get; set; }
            public Order_Lines[] order_lines { get; set; }
            public Merchant_Urls merchant_urls { get; set; }
            public string html_snippet { get; set; }
            public DateTime started_at { get; set; }
            public DateTime last_modified_at { get; set; }
            public Options options { get; set; }
            public object[] external_payment_methods { get; set; }
            public object[] external_checkouts { get; set; }
            public string merchant_reference1 { get; set; }
            public Shipping_Options shipping_options { get; set; }
        }
        
        
        public class Billing_Address
        {
            public string country { get; set; }
            public string email { get; set; }
            public string given_name { get; set; }
            public string family_name { get; set; }
            public string street_address { get; set; }
            public string postal_code { get; set; }
            public string city { get; set; }
            public string phone { get; set; }
        }

        public class Customer
        {
        }

        public class Shipping_Address
        {
            public string street_address { get; set; }
            public string postal_code { get; set; }
            public string city { get; set; }
            public string country { get; set; }
        }

        public class Shipping_Options
        {
            public decimal price { get; set; }
            public decimal tax_amount { get; set; }
        }

        public class Merchant_Urls
        {
            public string terms { get; set; }
            public string checkout { get; set; }
            public string confirmation { get; set; }
            public string push { get; set; }
        }

        public class Options
        {
            public bool allow_separate_shipping_address { get; set; }
            public bool date_of_birth_mandatory { get; set; }
            public bool require_validate_callback_success { get; set; }
        }

        public class Order_Lines
        {
            public string type { get; set; }
            public string reference { get; set; }
            public string name { get; set; }
            public int quantity { get; set; }
            public string quantity_unit { get; set; }
            public decimal unit_price { get; set; }
            public int tax_rate { get; set; }
            public int total_amount { get; set; }
            public int total_discount_amount { get; set; }
            public int total_tax_amount { get; set; }
        }
        
        
        public static KlarnaPost.Rootobject CreateKlarnaRootObject(ShoppingCart _shoppingCart, OrderService _orderService, List<ShoppingCartItem> items, int id )
        {
            
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
                order_amount = _shoppingCart.GetShoppingCartTotal() * 100,
                order_tax_amount = _shoppingCart.GetShoppingCartTotalTax() * 100,
                order_lines = _orderService.CreateOrderLines(items),
                merchant_urls = merchant,
                merchant_reference1 = id.ToString() 
            };

            return root;
        }

    }
}
