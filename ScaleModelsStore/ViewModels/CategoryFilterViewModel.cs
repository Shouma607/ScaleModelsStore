using ScaleModelsStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScaleModelsStore.ViewModels
{
    public class CategoryFilterViewModel
    {
        public Category Category { get; set; }
        public List<Product> Products { get; set; }
        public string FilterByManufacturerId { get; set; }
        public string FilterByScale { get; set; }
        public bool isFilterShown { get; set; }
    }
}