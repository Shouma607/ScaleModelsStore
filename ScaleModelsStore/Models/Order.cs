using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ScaleModelsStore.Models
{
    [FluentValidation.Attributes.Validator(typeof(OrderModelValidator))]
    public class Order
    {       
        public int OrderId { get; set; }
        [ForeignKey("User")]
        public string CustomerId { get; set; }
        public int DeliveryTypeId { get; set; }
        public DateTime OrderOpenDate { get; set; }
        public int OrderStatusId { get; set; }

        //Unregistered customer

        [StringLength(100, ErrorMessage = "Length must be less than 100 characters")]
        public string FirstName { get; set; }

        [StringLength(100, ErrorMessage = "Length must be less than 100 characters")]
        public string LastName { get; set; }

        [StringLength(10, ErrorMessage = "Length must be less than 10 characters")]
        public string PostalCode { get; set; }

        [StringLength(50, ErrorMessage = "Length must be less than 50 characters")]
        public string Country { get; set; }

        [StringLength(50, ErrorMessage = "Length must be less than 50 characters")]
        public string City { get; set; }

        [StringLength(100, ErrorMessage = "Length must be less than 100 characters")]
        public string Address { get; set; }

        [StringLength(30, ErrorMessage = "Length must be less than 30 characters")]
        public string Phone { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public virtual DeliveryTypesDictionary DeliveryTypes { get; set; }
        public virtual OrderStatusesDictionary OrderStatuses { get; set; }
        public virtual List<OrderToProduct> OrderToProducts { get; set; }
        public virtual User User { get; set; }
    }
}