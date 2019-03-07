using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Acapedia.Data.Models
{
    public partial class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Contribution = new HashSet<Contribution>();
        }

        public ICollection<Contribution> Contribution { get; set; }
    }
}
