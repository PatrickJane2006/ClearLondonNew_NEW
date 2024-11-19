﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Domain.Models;

namespace TravelAgency.Domain.Helpers
{
    public static class AuthenticateUserHelper
    {
        public static ClaimsIdentity Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Login),
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Role.ToString()),
                new Claim("AvatarPath", user.Path_Img),
            };

            return new ClaimsIdentity(claims, "ApplicationCookie",
                ClaimTypes.Email, ClaimsIdentity.DefaultRoleClaimType);
        }


    }
}
