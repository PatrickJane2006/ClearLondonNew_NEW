using System;
using AutoMapper;
using TravelAgency.Domain.ModelsDb;
using TravelAgency.DAL.Interfaces;
using System.Threading.Tasks;
using TravelAgency.Domain.Response;
using TravelAgency.Domain.Models;
using Microsoft.EntityFrameworkCore;

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

        public async Task<BaseResponce<User>> Login(User model)
        {
            try
            {
                var userdb = _mapper.Map<UsersDb>(model);

                if (await _UserStorage.GetAll().FirstOrDefaultAsync(x => x.Email == model.Email) == null)
                {
                    return new BaseResponce<User>()
                    {
                        Description = "Пользователь не найден"
                    };
                }

                if (userdb.Password != model.Password)
                {
                    return new BaseResponce<User>()
                    {
                        Description = "Неверный пароль или почта"
                    };
                }

                return new BaseResponce<User>()
                {
                    Data = model,
                    StatusCode = StatusCode.OK
                };

            }

            catch (Exception ex)
            {
                return new BaseResponce<User>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };

            }
        }

        public async Task<BaseResponce<User>> Register(User model)
        {
            try
            {
                model.Path_Img = "";
                model.CreatedAt = DateTime.Now;

                var userdb = _mapper.Map<UsersDb>(model);

                if (await _UserStorage.GetAll().FirstOrDefaultAsync(x => x.Email == model.Email) == null)
                {
                    return new BaseResponce<User>()
                    {
                        Description = "Пользователь с такой почтой уже есть",
                    };
                }

                await _UserStorage.Add(userdb);

                return new BaseResponce<User>()
                {
                    Data = model,
                    Description = "Пользователь зарегистрирован",
                    StatusCode = StatusCode.OK
                };

            }

            catch (Exception ex)
            {
                return new BaseResponce<User>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };

            }
        }

    }
}
            



    


    

        
    

    





       

