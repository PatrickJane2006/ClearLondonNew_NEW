using FluentValidation;
using TravelAgency.Domain.Models;

namespace TravelAgency.Domain.Validators
{
    public class OrderValidator : AbstractValidator<Order>
    {
        public OrderValidator() 
        {
            RuleFor(order => order.Id_User).NotEmpty().WithMessage("Id пользователя обязателен");
            RuleFor(order => order.Id_Service).NotEmpty().WithMessage("Id услуги обязателен");
            RuleFor(order => order.Price).NotEmpty().WithMessage("Цена должна быть больше нуля");   
        }


    }
}
