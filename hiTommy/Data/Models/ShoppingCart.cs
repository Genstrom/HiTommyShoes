using hiTommy.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hiTommy.Data.Models
{
    public class ShoppingCart
    {

        private readonly HiTommyApplicationDbContext _context;
        
        public string ShoppingCartId { get; set; }

        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        private ShoppingCart(HiTommyApplicationDbContext appcontext)
        {
            _context = appcontext;
        }

        public static ShoppingCart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?
                .HttpContext.Session;

            var context = services.GetService<HiTommyApplicationDbContext>();

            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

            session.SetString("CartId", cartId);

            return new ShoppingCart(context) { ShoppingCartId = cartId };

        }

        public void AddToCart(Shoe shoe, int amount, int size)
        {
            
                var shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = ShoppingCartId,
                    Shoe = shoe,
                    Amount = 1,
                    Size = size
                };

                _context.ShoppingCartItems.Add(shoppingCartItem);
            
            _context.SaveChanges();
        }

        public int RemoveFromCart(Shoe shoe)
        {
            var shoppingCartItem = _context.ShoppingCartItems.SingleOrDefault(
                s => s.Shoe.Id == shoe.Id && s.ShoppingCartId == ShoppingCartId);

            var localAmount = 0;

            if (shoppingCartItem != null)
            {
                if(shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                    localAmount = shoppingCartItem.Amount;
                }

                else
                {
                    _context.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }
            _context.SaveChanges();

            return localAmount;
          
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ??
                (ShoppingCartItems =
                _context.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                .Include(s => s.Shoe)
                .ToList());
        }

        public void ClearCart()
        {
            var cartItems = _context
                .ShoppingCartItems
                .Where(cart => cart.ShoppingCartId == ShoppingCartId);

            _context.ShoppingCartItems.RemoveRange(cartItems);

            _context.SaveChanges();
        }

        public decimal GetShoppingCartTotal()
        {
            var total = _context.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                .Select(c => c.Shoe.Price * c.Amount).Sum();
            return total;
        }
        public decimal GetShoppingCartTotalTax()
        {
            var total = _context.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                .Select(c => c.Shoe.Price * c.Amount).Sum();
            return total * (decimal)0.2;
        }

        public List<Shoe> getShoesFromCart(List<ShoppingCartItem> shoppingCartItems)
        {
            var shoeList = new List<Shoe>();
            foreach (var item in shoppingCartItems)
            {
                shoeList.Add(item.Shoe);
            }
            return shoeList;
        }


    }
}
