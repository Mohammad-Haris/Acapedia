using System.Text;
using System.Threading.Tasks;
using Acapedia.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Acapedia.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class ManageController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger _logger;

        public ManageController (
          UserManager<ApplicationUser> userManager,
          ILogger<ManageController> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }
        
        [HttpGet]
        public async Task<IActionResult> Index ()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                _logger.LogError($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
                return View("/Home/Index");
            }

            return View();
        }

        #region Helpers

        private void AddErrors (IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private string FormatKey (string unformattedKey)
        {
            var result = new StringBuilder();
            int currentPosition = 0;
            while (currentPosition + 4 < unformattedKey.Length)
            {
                result.Append(unformattedKey.Substring(currentPosition, 4)).Append(" ");
                currentPosition += 4;
            }
            if (currentPosition < unformattedKey.Length)
            {
                result.Append(unformattedKey.Substring(currentPosition));
            }

            return result.ToString().ToLowerInvariant();
        }

        #endregion
    }
}
