using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Acapedia.Data.Models;
using Acapedia.Data;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

namespace Acapedia.Extensions
{
    public static class UserManagerExtensions
    {
        public static string GetAvatar (this UserManager<ApplicationUser> manager, ClaimsPrincipal principal, AcapediaDbContext context)
        {            
            return context.ApplicationUser.Where(user => user.Id == manager.GetUserId(principal)).Select(user => user.Avatar).SingleOrDefault();
        }

        public static string GetEmail (this UserManager<ApplicationUser> manager, ClaimsPrincipal principal, AcapediaDbContext context)
        {            
            return context.ApplicationUser.Where(user => user.Id == manager.GetUserId(principal)).Select(user => user.Email).SingleOrDefault();
        }
    }
}
