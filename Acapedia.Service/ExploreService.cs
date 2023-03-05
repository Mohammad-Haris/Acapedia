using System;
using System.Collections.Generic;
using System.Linq;
using Acapedia.Data;
using Acapedia.Data.Contracts;
using Acapedia.Data.ViewModels.ExploreViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json.Linq;

namespace Acapedia.Service
{
    public class ExploreService : IExplore
    {
        private readonly AcapediaDbContext _Context;
        private readonly IMemoryCache _memoryCache;

        public ExploreService(AcapediaDbContext context, IMemoryCache memoryCache)
        {
            _Context = context;
            _memoryCache = memoryCache;
        }

        public IEnumerable<WebsiteLinkModel> GetUniversities(JArray _ClientSelection)
        {
            var country = _ClientSelection[0].ToString();
            var discipline = _ClientSelection[1].ToString();
            var cacheKey = GetCacheKey(country, discipline);

            if (_memoryCache.TryGetValue(cacheKey, out List<WebsiteLinkModel> cacheValue))
            {
                return cacheValue;
            }

            var _Countries = _Context.Country.AsNoTracking().Where(coun => coun.CountryName != "Online").Select(coun => coun.CountryName.ToLower());
            var _Disciplines = _Context.Discipline.AsNoTracking().Select(discip => discip.DisciplineName.ToLower());

            if (_Countries.Any(x => x == country.ToLower()) && _Disciplines.Any(x => x == discipline.ToLower()))
            {
                string discipId =
                    _Context.Discipline.AsNoTracking().Where(dis => dis.DisciplineName == discipline).Select(dis => dis.DisciplineId).FirstOrDefault();

                IQueryable<WebsiteLinkModel> queryResult =
                    _Context.WebsiteLink.AsNoTracking().Where(sel => sel.LinkCountryName == country).Where(sel => sel.LinkDisciplineId == discipId)
                        .Select(sel => new WebsiteLinkModel
                        {
                            LinkUrl = sel.LinkUrl,
                            Title = sel.Title,
                            Description = sel.Description
                        });

                MemoryCacheEntryOptions cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromHours(12))
                    .SetAbsoluteExpiration(TimeSpan.FromHours(24));

                cacheValue = queryResult.ToList();

                _memoryCache.Set(cacheKey, cacheValue, cacheEntryOptions);
            }

            if (cacheValue != default)
            {
                return cacheValue;
            }

            return new List<WebsiteLinkModel>();
        }

        public IEnumerable<WebsiteLinkModel> GetOnline(JArray _ClientSelection)
        {
            var country = "Online";
            var discipline = _ClientSelection[0].ToString();
            var cacheKey = GetCacheKey(country, discipline);

            if (_memoryCache.TryGetValue(cacheKey, out List<WebsiteLinkModel> cacheValue))
            {
                return cacheValue;
            }

            var _Disciplines = _Context.Discipline.AsNoTracking().Select(discip => discip.DisciplineName.ToLower());

            if (_Disciplines.Any(x => x == discipline.ToLower()))
            {
                string _DiscipId =
                    _Context.Discipline.AsNoTracking().Where(dis => dis.DisciplineName == discipline).Select(dis => dis.DisciplineId).FirstOrDefault();

                IQueryable<WebsiteLinkModel> _QueryResult =
                    _Context.WebsiteLink.AsNoTracking().Where(sel => sel.LinkCountryName == country).Where(sel => sel.LinkDisciplineId == _DiscipId)
                        .Select(sel => new WebsiteLinkModel
                        {
                            LinkUrl = sel.LinkUrl,
                            Title = sel.Title,
                            Description = sel.Description
                        });

                MemoryCacheEntryOptions cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromHours(12))
                    .SetAbsoluteExpiration(TimeSpan.FromHours(24));

                cacheValue = _QueryResult.ToList();

                _memoryCache.Set(cacheKey, cacheValue, cacheEntryOptions);
            }

            if (cacheValue != default)
            {
                return cacheValue;
            }

            return new List<WebsiteLinkModel>();
        }

        private string GetCacheKey(string country, string discipline)
        {
            return $"{country}--{discipline}";
        }
    }
}