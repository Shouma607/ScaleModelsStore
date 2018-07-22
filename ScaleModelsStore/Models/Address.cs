using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ScaleModelsStore.Models
{
    public class Address
    {
        [Key]
        public int RecordId { get; set; }

        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; }

        [Required]
        [Index(IsUnique =true)]
        [StringLength(20, ErrorMessage = "Length must be less than 20 characters")]
        public string ShortDescription { get; set; }

        [Required]
        [StringLength(10)]        
        public string PostalCode { get; set; }

        [Required]
        [StringLength(50)]
        public string Country { get; set; }

        [Required]
        [StringLength(50)]
        public string City { get; set; }

        [Required]
        [StringLength(100)]
        public string AddressString { get; set; }

        public virtual User User { get; set; }
    }
}