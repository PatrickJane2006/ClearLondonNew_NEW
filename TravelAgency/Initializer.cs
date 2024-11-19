using TravelAgency.Domain.ModelsDb;
using TravelAgency.DAL.Interfaces;
using TravelAgency.DAL.Storage;
using TravelAgency.Service.Implementation;
using System;
using Microsoft.Extensions.DependencyInjection;

namespace TravelAgency
{
    public static class Initializer
    {
        public static void InitializeReposiitories(this IServiceCollection services)
        {
            services.AddScoped<IBaseStorage<UsersDb>, UserStorage>();
        }

        public static void InitializeServices(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();

            services.AddControllersWithViews()
                .AddDataAnnotationsLocalization()
                .AddViewLocalization();
           

        }

    }
}
