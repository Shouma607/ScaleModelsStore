using ScaleModelsStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScaleModelsStore.ViewModels
{
    public class ProductListFilterViewModel
    {
        public List<Product> Products { get; set; }
        public int FilterByCategoryId { get; set; }
        public int FilterByManufacturerId { get; set; }
        public string FilterByScale { get; set; }
        public bool isFilterShown { get; set; }
        public string hiddenSearchResult { get; set; }
    }
}