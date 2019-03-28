using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Acapedia.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Avatar
        {
            get; set;
        }
    }
}
