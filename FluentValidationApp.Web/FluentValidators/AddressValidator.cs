using FluentValidation;
using FluentValidationApp.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FluentValidationApp.Web.FluentValidators
{
    public class AddressValidator : AbstractValidator<Address>
    {
        public AddressValidator()
        {
            RuleFor(m => m.Content).NotEmpty();
            RuleFor(m => m.Province).NotEmpty();
            RuleFor(m => m.PostCode)
                .NotEmpty()
                .MaximumLength(5);


        }
    }
}
