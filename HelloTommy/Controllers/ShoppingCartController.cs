using HelloTommy.Models;
using hiTommy.Data.Models;
using hiTommy.Data.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloTommy.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly ShoeServices shoeServices;
        private readonly ShoppingCart shoppingCart;

        public ShoppingCartController(ShoeServices shoeServices, ShoppingCart shoppingCart)
        {
            this.shoeServices = shoeServices;
            this.shoppingCart = shoppingCart;
        }



        public IActionResult Index()
        {
            var items = shoppingCart.GetShoppingCartItems();
            shoppingCart.ShoppingCartItems = items;

            var shoppingCartViewModel = new ShoppingCartViewModel
            {
                ShoppingCart = shoppingCart,
                ShoppingCartTotal = shoppingCart.GetShoppingCartTotal()
            };

            return View(shoppingCartViewModel);
        }

        public IActionResult AddToShoppingCart(int Id, int size)
        {

            var selectedShoe = shoeServices.GetShoeById(Id);

            if (selectedShoe != null)
            {
                shoppingCart.AddToCart(selectedShoe, 1, size);
            }

            return RedirectToAction($"{Id}", "Product");

        }

        public RedirectToActionResult RemoveFromShoppingCart(int Id)
        {
            var selectedShoe = shoeServices.GetShoeById(Id);
            if (selectedShoe != null)
            {
                shoppingCart.RemoveFromCart(selectedShoe);
            }

            return RedirectToAction("Index");

        }
    }
}
