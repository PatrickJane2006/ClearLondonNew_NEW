using FluentValidation;
using TravelAgency.Domain.Models;

namespace TravelAgency.Domain.Validators
{
    public class RequestValidator : AbstractValidator<Request>
    {
        public RequestValidator() 
        {
            RuleFor(request => request.Id_User).NotEmpty().WithMessage("Id пользователя обязателен");
            RuleFor(request => request.Description).NotEmpty().WithMessage("Описание обязательно");
            RuleFor(request => request.Status).IsInEnum().WithMessage("Статус должен быть допустимым значением перечисления");

        }


    }
}
