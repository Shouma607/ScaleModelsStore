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
    public class OrdersController : Controller
    {
        ScaleModelsStoreEntities storeDb = new ScaleModelsStoreEntities();
        // GET: Orders
        public ActionResult Current()
        {
            List<OrderToProduct> products = new List<OrderToProduct>();
            List<OrdersViewModel> viewModel = new List<OrdersViewModel>();
            decimal price;
            var userId = User.Identity.GetUserId();
            var orders = storeDb.Orders.Where(o => o.CustomerId == userId&&o.OrderStatusId<7).ToList();
            foreach(var item in orders)
            {
                price = decimal.Zero;

                foreach (var product in item.OrderToProducts.ToList())
                {
                    price += product.Product.Price*product.Quantity;
                }
                viewModel.Add(new OrdersViewModel { Order = item, Price = price });
            }

            return View(viewModel);
        }

        public ActionResult History()
        {
            List<OrderToProduct> products = new List<OrderToProduct>();
            List<OrdersViewModel> viewModel = new List<OrdersViewModel>();
            decimal price;
            var userId = User.Identity.GetUserId();
            var orders = storeDb.Orders.Where(o => o.CustomerId == userId && o.OrderStatusId >= 7).ToList();
            foreach (var item in orders)
            {
                price = decimal.Zero;

                foreach (var product in item.OrderToProducts.ToList())
                {
                    price += product.Product.Price * product.Quantity;
                }
                viewModel.Add(new OrdersViewModel { Order = item, Price = price });
            }

            return View(viewModel);
        }

        public ActionResult Details(int id)
        {
            var order = storeDb.Orders.SingleOrDefault(o => o.OrderId == id);         
            List<ProductDetails> products = new List<ProductDetails>();
            decimal price;
            foreach(var item in order.OrderToProducts)
            {
                price = decimal.Zero;
                price = item.Product.Price * item.Quantity;
                products.Add(new ProductDetails { ProductName = item.Product.ProductName,UnitPrice=item.Product.Price,Quantity=item.Quantity, Price = price });
            }
            var viewModel = new OrderDetailsViewModel
            {
                Order = order,
                Products = products
            };

            return View(viewModel);
        }
    }
}