using System.Linq;
using Acapedia.Data.Contracts;
using Acapedia.Data;
using Acapedia.Data.Models;
using System.Collections.Generic;
using System;
using Newtonsoft.Json.Linq;
using Acapedia.Data.ViewModels;

namespace Acapedia.Service
{
    public class ExploreService : IExplore
    {
        private readonly AcapediaDbContext _Context;

        public ExploreService (AcapediaDbContext context)
        {
            _Context = context;
        }

        public IEnumerable<WebsiteLink> GetUniversities (JArray _ClientSelection)
        {
            List<string> _Countries = _Context.Country.Where(coun => coun.CountryName != "Online").Select(coun => coun.CountryName).ToList();
            List<string> _Disciplines = _Context.Discipline.Select(discip => discip.DisciplineName).ToList();

            string _Country = _ClientSelection[0].ToString();
            string _Discipline = _ClientSelection[1].ToString();

            if (_Countries.Contains(_Country) && _Disciplines.Contains(_Discipline))
            {
                string _DiscipId = _Context.Discipline.Where(dis => dis.DisciplineName == _Discipline).Select(dis => dis.DisciplineId).FirstOrDefault();
                var _QueryResult = _Context.WebsiteLink.Where(sel => sel.LinkCountryName == _Country).Where(sel => sel.LinkDisciplineId == _DiscipId).Select(sel => sel);

                return _QueryResult.ToList();
            }

            else
            {
                return new List<WebsiteLink>();
            }

        }
    }
}
