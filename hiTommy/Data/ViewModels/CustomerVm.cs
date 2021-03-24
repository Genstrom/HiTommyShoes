using System.Collections.Generic;
using hiTommy.Data.Models;

namespace hiTommy.Data.ViewModels
{
    public class CustomerVm
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string TelephoneNumber { get; set; }
    }

    public class CustomerWithOrdersVm
    {
        public List<Order> Orders;

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string TelephoneNumber { get; set; }
    }
}