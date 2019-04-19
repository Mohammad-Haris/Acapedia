using Acapedia.Data.Contracts;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Acapedia.Controllers
{
    public class ExploreController : Controller
    {
        private readonly IExplore _ExploreService;
        private readonly IRateLimit _RateLimit;

        public ExploreController (IExplore explore, IRateLimit limit)
        {
            _ExploreService = explore;
            _RateLimit = limit;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult GetUniversities ([FromBody] JArray _Data)
        {
            if (!_RateLimit.LimitRate())
            {
                var _Unis = _ExploreService.GetUniversities(_Data);
                JArray _ToClient = new JArray();

                foreach (var _Uni in _Unis)
                {
                    var _Juni = new JObject();
                    _Juni.Add("Link", _Uni.LinkUrl);
                    _Juni.Add("Title", _Uni.Title);
                    _Juni.Add("Description", _Uni.Description);

                    _ToClient.Add(_Juni);
                }
                
                return Content(_ToClient.ToString());
            }

            return Content("");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult GetOnline ([FromBody] JArray _Data)
        {
            if (!_RateLimit.LimitRate())
            {
                var _Unis = _ExploreService.GetOnline(_Data);
                JArray _ToClient = new JArray();

                foreach (var _Uni in _Unis)
                {
                    var _Juni = new JObject();
                    _Juni.Add("Link", _Uni.LinkUrl);
                    _Juni.Add("Title", _Uni.Title);
                    _Juni.Add("Description", _Uni.Description);

                    _ToClient.Add(_Juni);
                }

                return Content(_ToClient.ToString());
            }

            return Content("");
        }

        public IActionResult Index ()
        {
            return View();
        }
    }
}