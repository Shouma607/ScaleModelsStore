using FluentValidation;
using FluentValidation.Attributes;
using ScaleModelsStore.Models;
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

        [EmailAddress(ErrorMessage = "E-mail address is not valid")]
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
        [Required(ErrorMessage = "Current password is required")]
        [DataType(DataType.Password)]
        [Display(Name ="Current password")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "New password is required")]
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
        [Required(ErrorMessage = "Old e-mail address is required")]
        [EmailAddress(ErrorMessage = "E-mail address is not valid")]
        [Display(Name = "Current e-mail address")]
        public string OldEmail { get; set; }

        [Required(ErrorMessage = "New e-mail address is required")]
        [EmailAddress(ErrorMessage = "E-mail address is not valid")]
        [Display(Name = "New e-mail address")]
        public string NewEmail { get; set; }

        [EmailAddress(ErrorMessage = "E-mail address is not valid")]
        [Display(Name = "Confirm e-mail address")]
        [Compare("NewEmail", ErrorMessage = "The new e-mail and confirmation do not match")]
        public string ConfirmEmail { get; set; }
    }

    public class ChangePhoneViewModel
    {
        [Required(ErrorMessage = "Old phone number is required")]
        [RegularExpression(@"(\+\d{1,2}\s?)?(\(?\d{3}\)?)?[\s]?\d{3}[\s.-]?\d{2}[\s.-]?\d{2}",
            ErrorMessage = "Phone number is not valid")]
        [Display(Name = "Current phone number")]
        public string OldPhone { get; set; }

        [Required(ErrorMessage = "New Phone number is required")]
        [RegularExpression(@"(\+\d{1,2}\s?)?(\(?\d{3}\)?)?[\s]?\d{3}[\s.-]?\d{2}[\s.-]?\d{2}",
            ErrorMessage = "Phone number is not valid")]
        [Display(Name = "New phone number")]
        public string NewPhone { get; set; }
    }

    [Validator(typeof(AddAddressViewModelValidator))]
    public class AddAddressViewModel
    {
        [Display(Name ="Short desription")]
        [StringLength(20, ErrorMessage = "Length must be less than 20 characters")]
        public string Description { get; set; }

        [StringLength(10, ErrorMessage = "Length must be less than 10 characters")]
        public string PostalCode { get; set; }

        [StringLength(50, ErrorMessage = "Length must be less than 50 characters")]
        public string Country { get; set; }

        [StringLength(50, ErrorMessage = "Length must be less than 50 characters")]
        public string City { get; set; }

        [StringLength(100, ErrorMessage = "Length must be less than 100 characters")]
        public string Address { get; set; }
    }

    public class AddAddressViewModelValidator : AbstractValidator<AddAddressViewModel>
    {
        public AddAddressViewModelValidator()
        {
            RuleFor(o => o.Description).NotEmpty().WithMessage("Short description is required")
                                        .Must(isUnique).WithMessage("Description is already exists");
            RuleFor(o => o.PostalCode).NotEmpty().WithMessage("Postal code is required");
            RuleFor(o => o.Country).NotEmpty().WithMessage("Country is required");
            RuleFor(o => o.City).NotEmpty().WithMessage("City is required");
            RuleFor(o => o.Address).NotEmpty().WithMessage("Address is required");
        }

        private bool isUnique(string value)
        {
            ScaleModelsStoreEntities storeDb = new ScaleModelsStoreEntities();

            var address = storeDb.Addresses.SingleOrDefault(x => x.ShortDescription.ToLower() == value.ToLower());

            if (address == null)
                return true;
            return false;
        }
    }

    public class EditAddressViewModel
    {
        [Required(ErrorMessage = "There is nothing to edit")]
        [Display(Name = "Short desription")]       
        public string Description { get; set; }

        [Required(ErrorMessage ="Postal code is required")]
        [StringLength(10, ErrorMessage = "Length must be less than 10 characters")]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "Country is required")]
        [StringLength(50, ErrorMessage = "Length must be less than 50 characters")]
        public string Country { get; set; }

        [Required(ErrorMessage = "City is required")]
        [StringLength(50, ErrorMessage = "Length must be less than 50 characters")]
        public string City { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [StringLength(100, ErrorMessage = "Length must be less than 100 characters")]
        public string Address { get; set; }
    }

}