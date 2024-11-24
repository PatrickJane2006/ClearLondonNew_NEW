using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelAgency.Domain.ViewModels.LoginAndRegistration;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using AutoMapper;
using TravelAgency.Service.Implementation;
using TravelAgency.Domain.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;


namespace TravelAgency.Controllers
{ 
        public class HomeController : Controller
        {
            public readonly ILogger<HomeController> _logger;

            public IActionResult SiteInformation()
            {
                return View();
            }

            public IActionResult Privacy()
            {
                return View();
            }

            [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
            public IActionResult Error()
            {
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }


                private readonly IAccountService _accountService;

                private IMapper _mapper { get; set; }


                MapperConfiguration mapperConfiguration = new MapperConfiguration(p =>
                {
                        p.AddProfile<AppMappingProfile>();
                });

                public HomeController(ILogger<HomeController> logger, IAccountService accountService)
                {
                        _accountService = accountService;
                        _logger = logger;
                        _mapper = mapperConfiguration.CreateMapper();
                }
            
            [HttpPost]
            public async Task<IActionResult> Login([FromBody] LoginViewModel model)
            {
                if (ModelState.IsValid)
                {
                    var user = _mapper.Map<User>(model);

                    var responce = await _accountService.Login(user);

                    if (responce.StatusCode == Domain.Response.StatusCode.OK)
                    {
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(responce.Data));


                        return Ok(model);
                    }
                        ModelState.AddModelError("", responce.Description);
                        
                }

                var errors = ModelState.Values.SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();

                return BadRequest(errors);
            }

            [HttpPost]
            public async Task<IActionResult> Register([FromBody] LoginViewModel model)
            {
                if (ModelState.IsValid)
                {
                    var user = _mapper.Map<User>(model);

                    var confirm = _mapper.Map<ConfirmEmailViewModel>(model);

                    var code = await _accountService.Register(user);

                    confirm.GeneratedCode = code.Data;

                        return Ok(confirm);
     

                }
                    var errors = ModelState.Values.SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                    return BadRequest(errors);
            }

            [AutoValidateAntiforgeryToken]

            public  async  Task<IActionResult> Logout()
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("SiteInformation", "Home");
            }
        
        }
}
