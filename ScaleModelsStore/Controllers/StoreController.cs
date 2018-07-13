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
        public ActionResult Index(FormCollection values)
        {
            var products = storeDb.Products.ToList();

            int CategoryId=0, ManufacturerId=0;
            string Scale=null;
            if (!String.IsNullOrEmpty(values["FilterByCategory"]))
                CategoryId = Int32.Parse(values["FilterByCategory"]);
            if (!String.IsNullOrEmpty(values["FilterByManufacturer"]))
                ManufacturerId = Int32.Parse(values["FilterByManufacturer"]);
            if (!String.IsNullOrEmpty(values["FilterByScale"]))
                Scale = "1/" + values["FilterByScale"];

            if (CategoryId != 0)
                products=products.Where(p => p.CategoryId == CategoryId).ToList();
            if (ManufacturerId != 0)
                products=products.Where(p => p.ManufacturerId == ManufacturerId).ToList();
            if (Scale != null)
                products=products.Where(p => p.Scale == Scale).ToList();
            
            ViewBag.SortOptions = Utils.GetSortOptions();
            ViewBag.FilterOptionCategories = new SelectList(storeDb.Categories, "CategoryId", "Name");
            ViewBag.FilterOptionManufacturers = new SelectList(storeDb.Manufacturers, "ManufacturerId", "Name");
            ViewBag.FilterOptionScale = Utils.GetScalesList(products);
            return View(products);
        }

        //GET: /Store/Category/Category?categoryName={value}
        public ActionResult Category(string categoryName)
        {
            var category = storeDb.Categories.Include("Products").Single(c => c.Name == categoryName);

            ViewBag.SortOptions = Utils.GetSortOptions();
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