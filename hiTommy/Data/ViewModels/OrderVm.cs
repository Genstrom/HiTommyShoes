using System;
using System.Collections.Generic;
using hiTommy.Models;

namespace hiTommy.Data.ViewModels
{
    public class OrderVm
    {
      
        public int CustomerId { get; set; }
        public DateTime OrderDateTime { get; set; }
        public List<Shoe> OrderList { get; set; }
    }
}