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
           
            if (model.FilterByCategoryId!=0)
                products = products.Where(p => p.CategoryId == model.FilterByCategoryId).ToList();
            if (model.FilterByManufacturerId!=0)
                products = products.Where(p => p.ManufacturerId == model.FilterByManufacturerId).ToList();
            if (!String.IsNullOrEmpty(model.FilterByScale))
                products = products.Where(p => p.Scale == "1/" + model.FilterByScale).ToList();

            var viewModel = new ProductListFilterViewModel
            {
                Products = products,
                FilterByCategoryId = model.FilterByCategoryId,
                FilterByManufacturerId = model.FilterByManufacturerId,
                FilterByScale = "1/" + model.FilterByScale
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

            if (model.FilterByManufacturerId!=0)
                products = products.Where(p => p.ManufacturerId == model.FilterByManufacturerId).ToList();
            if (!String.IsNullOrEmpty(model.FilterByScale))
                products = products.Where(p => p.Scale == "1/" + model.FilterByScale).ToList();          
            
            var viewModel = new CategoryFilterViewModel
            {
                Category = category,
                Products = products,
                FilterByManufacturerId = model.FilterByManufacturerId,
                FilterByScale = "1/" + model.FilterByScale
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