using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ScaleModelsStore.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        [StringLength(20)]
        public string Name { get; set; }

        [StringLength(250)]
        public string Description { get; set; }

        public List<Product> Products { get; set; }
    }
}