using Microsoft.AspNetCore.Mvc;

namespace Acapedia.Controllers
{
    [Route("[controller]")]
    public class NotSupportedController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}