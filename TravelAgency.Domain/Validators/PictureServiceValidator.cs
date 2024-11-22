using FluentValidation;
using TravelAgency.Domain.Models;

namespace TravelAgency.Domain.Validators
{
    public class PictureServiceValidator : AbstractValidator<PictureService>
    {
        public PictureServiceValidator() 
        {
            RuleFor(picture => picture.Path_Img).NotEmpty().WithMessage("Путь изображения обязателен");
            RuleFor(picture => picture.Id_Service).NotEmpty().WithMessage("Id услуги обязательно");
        }
    }
}
