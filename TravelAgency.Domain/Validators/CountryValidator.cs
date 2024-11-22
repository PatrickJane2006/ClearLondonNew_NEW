using FluentValidation;
using TravelAgency.Domain.Models;

namespace TravelAgency.Domain.Validators
{
    public class CountryValidator : AbstractValidator<Country>
    {
        public CountryValidator() 
        {
            RuleFor(country => country.Path_img).NotEmpty().WithMessage("Путь изображения обязателен");
            RuleFor(country => country.Count_Services).GreaterThanOrEqualTo(0).WithMessage("Количество услуг не может быть отрицательным");
        }
        

    }
}
