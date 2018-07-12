using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ScaleModelsStore.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int? CustomerId { get; set; }
        public int DeliveryTypeId { get; set; }
        public DateTime OrderOpenDate { get; set; }
        public int OrderStatusId { get; set; }

        //Unregistered customer
        [Required(ErrorMessage = "First name is required")]
        [StringLength(100)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name is required")]
        [StringLength(100)]
        public string LastName { get; set; }

        //TODO: Try to use "Fluent validator"!
        [StringLength(10)]
        public string PostalCode { get; set; }

        [StringLength(50)]
        public string Country { get; set; }

        [StringLength(50)]
        public string City { get; set; }

        [StringLength(100)]
        public string Address { get; set; }

        [StringLength(30)]
        [Required(ErrorMessage = "Phone number is required")]        
        [RegularExpression(@"(\+\d{1,2}\s?)?(\(?\d{3}\)?)?[\s]?\d{3}[\s.-]?\d{2}[\s.-]?\d{2}",
            ErrorMessage = "Phone number is not valid")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "E-mail address is required")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}",
            ErrorMessage = "Email is is not valid.")]
        public string Email { get; set; }

        public virtual DeliveryTypesDictionary DeliveryTypes { get; set; }
        public virtual OrderStatusesDictionary OrderStatuses { get; set; }
        public virtual List<OrderToProduct> OrderToProducts { get; set; }
    }
}