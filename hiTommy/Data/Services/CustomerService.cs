using System.Collections.Generic;
using System.Linq;
using hiTommy.Data.Models;
using hiTommy.Data.ViewModels;

namespace hiTommy.Data.Services
{
    public class CustomerService
    {
        private readonly HiTommyApplicationDbContext _context;

        public CustomerService(HiTommyApplicationDbContext context)
        {
            _context = context;
        }

        public void AddCustomer(CustomerVm customer)
        {
            var _customer = new Customers
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                Address = customer.Address,
                PostalCode = customer.PostalCode,
                TelephoneNumber = customer.TelephoneNumber,
                City = customer.City,
                Password = ""
            };
            _context.Customers.Add(_customer);
            _context.SaveChanges();
        }

        public List<Customers> GetAllCustomers()
        {
            return _context.Customers.ToList();
        }

        public Customers GetCustomerById(int customerId)
        {
            return _context.Customers.FirstOrDefault(n => n.Id == customerId);
        }
        public Customers GetCustomerByEmail(string email)
        {
            return _context.Customers.FirstOrDefault(n => n.Email == email);

        }

        public Customers UpdateCustomerById(int customerId, CustomerVm customer)
        {
            var _customer = _context.Customers.FirstOrDefault(n => n.Id == customerId);
            if (_customer is null) return _customer;
            _customer.FirstName = customer.FirstName;
            _customer.LastName = customer.LastName;
            _customer.Email = customer.Email;
            _customer.Address = customer.Address;
            _customer.PostalCode = customer.PostalCode;
            _customer.TelephoneNumber = customer.TelephoneNumber;


            _context.SaveChanges();

            return _customer;
        }

        public void DeleteCustomerById(int customerId)
        {
            var _customer = _context.Customers.FirstOrDefault(n => n.Id == customerId);
            if (_customer is null) return;
            _context.Customers.Remove(_customer);
            _context.SaveChanges();
        }

        public CustomerWithOrdersVm GetOrdersByCustomerId(int customerId)
        {
            var _customer = _context.Customers.Where(n => n.Id == customerId).Select(n => new CustomerWithOrdersVm
            {
                FirstName = n.FirstName,
                LastName = n.LastName,
                Address = n.Address,
                Email = n.Email,
                PostalCode = n.PostalCode,
                TelephoneNumber = n.TelephoneNumber,
                Orders = n.Orders
            }).FirstOrDefault();

            return _customer;
        }
    }
}