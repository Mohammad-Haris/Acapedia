using System.Linq;
using Acapedia.Data.Contracts;
using Acapedia.Data;
using Acapedia.Data.Models;
using System.Collections.Generic;
using System;
using Newtonsoft.Json.Linq;
using Acapedia.Data.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

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
            var _Countries = _Context.Country.AsNoTracking().Where(coun => coun.CountryName != "Online").Select(coun => coun.CountryName);
            var _Disciplines = _Context.Discipline.AsNoTracking().Select(discip => discip.DisciplineName);

            string _Country = _ClientSelection[0].ToString();
            string _Discipline = _ClientSelection[1].ToString();

            if (_Countries.ToList().Contains(_Country) && _Disciplines.ToList().Contains(_Discipline))
            {
                string _DiscipId = _Context.Discipline.AsNoTracking().Where(dis => dis.DisciplineName == _Discipline).Select(dis => dis.DisciplineId).FirstOrDefault();
                var _QueryResult = _Context.WebsiteLink.AsNoTracking().Where(sel => sel.LinkCountryName == _Country).Where(sel => sel.LinkDisciplineId == _DiscipId).Select(sel => sel);

                return _QueryResult.ToList();
            }

            else
            {
                return new List<WebsiteLink>();
            }
        }
    }
}