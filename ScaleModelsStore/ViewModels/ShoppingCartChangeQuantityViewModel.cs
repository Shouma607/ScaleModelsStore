using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScaleModelsStore.ViewModels
{
    public class ShoppingCartChangeQuantityViewModel
    {
        public string Message { get; set; }
        public decimal CartTotal { get; set; }
        public int CartQuantity { get; set; }
        public int ItemQuantity { get; set; }
        public int ChangeId { get; set; }
    }
}