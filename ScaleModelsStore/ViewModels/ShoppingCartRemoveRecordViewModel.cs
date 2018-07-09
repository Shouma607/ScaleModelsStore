using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScaleModelsStore.ViewModels
{
    public class ShoppingCartRemoveRecordViewModel
    {
        public string Message { get; set; }
        public decimal CartTotal { get; set; }
        public int CartQuantity { get; set; }
        public int ItemQuantity { get; set; }
        public int DeleteId { get; set; }
    }
}