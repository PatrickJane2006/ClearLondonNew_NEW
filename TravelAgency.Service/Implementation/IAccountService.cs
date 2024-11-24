using System.Threading.Tasks;
using TravelAgency.Domain.Response;
using TravelAgency.Domain.Models;
using System.Security.Claims;

namespace TravelAgency.Service.Implementation
{
    public interface IAccountService
    {
        Task<BaseResponce<string>> Register(User model);
        Task<BaseResponce<ClaimsIdentity>> Login(User model);
        Task<BaseResponce<ClaimsIdentity>> ConfirmEmail(User model, string code, string confirmCode);

    }


   

}
