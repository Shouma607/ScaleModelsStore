using ScaleModelsStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ScaleModelsStore.Controllers
{
    public class CheckoutController : Controller
    {
        ScaleModelsStoreEntities storeDb = new ScaleModelsStoreEntities();
        // GET: Checkout
        public ActionResult Index()
        {
            var cartList = ShoppingCart.GetCart(this.HttpContext).GetCartItems();
            if (cartList.Count==0)
            {
                ViewBag.ErrorMessage = "Your cart is empty!";
                return View("Error");
            }
            
            ViewBag.DeliveryTypeId = new SelectList(storeDb.DeliveryTypes, "DeliveryTypeId", "DeliveryTypeDrescription");
            return View();
        }

        [HttpPost]
        public ActionResult Index(Order order)
        {
            if(ModelState.IsValid)
            {
                order.OrderOpenDate = DateTime.Now;
                order.OrderStatusId = 1;
                storeDb.Orders.Add(order);
                storeDb.SaveChanges();
                var cart = ShoppingCart.GetCart(this.HttpContext);
                cart.CreateOrder(order);

                TempData["OrderId"] = order.OrderId;
                return RedirectToAction("Complete");
            }
            ViewBag.DeliveryTypeId = new SelectList(storeDb.DeliveryTypes, "DeliveryTypeId", "DeliveryTypeDrescription");
            return View(order);
        }

        public ActionResult Complete()
        {
            if (TempData["OrderId"] != null)
            {
                ViewBag.OrderId = TempData["OrderId"];
                return View();
            }
            else
            {
                ViewBag.ErrorMessage = "We're sorry, we.ve hit an unexpected error.";
                return View("Error");
            }
        }
    }
}