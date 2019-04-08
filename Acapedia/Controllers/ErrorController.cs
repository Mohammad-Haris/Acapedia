using Acapedia.Data.ViewModels.ErrorViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Acapedia.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index ()
        {
            string _Path = HttpContext.Request.Path.ToString().ToLower();

            if (_Path == "/error/index" || _Path == "/error" || _Path == "/error/" || _Path == "/error/index/")
            {
                return View(new ErrorViewModel
                {
                    StatusCode = "404",
                    Description = "Not Found"
                });
            }

            else
            {
                var ErrorInfo = new ErrorViewModel();

                ErrorInfo.StatusCode = HttpContext.Response.StatusCode.ToString();
                ErrorInfo.Description = Microsoft.AspNetCore.WebUtilities.ReasonPhrases.GetReasonPhrase(HttpContext.Response.StatusCode);

                return View(ErrorInfo);
            }
        }
    }
}