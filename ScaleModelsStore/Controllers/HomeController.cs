using ScaleModelsStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ScaleModelsStore.Controllers
{
    public class HomeController : Controller
    {
        ScaleModelsStoreEntities storeDb = new ScaleModelsStoreEntities();

        public ActionResult Index()
        {
            var products = GetNewProducts(5);
            return View(products);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        private List<Product> GetNewProducts(int count)
        {
            return storeDb.Products
                .OrderByDescending(p=>p.ProductId)
                .Take(count)
                .ToList();
        }
    }
}