using AutoMapper;
using TravelAgency.Domain.ModelsDb;
using TravelAgency.Domain.Models;
using TravelAgency.Domain.ViewModels.LoginAndRegistration;

namespace TravelAgency.Service.Implementation
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            CreateMap<User, UsersDb>().ReverseMap();

            CreateMap<User, LoginViewModel>().ReverseMap();

            CreateMap<User, RegisterViewModel>().ReverseMap();
        }

    }

}
