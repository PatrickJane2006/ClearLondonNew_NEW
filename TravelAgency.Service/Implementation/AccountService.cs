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
using MimeKit;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;
using System.Drawing;
using System.IO;
using System.Net.Mail;
using System.Reflection.PortableExecutable;
using Org.BouncyCastle.Asn1.X509;
using System.Security.Policy;
using MailKit.Net.Smtp;

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

        public async Task<BaseResponce<string>> Register(User model)
        {
            try
            {
                Random random = new Random();
                string confirmationCode = $"{random.Next(10)}{random.Next(10)}{random.Next(10)}{random.Next(10)}";

                if (await _UserStorage.GetAll().FirstOrDefaultAsync(x => x.Email == model.Email) != null)
                {
                    return new BaseResponce<string>()
                    {
                        Description = "Пользователь с такой почтой уже есть",
                    };
                }

                await SendEmail(model.Email, confirmationCode);

                // Создайте ClaimsIdentity после регистрации пользователя
                var claimsIdentity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, model.Email),

                }, "Password");

                return new BaseResponce<string>()
                {
                    Data = confirmationCode, // Передаем ClaimsIdentity
                    Description = "Письмо отправлено",
                    StatusCode = StatusCode.OK
                };
            }
            catch (ValidationException ex)
            {
                //Получение сообщений об ошибках валидации
                var errorsMessages = string.Join(";", ex.Errors.Select(e => e.ErrorMessage));

                return new BaseResponce<string>()
                {
                    Description = errorsMessages,
                    StatusCode = StatusCode.BadRequest
                };
            }
        }


        public async Task SendEmail(string email, string confirmationCode)
        {
            string path = "E:\\Проверка_Почты\\passwordPractice.txt"; var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("Адинистрачия caйтa", "ClearLondonNew@gmail.com")); 
            emailMessage.To.Add(new MailboxAddress("", email)); 
            emailMessage.Subject = "Добро пожаловать!";
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = "<html>" + "<head>" + "<style>" +
                "body { font-family: Arial, sans-serif; background-color: #f2f2f2; }" +
                ".container { max-width: 600px; margin: 0 auto; padding: 20px; background-color: #fff; border-radius: 10px; box-shadow: 0px 0px 10px rgba(0,0,0,0.1); }" +
                ".header { text-align: center; margin-bottom: 20px; }" +
                ".message { font-size: 16px; line - height: 1.6; }" + ".conteiner-code { background-color: #fefefe; padding: 5px; border-radius: 5px; font-weight: bold; }" +
                ".code {text-align: center; }" +
                "</style>" +
                "</head>" +
                "<body>" +
                "<div class='container'>" +
                "<div class='header'><h1>Дoбpо пожаловать на сайт Клининговой компании ClearLondonNew!</h1></div>" +
                "<div class='message'>" +
                "< р > Пожалуйста, введите данный код на сайте, чтобы подтвердить ваш email и завершить регистрацию: </ p > " + " < div class='conteiner-code'><p class='code'>" + confirmationCode + "</p></div>" +
                "</div>" + "</div>" + "</body>" + "</html>"

            };

            using (StreamReader reader = new StreamReader(path))
            {
                string password = await reader.ReadToEndAsync();

                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    await client.ConnectAsync("smtp.gmail.com", 465, true);
                    await client.AuthenticateAsync("Ilyagradinar228@gmail.com", password);
                    await client.SendAsync(emailMessage);

                    await client.DisconnectAsync(true);
                }

            }

        }

        public async Task<BaseResponce<ClaimsIdentity>> ConfirmEmail(User model, string code, string confirmCode)
        {
            try
            {
                if (code != confirmCode)
                {
                    throw new Exception("Неверный код! регистрация не выполнена.");
                }

                model.Path_Img = "/imager/user.png";
                model.CreatedAt = DateTime.Now;
                model.Password = HashPasswordHelper.HashPassword(model.Password);

                await _validationRules.ValidateAndThrowAsync(model);

                var userdb = _mapper.Map<UsersDb>(model);

                await _UserStorage.Add(userdb);

                var result = AuthenticateUserHelper.Authenticate(userdb);

                return new BaseResponce<ClaimsIdentity>
                {
                    Data = result,
                    Description = "Объект добавился",
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


   

