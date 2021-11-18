using FluentValidation;
using FluentValidationApp.Web.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace FluentValidationApp.Web.FluentValidators
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public string NotEmptyMessage { get; } = "{PropertyName} alanı boş olamaz";

        public CustomerValidator()
        {


            RuleFor(m => m.Name)
                .NotEmpty();

            RuleFor(m => m.Email)
                .NotEmpty()
                .WithMessage(NotEmptyMessage)
                .EmailAddress().WithMessage("geçerli bir email adresi giriniz");

            RuleFor(m => m.Age)
                .NotEmpty()
                .WithMessage(NotEmptyMessage)
                .ExclusiveBetween(18, 60).WithMessage("yaş aralıgı 18 ile 60 arasında olmalıdır");

            RuleFor(m => m.BirthDay)
                .NotEmpty()
                .WithMessage(NotEmptyMessage)
                .Must(m =>
                {
                    return DateTime.Now.AddYears(-18) >= m;
                }).WithMessage("yaşınız 18 den küçük");

            RuleForEach(m => m.Addresses).SetValidator(new AddressValidator());

        }
    }
}
