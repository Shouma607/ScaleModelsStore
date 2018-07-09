using ScaleModelsStore.Models;
using ScaleModelsStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ScaleModelsStore.Controllers
{
    public class ShoppingCartController : Controller
    {
        ScaleModelsStoreEntities storeDb = new ScaleModelsStoreEntities();

        // GET: ShoppingCart
        public ActionResult Index()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);

            var viewModel = new ShoppingCartViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal()
            };
            return View(viewModel);
        }

        public ActionResult AddToCart(int id)
        {
            var addedProduct = storeDb.Products.Single(p => p.ProductId == id);
            var cart = ShoppingCart.GetCart(this.HttpContext);
            cart.AddToCart(addedProduct);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult RemoveRecord(int id)
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);
            string productName = storeDb.Carts.Single(c => c.RecordId == id).Product.ProductName;

            int itemQuantity = cart.RemoveRecord(id);

            var results = new ShoppingCartRemoveRecordViewModel
            {
                Message = Server.HtmlEncode(productName) + " has been removed from your cart.",
                CartTotal = cart.GetTotal(),
                CartQuantity = cart.GetQuantity(),
                ItemQuantity = itemQuantity,
                DeleteId = id
            };

            return Json(results);
        }

        [ChildActionOnly]
        public ActionResult CartSummary()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);

            ViewData["CartQuantity"] = cart.GetQuantity();

            return PartialView("CartSummary");
        }
    }
}