using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Acapedia.Data.Contracts;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Acapedia.Controllers
{
    public class ExploreController : Controller
    {
        private readonly IExplore _ExploreService;

        public ExploreController (IExplore service)
        {
            _ExploreService = service;
        }

        public IActionResult GetUniversities ([FromBody] JArray _Data)
        {
            var _Unis =  _ExploreService.GetUniversities(_Data);
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

        public IActionResult GetOnline ([FromBody] JArray _Data)
        {
            var _Unis =  _ExploreService.GetOnline(_Data);
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

        public IActionResult Index ()
        {
            return View();
        }
    }
}