using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ScaleModelsStore.Models
{
    public class OrderToProduct
    {   
        [Key, Column(Order = 0)]
        public int OrderId { get; set; }
        [Key, Column(Order = 1)]
        public int ProductId { get; set; }
        public int Quantity { get; set; }        
    }
}