using System.Security.Claims;
using System.Threading.Tasks;
using Acapedia.Data.Models;
using Acapedia.Data.ViewModels.AccountViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Acapedia.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;
        private readonly string _Redirect = "/Account/Redirect";

        public AccountController (
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login ()
        {
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout ()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult ExternalLogin (string provider)
        {
            // Request a redirect to the external login provider.
            var redirectUrl = Url.Action(nameof(ExternalLoginCallback), "Account");
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties, provider);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback (string remoteError = null)
        {
            if (remoteError != null)
            {
                _logger.LogInformation("External login 'remoteerror' while calling ExternalLoginCallback");
                return RedirectToLocal(_Redirect);
            }
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                _logger.LogInformation("Couldn't get information from external login provider");
                return RedirectToAction(nameof(Login));
            }

            // Sign in the user with this external login provider if the user already has a login.
            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: true, bypassTwoFactor: true);

            if (result.Succeeded)
            {
                _logger.LogInformation("User logged in with {Name} provider.", info.LoginProvider);

                return RedirectToLocal(_Redirect);
            }

            else
            {
                // If the user does not have an account, then create an account.
                
                return await ExternalLoginPersist(new ExternalLoginViewModel 
                { 
                    Email = info.Principal.FindFirstValue(ClaimTypes.Email), 
                    Avatar = info.Principal.FindFirstValue(ClaimTypes.Uri),
                    UserName = info.Principal.FindFirstValue(ClaimTypes.Name) 
                },
                info);
            }
        }

        // how is the anti forgery token being validated without post?
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExternalLoginPersist (ExternalLoginViewModel model, ExternalLoginInfo info)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.UserName.Replace(" ", "_"), Email = model.Email, Avatar = model.Avatar, EmailConfirmed = true };
                var result = await _userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await _userManager.AddLoginAsync(user, info);
                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: true);
                        _logger.LogInformation("User created an account using {Name} provider.", info.LoginProvider);

                        return RedirectToLocal(_Redirect);
                    }
                }

                AddErrors(result);
            }

            return RedirectToLocal(_Redirect);
        }

        public IActionResult Redirect ()
        {
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

        private IActionResult RedirectToLocal (string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

        #endregion
    }
}
