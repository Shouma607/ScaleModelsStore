using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScaleModelsStore.Models
{
    public class OrderToProducts
    {        
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }        
    }
}