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

        public IEnumerable<WebsiteLink> GetLinks (object _ClientSelection)
        {
            return new List<WebsiteLink>();
        }        
    }
}
