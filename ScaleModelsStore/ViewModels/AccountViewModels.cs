using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ScaleModelsStore.ViewModels
{
    public class LogInViewModel
    {
        [Required]
        [Display(Name ="E-mail")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name ="Password")]
        public string Password { get; set; }

        [Display(Name ="Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [StringLength(100)]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name ="E-mail")]
        public string Email { get; set; }

        [StringLength(30)]
        [Required(ErrorMessage = "Phone number is required")]        
        [RegularExpression(@"(\+\d{1,2}\s?)?(\(?\d{3}\)?)?[\s]?\d{3}[\s.-]?\d{2}[\s.-]?\d{2}",
            ErrorMessage = "Phone number is not valid")]
        public string Phone { get; set; }

        [Required]
        [StringLength(100,ErrorMessage ="The {0} must be at least {2} characters long.", MinimumLength =6)]
        [DataType(DataType.Password)]
        [Display(Name ="Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name ="Confirm password")]
        [Compare("Password",ErrorMessage ="The password and confirmation do not match")]
        public string ConfirmPassword { get; set; }
    }

}