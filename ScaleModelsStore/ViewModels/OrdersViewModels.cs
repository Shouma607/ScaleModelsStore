using ScaleModelsStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScaleModelsStore.ViewModels
{
    public class OrdersViewModel
    {
        public Order Order { get; set; }
        public decimal Price { get; set; }
    }

    public class OrderDetailsViewModel
    {        
        public Order Order { get; set; }
        public List<ProductDetails> Products { get; set; }
    }

    public class ProductDetails
    {
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}