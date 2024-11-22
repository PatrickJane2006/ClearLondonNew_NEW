using FluentValidation;
using TravelAgency.Domain.Models;


namespace TravelAgency.Domain.Validators
{
    public class ServiceValidator : AbstractValidator<Service>
    {
        public ServiceValidator() 
        {
            RuleFor(service => service.Id_country).NotEmpty().WithMessage("Id страны обязательно");
            RuleFor(service => service.City).NotEmpty().WithMessage("Город обязателен");
            RuleFor(service => service.Name_Service).NotEmpty().WithMessage("Название услуги обязательно");
            RuleFor(service => service.Cleaning_Office_Price).GreaterThan(0).WithMessage("Цена уборки офиса должна быть больше нуля");
            RuleFor(service => service.Cleaning_Garden_Price).GreaterThan(0).WithMessage("Цена уборки сада должна быть больше нуля");
            RuleFor(service => service.Cleaning_Apartment_Price).GreaterThan(0).WithMessage("Цена уборки квартиры должна быть больше нуля");
            RuleFor(service => service.Cleaning_Area_Price).GreaterThan(0).WithMessage("Цена очистки территории должна быть больше нуля");
            RuleFor(service => service.Cleaning_Home_Price).GreaterThan(0).WithMessage("Цена уборки дома должна быть больше нуля");
            RuleFor(service => service.Cleaning_Construction_Price).GreaterThan(0).WithMessage("Цена уборки стройматериалов должна быть больше нуля");
        }



    }
}
