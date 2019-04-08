using Acapedia.Data.ViewModels.ErrorViewModels;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Acapedia.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index ()
        {
            var ErrorInfo = new ErrorViewModel();

            ErrorInfo.StatusCode = HttpContext.Response.StatusCode.ToString();
            ErrorInfo.Description = Microsoft.AspNetCore.WebUtilities.ReasonPhrases.GetReasonPhrase(HttpContext.Response.StatusCode);            

            return View(ErrorInfo);
        }
    }
}