using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ScaleModelsStore.Models
{
    public class DeliveryTypesDictionary
    {
        [Key]
        public int DeliveryTypeId { get; set; }
        public string DeliveryTypeDrescription { get; set; }
    }
}