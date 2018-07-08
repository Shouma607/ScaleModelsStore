using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ScaleModelsStore.Models
{
    public class Product
    {
        [ScaffoldColumn(false)]
        public int ProductId { get; set; }

        [DisplayName("Category")]
        public int CategoryId { get; set; }

        [DisplayName("Manufacturer")]
        public int ManufacturerId { get; set; }
        
        [StringLength(150)]
        public string ProductName { get; set; }
        
        [StringLength(5)]
        public string Scale { get; set; }

        [StringLength(15)]
        public string Material { get; set; }

        public decimal Price { get; set; }

        [StringLength(300)]
        public string Description { get; set; }

        [DisplayName("Product photo URL")]
        [StringLength(1024)]
        public string ImagePath { get; set; }

        //TODO 1: QuantityLimit      
        public int Quantity { get; set; }

        public virtual Category Category { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }
    }
}