using Microsoft.AspNetCore.Mvc;

namespace Acapedia.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index ()
        {
            return View();
        }        
        
        public IActionResult Contact ()
        {
            return View();
        }        
        
        public IActionResult Contribute ()
        {
            return View();
        }        
        
        public IActionResult About ()
        {
            return View();
        }        
        
        public IActionResult PrivacyPolicy ()
        {
            return View();
        }
    }
}
