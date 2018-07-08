using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScaleModelsStore.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int OrderDetailsId { get; set; }
        public int CustomerId { get; set; }
        public int DeliveryTypeId { get; set; }
        public DateTime OrderOpenDate { get; set; }
        public string OrderStatus { get; set; }       

        //Unregistered customer
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}