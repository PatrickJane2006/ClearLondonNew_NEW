using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelAgency.Domain.ViewModels.LoginAndRegistration;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace TravelAgency.Controllers
{ 
        public class HomeController : Controller
        {
            public readonly ILogger<HomeController> _logger;

            public HomeController(ILogger<HomeController> logger)
            {
                _logger = logger;
            }

            public IActionResult SiteInformation()
            {
                return View();
            }

            [HttpPost]
            public IActionResult Login([FromBody] LoginViewModel model)
            {
                if (ModelState.IsValid)
                {
                    return Ok(model);
                }
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return BadRequest(errors);
            }

            [HttpPost]
            public IActionResult Register([FromBody] RegisterViewModel model)
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();
                }

                return Ok(model);
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
        }
}
