using System.Threading.Tasks;
using TravelAgency.Domain.Response;
using TravelAgency.Domain.Models;
using System;

namespace TravelAgency.Service.Implementation
{
    public interface IAccountService
    {
        Task<BaseResponce<User>> Register(User model);
        Task<BaseResponce<User>> Login(User model);

    }


   

}
