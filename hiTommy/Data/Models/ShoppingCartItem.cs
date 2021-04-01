using hiTommy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hiTommy.Data.Models
{
    public class ShoppingCartItem
    {
        public int ShoppingCartItemId { get; set; }
        public Shoe Shoe { get; set; }
        public int Amount { get; set; }
        public string ShoppingCartId { get; set; }
    }
}
