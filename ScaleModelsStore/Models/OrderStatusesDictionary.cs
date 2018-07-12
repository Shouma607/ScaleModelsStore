using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ScaleModelsStore.Models
{
    public class OrderStatusesDictionary
    {
        [Key]
        public int OrderStatusId { get; set; }
        public string StatusDescription { get; set; }
    }
}