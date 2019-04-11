using System.ComponentModel.DataAnnotations;

namespace Acapedia.Data.ViewModels.AccountViewModels
{
    public class ExternalLoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email
        {
            get; set;
        }

        public string Avatar
        {
            get; set;
        }

        public string UserName
        {
            get; set;
        }
    }
}
