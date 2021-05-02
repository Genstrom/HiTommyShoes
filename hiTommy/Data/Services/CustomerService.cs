using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloTommy.Models;
using hiTommy.Data.HelperClasses;
using hiTommy.Data.Models;
using hiTommy.Data.Services;
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

        public void CreateCustomerVm(Klarna.Rootobject klarna)
        {
            var customerVm = new CustomerVm
            {
                FirstName = klarna.billing_address.given_name,
                LastName = klarna.billing_address.family_name,
                Email = klarna.billing_address.email,
                PostalCode = klarna.billing_address.postal_code,
                Address = klarna.billing_address.street_address,
                City = klarna.billing_address.city,
                TelephoneNumber = klarna.billing_address.phone
            };
            AddCustomer(customerVm);
        }
    }
}