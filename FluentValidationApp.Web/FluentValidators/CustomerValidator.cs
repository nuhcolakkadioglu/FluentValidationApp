using FluentValidation;
using FluentValidationApp.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FluentValidationApp.Web.FluentValidators
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(m => m.Name)
                .NotEmpty()
                .WithMessage("name alanı boş olamaz");

            RuleFor(m => m.Email)
                .NotEmpty()
                .WithMessage("email alanı boş olamaz")
                .EmailAddress().WithMessage("geçerli bir email adresi giriniz");

            RuleFor(m => m.Age)
                .NotEmpty()
                .WithMessage("yaş alanı boş oalamaz")
                .ExclusiveBetween(18, 60).WithMessage("yaş aralıgı 18 ile 60 arasında olmalıdır"); 

        }
    }
}
