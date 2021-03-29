using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloTommy.Models;
using hiTommy.Data.HelperClasses;
using hiTommy.Data.Models;

namespace HelloTommy
{
    public class MailCreator
    {



        public static MailHelper MailInfoCreator(Klarna.Rootobject klarna, Order order)
        {
            return new MailHelper()
            {
                OrderId = order.OrderId.ToString(),
                Orderdate = order.OrderDateTime.ToString(),
                OrderList = order.OrderList,
                ShippingPrice = klarna.shipping_options.price,
                ShippingVat = klarna.shipping_options.tax_amount,
                CustomerName = klarna.billing_address.given_name + klarna.billing_address.family_name,
                CustomerAdress = klarna.billing_address.street_address,
                CustomerPostal = klarna.billing_address.postal_code,
                CustomerPhone = klarna.billing_address.phone,
                CustomerEmail = klarna.billing_address.email,
                CustomerShppingAdress = klarna.shipping_address.street_address,
                CustomerShippingPostal = klarna.shipping_address.postal_code,
                CustomerShippingCity = klarna.shipping_address.city

            };

        }
    }
}
