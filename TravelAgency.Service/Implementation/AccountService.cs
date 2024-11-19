using System;
using AutoMapper;
using TravelAgency.Domain.ModelsDb;
using TravelAgency.DAL.Interfaces;
using System.Threading.Tasks;
using TravelAgency.Domain.Response;
using TravelAgency.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TravelAgency.Domain.Helpers;

namespace TravelAgency.Service.Implementation
{
    public class AccountService : IAccountService
    {
        private readonly IBaseStorage<UsersDb> _UserStorage;
        private IMapper _mapper { get; set; }

        MapperConfiguration mapperConfiguration = new MapperConfiguration(p =>
        {
            p.AddProfile<AppMappingProfile>();
        });

        public AccountService(IBaseStorage<UsersDb> userStorage)
        {
            _UserStorage = userStorage;
            _mapper = mapperConfiguration.CreateMapper();
        }

        public async Task<BaseResponce<ClaimsIdentity>> Login(User model)
        {
            try
            {
                var userdb = await _UserStorage.GetAll().FirstOrDefaultAsync(x => x.Email == model.Email);

                if (userdb == null)
                {
                    return new BaseResponce<ClaimsIdentity>()
                    {
                        Description = "Пользователь не найден"
                    };
                }

                if (userdb.Password != HashPasswordHelper.HashPassword(model.Password))
                {
                    return new BaseResponce<ClaimsIdentity>()
                    {
                        Description = "Неверный пароль или почта"
                    };
                }

                // ClaimsIdentity для аутентификации
                var claimsIdentity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, model.Email),
                   
                }, "Password");

                return new BaseResponce<ClaimsIdentity>()
                {
                    Data = claimsIdentity, // Передаем ClaimsIdentity
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponce<ClaimsIdentity>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponce<ClaimsIdentity>> Register(User model)
        {
            try
            {
                model.Path_Img = "";
                model.CreatedAt = DateTime.Now;
                model.Password = HashPasswordHelper.HashPassword(model.Password);

                var userdb = _mapper.Map<UsersDb>(model);

                if (await _UserStorage.GetAll().FirstOrDefaultAsync(x => x.Email == model.Email) != null)
                {
                    return new BaseResponce<ClaimsIdentity>()
                    {
                        Description = "Пользователь с такой почтой уже есть",
                    };
                }

                await _UserStorage.Add(userdb);

                // Создайте ClaimsIdentity после регистрации пользователя
                var claimsIdentity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, model.Email),
                 
                }, "Password");

                return new BaseResponce<ClaimsIdentity>()
                {
                    Data = claimsIdentity, // Передаем ClaimsIdentity
                    Description = "Пользователь зарегистрирован",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponce<ClaimsIdentity>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
