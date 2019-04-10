using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Acapedia.Data.ViewModels.ManageViewModels
{
    public class IndexViewModel
    {
        public string Username
        {
            get; set;
        }

        [Required]
        [EmailAddress]
        public string Email
        {
            get; set;
        }
    }
}
