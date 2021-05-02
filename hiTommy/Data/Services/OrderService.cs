using System;
using System.Collections.Generic;
using System.Linq;
using hiTommy.Data.Models;
using hiTommy.Data.ViewModels;
using hiTommy.Models;

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

        public Order AddEmptyOrderAndReturnEmptyOrder(Order order)
        {
            _context.Add(order);
            _context.SaveChanges();
            return order;
        }


        public void UpdateOrder(string OrderId, Customers customer, List<Shoe>shoes)
        {
            var order = GetOrderById(int.Parse(OrderId));
            order.OrderDateTime = DateTime.Now;
            order.CustomerId = customer.Id;
            order.Customer = customer;
            order.OrderList = shoes;
            _context.Order.Update(order);
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

        public void AddOrderRows(List<Shoe> orderList,int OrderId)
        {
            var orderRows = new List<OrderRows>();
            foreach (var item in orderList)
            {
                orderRows.Add(
                new OrderRows()
                {
                    OrderItemName = item.Name,
                    OrderItemPrice = item.Price.ToString(),
                    OrderItemType = item.ToString(),
                    OrderId = OrderId
                }
                );
            }


            _context.OrderRows.AddRange(orderRows);
        }
    }
}