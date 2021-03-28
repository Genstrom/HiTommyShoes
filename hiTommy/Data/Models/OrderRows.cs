using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hiTommy.Data.Models
{
    public class OrderRows
    {
        public int Id { get; set; }
        public string OrderItemName { get; set; }
        public string OrderItemType { get; set; }
        public string OrderItemPrice { get; set; }

        //Navigation Properties
        public int OrderId { get; set; }
    }
}
