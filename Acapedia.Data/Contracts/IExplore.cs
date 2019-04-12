using Acapedia.Data.Models;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Acapedia.Data.ViewModels.ExploreViewModels;

namespace Acapedia.Data.Contracts
{
    public interface IExplore
    {
        IEnumerable<WebsiteLinkModel> GetUniversities (JArray _ClientSelection);
        IEnumerable<WebsiteLinkModel> GetOnline (JArray _ClientSelection);
    }
}
