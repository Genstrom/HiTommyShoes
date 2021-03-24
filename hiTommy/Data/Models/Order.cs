using System;
using System.Collections.Generic;
using hiTommy.Models;

namespace hiTommy.Data.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime OrderDateTime { get; set; }

        //Navigation Properties
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public List<Shoe> OrderList { get; set; }
    }
}