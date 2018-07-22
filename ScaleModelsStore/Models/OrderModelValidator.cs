using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScaleModelsStore.Models
{
    public class OrderModelValidator:AbstractValidator<Order>
    {
        public OrderModelValidator()
        {
            RuleFor(o => o.FirstName).NotNull().WithMessage("First name is required");
            RuleFor(o => o.LastName).NotNull().WithMessage("Last name is required");
            RuleFor(o => o.Phone).NotNull().WithMessage("Phone number is required");
            RuleFor(o => o.Phone)
                .Matches(@"(\+\d{1,2}\s?)?(\(?\d{3}\)?)?[\s]?\d{3}[\s.-]?\d{2}[\s.-]?\d{2}")
                .WithMessage("Phone number is not valid");
            RuleFor(o => o.Email).NotEmpty().WithMessage("E-mail address is required");
            RuleFor(o => o.Email).EmailAddress().WithMessage("E-mail address is is not valid");

            RuleFor(o => o.PostalCode).NotEmpty().When(o => o.DeliveryTypeId != 1)
                                      .WithMessage("Postal code is required");
            RuleFor(o => o.Country).NotEmpty().When(o => o.DeliveryTypeId != 1)
                          .WithMessage("Country is required");
            RuleFor(o => o.City).NotEmpty().When(o => o.DeliveryTypeId != 1)
                          .WithMessage("City is required");
            RuleFor(o => o.Address).NotEmpty().When(o => o.DeliveryTypeId != 1)
                          .WithMessage("Address is required");
        }
    }
}