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
            bool isAdded=cart.AddToCart(addedProduct);
            if (isAdded)
            {                
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Message = String.Format($"You can not add more than 3 units of {addedProduct.ProductName}");
                return View("Error");
            }   
        }

        [HttpPost]
        public ActionResult RemoveRecord(int id)
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);
            string productName = storeDb.Carts.Single(c => c.RecordId == id).Product.ProductName;

            cart.RemoveRecord(id);

            var results = new ShoppingCartRemoveRecordViewModel
            {
                Message = Server.HtmlEncode(productName) + " has been removed from your cart.",
                CartTotal = cart.GetTotal(),
                CartQuantity = cart.GetQuantity(),                
                DeleteId = id
            };

            return Json(results);
        }

        [HttpPost]
        public ActionResult RemoveUnit(int id)
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);
            string productName = storeDb.Carts.Single(c => c.RecordId == id).Product.ProductName;

            int itemQuantity = cart.RemoveUnit(id);
            var results = new ShoppingCartChangeQuantityViewModel
            {
                Message = Server.HtmlEncode(productName) + " unit has been removed from your cart",
                CartTotal = cart.GetTotal(),
                CartQuantity = cart.GetQuantity(),
                ItemQuantity = itemQuantity,
                ChangeId = id
            };
            return Json(results);
        }

        [HttpPost]
        public ActionResult AddUnit(int id)
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);
            string productName = storeDb.Carts.Single(c => c.RecordId == id).Product.ProductName;

            int itemQuantity = cart.AddUnit(id);
            var results = new ShoppingCartChangeQuantityViewModel
            {
                Message = Server.HtmlEncode(productName) + " unit has been added to your cart",
                CartTotal = cart.GetTotal(),
                CartQuantity = cart.GetQuantity(),
                ItemQuantity = itemQuantity,
                ChangeId = id
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

        [ChildActionOnly]
        public ActionResult Error()
        {
            return PartialView("Error");
        }
    }
}