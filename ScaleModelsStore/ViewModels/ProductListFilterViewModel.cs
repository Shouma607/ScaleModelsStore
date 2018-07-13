using ScaleModelsStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScaleModelsStore.ViewModels
{
    public class ProductListFilterViewModel
    {
        List<Product> Products { get; set; }
        string FilterByCategoryId { get; set; }
        string FilterByManufacturerId { get; set; }
        string FilterByScale { get; set; }
    }
}