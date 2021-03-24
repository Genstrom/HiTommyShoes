using System;
using System.Collections.Generic;
using System.Linq;
using hiTommy.Data.Models;
using hiTommy.Data.ViewModels;

namespace hiTommy.Data.Services
{
    public class OrderService
    {
        private readonly HiTommyApplicationDbContext _context;

        public OrderService(HiTommyApplicationDbContext context)
        {
            _context = context;
        }

        public List<Order> GetAllOrders()
        {
            return _context.Order.ToList();
        }

        public void AddOrder(OrderVm order)
        {
            var _order = new Order
            {
                CustomerId = order.CustomerId,
                Customer = _context.Customers.FirstOrDefault(n => n.Id == order.CustomerId),
                OrderDateTime = DateTime.Now
            };
            _context.Order.Add(_order);
            _context.SaveChanges();
        }

        public Order GetOrderById(int id)
        {
            return _context.Order.FirstOrDefault(n => n.OrderId == id);
        }

        public void DeleteOrderById(int orderId)
        {
            var _order = _context.Order.FirstOrDefault(n => n.OrderId == orderId);
            if (_order is null) return;
            _context.Order.Remove(_order);
            _context.SaveChanges();
        }
    }
}