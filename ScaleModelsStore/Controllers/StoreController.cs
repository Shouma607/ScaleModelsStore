using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ScaleModelsStore.Models;

namespace ScaleModelsStore.Controllers
{
    public class StoreController : Controller
    {
        ScaleModelsStoreEntities storeDb = new ScaleModelsStoreEntities();
        
        // GET: /Store/Index
        public ActionResult Index()
        {
            var products = storeDb.Products.ToList();
            return View(products);
        }

        //GET: /Store/Category/Category?categoryName={value}
        public ActionResult Category(string categoryName)
        {
            var category = storeDb.Categories.Include("Products").Single(c => c.Name == categoryName);
            return View(category);
        }

        //GET: /Store/Product/{id}
        public ActionResult Product(int id)
        {
            var product = storeDb.Products.Find(id);
            return View(product);
        }

        [ChildActionOnly]
        public ActionResult CategoryMenu()
        {
            var categories = storeDb.Categories.ToList();
            return PartialView(categories);
        }
    }
}