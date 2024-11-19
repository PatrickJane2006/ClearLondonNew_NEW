using System.Threading.Tasks;
using TravelAgency.Domain.Response;
using TravelAgency.Domain.Models;
using System.Security.Claims;

namespace TravelAgency.Service.Implementation
{
    public interface IAccountService
    {
        Task<BaseResponce<ClaimsIdentity>> Register(User model);
        Task<BaseResponce<ClaimsIdentity>> Login(User model);

    }


   

}
