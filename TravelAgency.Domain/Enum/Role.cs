using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TravelAgency.Domain.Enum
{
    public enum Role
    {
        [Display(Name = "Пользователь")]
        User = 0,
        [Display(Name = "Модератор")]
        Moderator = 1,
        [Display(Name = "Админ")]
        Admin = 2,
    }

    public enum Status
    {
        [Description("Не рассмотрено")]
        NotConsidered = 0,

        [Description("Одобрено")]
        Approved,

        [Description("Отказано")]
        Denied,

    }


}
