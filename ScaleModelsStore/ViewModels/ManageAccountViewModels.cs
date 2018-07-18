using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ScaleModelsStore.ViewModels
{
    public class UserInfoViewModel
    {
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Display(Name = "Phone number")]
        [RegularExpression(@"(\+\d{1,2}\s?)?(\(?\d{3}\)?)?[\s]?\d{3}[\s.-]?\d{2}[\s.-]?\d{2}",
            ErrorMessage = "Phone number is not valid")]
        public string PhoneNumber { get; set; }

        [EmailAddress]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        public string EmailStatus { get; set; }

        [Display(Name = "Postal code")]
        public string PostalCode { get; set; }

        [Display(Name = "Country")]
        public string Country { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name ="Address")]
        public string Address { get; set; }        
    }

    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name ="Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100,ErrorMessage ="The {0} must be at least {2} characters long",MinimumLength =6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword",ErrorMessage ="The new password and confirmation do not match")]
        public string ConfirmPassword { get; set; }
    }

    public class ChangeEmailViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Current e-mail address")]
        public string OldEmail { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "New e-mail address")]
        public string NewEmail { get; set; }

        [EmailAddress]
        [Display(Name = "Confirm e-mail address")]
        [Compare("NewEmail", ErrorMessage = "The new e-mail and confirmation do not match")]
        public string ConfirmEmail { get; set; }
    }

    public class ChangePhoneViewModel
    {
        [Required]
        [RegularExpression(@"(\+\d{1,2}\s?)?(\(?\d{3}\)?)?[\s]?\d{3}[\s.-]?\d{2}[\s.-]?\d{2}",
            ErrorMessage = "Phone number is not valid")]
        [Display(Name = "Current phone number")]
        public string OldPhone { get; set; }

        [Required]
        [RegularExpression(@"(\+\d{1,2}\s?)?(\(?\d{3}\)?)?[\s]?\d{3}[\s.-]?\d{2}[\s.-]?\d{2}",
            ErrorMessage = "Phone number is not valid")]
        [Display(Name = "New phone number")]
        public string NewPhone { get; set; }
    }

    public class ChangeAddressViewModel
    {
        [StringLength(10)]
        public string PostalCode { get; set; }

        [StringLength(50)]
        public string Country { get; set; }

        [StringLength(50)]
        public string City { get; set; }

        [StringLength(100)]
        public string Address { get; set; }
    }
}