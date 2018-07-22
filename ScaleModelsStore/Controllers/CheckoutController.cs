using Microsoft.AspNet.Identity;
using ScaleModelsStore.Models;
using ScaleModelsStore.ViewModels;
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

            string userId = User.Identity.GetUserId();
            var addressList = storeDb.Addresses.Where(a => a.UserId == userId).ToList();
            ViewBag.Addresses = new SelectList(Utils.GetAddressDropDown(addressList), "Key", "Value");

            var cartList = ShoppingCart.GetCart(this.HttpContext).GetCartItems();
            if (cartList.Count==0)
            {
                ViewBag.ErrorMessage = "Your cart is empty!";
                return View("Error");
            }

            var list = new SelectList(Utils.CountriesList(),"Key","Value");
            var sortList = list.OrderBy(p => p.Text).ToList();
            
            ViewBag.Countries = sortList;
            ViewBag.DeliveryTypeId = new SelectList(storeDb.DeliveryTypes, "DeliveryTypeId", "DeliveryTypeDrescription");
            return View();
        }

        [HttpPost]
        public ActionResult Index(Order order)
        {
            if(ModelState.IsValid)
            {
                var cart = ShoppingCart.GetCart(this.HttpContext);
                order.OrderOpenDate = DateTime.Now;
                order.OrderStatusId = 1;
                if (!String.IsNullOrWhiteSpace(User.Identity.Name))
                    order.CustomerId = User.Identity.GetUserId();                
                List<Product> results=ProductMaxQuantityCheck.CheckMaxQuantity(cart, order);
                if(results.Count>0)
                {        
                    foreach(var item in results)
                    {
                        cart.RemoveByProductId(item.ProductId);
                    }
                    ViewData["RestrictedProducts"] = results;                    
                    return View("Error");
                }
                storeDb.Orders.Add(order);
                storeDb.SaveChanges();                
                cart.CreateOrder(order);

                TempData["OrderId"] = order.OrderId;
                return RedirectToAction("Complete");
            }

            var list = new SelectList(Utils.CountriesList(), "Key", "Value");
            var sortList = list.OrderBy(p => p.Text).ToList();
            ViewBag.Countries = sortList;
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
                ViewBag.ErrorMessage = "We're sorry, we've hit an unexpected error.";
                return View("Error");
            }
        }

        public ActionResult CheckStatus()
        {
            return View();
        }
         
        [HttpPost]
        public ActionResult CheckStatus(OrderStatusCheckViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            ScaleModelsStoreEntities storeDb = new ScaleModelsStoreEntities();
            var order = storeDb.Orders.SingleOrDefault(o => o.OrderId == model.OrderId&&o.Email==model.Email);
            if(order==null)
            {
                ViewBag.ErrorMessage = "Incorrec order number and/or e-mail address";
                return View("Error");
            }
            var viewModel = new OrderStatusCheckViewModel
            {
                Email=model.Email,
                OrderId = model.OrderId,
                OrderStatus = order.OrderStatuses.StatusDescription
            };

            return RedirectToAction("CheckResult","Checkout",viewModel);
        }

        public ActionResult CheckResult(OrderStatusCheckViewModel model)
        {
            return View(model);
        }

    }
}