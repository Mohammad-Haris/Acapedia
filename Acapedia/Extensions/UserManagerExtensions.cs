using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Acapedia.Data.Models;
using System.Security.Claims;
using Microsoft.Extensions.Options;

namespace Acapedia.Extensions
{
    public static class UserManagerExtensions
    {
        public static string GetAvatar(this UserManager<ApplicationUser> manager, ClaimsPrincipal principal)
		{
			if (principal == null)
			{
				throw new ArgumentNullException("principal");
			}

			return principal.FindFirstValue(ClaimTypes.Name);			
		}
    }
}
