using System.Collections.Generic;

namespace hiTommy.Data.Models
{
    public class Customer
    {
        //Navigation Properties
        public List<Order> Orders;
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string TelephoneNumber { get; set; }
        public string Passowrd { get; set; }
    }
}