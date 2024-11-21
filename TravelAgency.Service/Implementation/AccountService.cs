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
using FluentValidation;
using TravelAgency.Domain.Validators;
using System.Linq;

namespace TravelAgency.Service.Implementation
{
    public class AccountService : IAccountService
    {
        private readonly IBaseStorage<UsersDb> _UserStorage;
        private IMapper _mapper { get; set; }

        private UserValidator _validationRules { get; set; }

        MapperConfiguration mapperConfiguration = new MapperConfiguration(p =>
        {
            p.AddProfile<AppMappingProfile>();
        });

        public AccountService(IBaseStorage<UsersDb> userStorage)
        {
            _UserStorage = userStorage;
            _mapper = mapperConfiguration.CreateMapper();
            _validationRules = new UserValidator();
        }

        public async Task<BaseResponce<ClaimsIdentity>> Login(User model)
        {
            try
            {
                await _validationRules.ValidateAndThrowAsync(model);

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
            catch (ValidationException ex)
            {
                //Получение сообщений об ошибках
                var errorsMessages = string.Join(";", ex.Errors.Select(e => e.ErrorMessage));

                return new BaseResponce<ClaimsIdentity>()
                {
                    Description = errorsMessages,
                    StatusCode = StatusCode.BadRequest
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

                await _validationRules.ValidateAndThrowAsync(model);

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
            catch (ValidationException ex)
            {
                //Получение сообщений об ошибках валидации
                var errorsMessages = string.Join(";", ex.Errors.Select(e => e.ErrorMessage));

                return new BaseResponce<ClaimsIdentity>()
                {
                    Description = errorsMessages,
                    StatusCode = StatusCode.BadRequest
                };
            }
        }
    }
}
