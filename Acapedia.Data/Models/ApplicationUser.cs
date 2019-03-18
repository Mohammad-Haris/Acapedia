using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Acapedia.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Contribution = new HashSet<Contribution>();
        }

        public string Avatar
        {
            get; set;
        }

        public ICollection<Contribution> Contribution { get; set; }
    }
}
