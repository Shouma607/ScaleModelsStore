using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ScaleModelsStore.Models;
using ScaleModelsStore.ViewModels;

namespace ScaleModelsStore.Controllers
{
    public class StoreController : Controller
    {
        ScaleModelsStoreEntities storeDb = new ScaleModelsStoreEntities();
        
        // GET: /Store/Index
        public ActionResult Index(ProductListFilterViewModel model)
        {
            var products = storeDb.Products.ToList();

            int CategoryId=0, ManufacturerId=0;
            string Scale=null;
           

            if (!String.IsNullOrEmpty(model.FilterByCategoryId))
                CategoryId = Int32.Parse(model.FilterByCategoryId);
            if (!String.IsNullOrEmpty(model.FilterByManufacturerId))
                ManufacturerId = Int32.Parse(model.FilterByManufacturerId);
            if (!String.IsNullOrEmpty(model.FilterByScale))
                Scale = "1/" + model.FilterByScale;            


            if (CategoryId != 0)
                products=products.Where(p => p.CategoryId == CategoryId).ToList();
            if (ManufacturerId != 0)
                products=products.Where(p => p.ManufacturerId == ManufacturerId).ToList();
            if (Scale != null)
                products=products.Where(p => p.Scale == Scale).ToList();            

            var viewModel = new ProductListFilterViewModel
            {
                Products = products,
                FilterByCategoryId = CategoryId.ToString(),
                FilterByManufacturerId = ManufacturerId.ToString(),
                FilterByScale = Scale!=null?Scale.Substring(2):""                
            };

            ViewBag.SortOptions = Utils.GetSortOptions();
            ViewBag.FilterOptionCategories = new SelectList(storeDb.Categories, "CategoryId", "Name");
            ViewBag.FilterOptionManufacturers = new SelectList(storeDb.Manufacturers, "ManufacturerId", "Name");
            ViewBag.FilterOptionScale = Utils.GetScalesList(storeDb.Products.ToList());
            return View(viewModel);
        }

        //GET: /Store/Category/Category?categoryName={value}
        public ActionResult Category(string categoryName, CategoryFilterViewModel model)
        {
            var category = storeDb.Categories.Include("Products").Single(c => c.Name == categoryName);
            List<Product> products = category.Products;

            int ManufacturerId = 0;
            string Scale = null;
           // bool checkStatus = model.isFilterShown;

            if (!String.IsNullOrEmpty(model.FilterByManufacturerId))
                ManufacturerId = Int32.Parse(model.FilterByManufacturerId);
            if (!String.IsNullOrEmpty(model.FilterByScale))
                Scale = "1/" + model.FilterByScale;

            if (ManufacturerId != 0)
                products = products.Where(p => p.ManufacturerId == ManufacturerId).ToList();
            if (Scale != null)
                products = products.Where(p => p.Scale == Scale).ToList();

            var viewModel = new CategoryFilterViewModel
            {
                Category = category,
                Products = products,
                FilterByManufacturerId = ManufacturerId.ToString(),
                FilterByScale = Scale != null ? Scale.Substring(2) : ""
            };

            ViewBag.SortOptions = Utils.GetSortOptions();
            ViewBag.FilterOptionManufacturers = new SelectList(storeDb.Manufacturers, "ManufacturerId", "Name");
            ViewBag.FilterOptionScale = Utils.GetScalesList(storeDb.Products.ToList());
            return View(viewModel);
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