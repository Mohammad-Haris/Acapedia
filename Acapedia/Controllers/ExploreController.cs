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

        public IActionResult GetUniversities ([FromBody] object data)
        {
            var unis = _ExploreService.GetUniversities(data);
            JArray _ToClient = new JArray();

            foreach (var uni in unis)
            {
                _ToClient.Add(uni.UniversityName);
            }

            return Content(_ToClient.ToString());
        }

        public IActionResult InsertUniversities ([FromBody] object data)
        {
            var added = _ExploreService.InsertUniversities(data);
            JArray _ToCLient = new JArray();

            foreach (var uni in added)
            {
                _ToCLient.Add(uni.UniversityName);
            }

            return Content(_ToCLient.ToString());
        }

        public IActionResult Humanities ()
        {
            return View();
        }

        public IActionResult Formal_Sciences ()
        {
            return View();
        }

        public IActionResult Natural_Sciences ()
        {
            return View();
        }

        public IActionResult Social_Sciences ()
        {
            return View();
        }

        public IActionResult Professional_And_Applied_Sciences ()
        {
            return View();
        }
    }
}