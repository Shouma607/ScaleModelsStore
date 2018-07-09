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
            return View();
        }
    }
}