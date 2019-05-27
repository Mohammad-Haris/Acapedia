using System.Collections.Generic;
using Acapedia.Data.ViewModels.ExploreViewModels;
using Newtonsoft.Json.Linq;

namespace Acapedia.Data.Contracts
{
    public interface IExplore
    {
        IEnumerable<WebsiteLinkModel> GetUniversities (JArray _ClientSelection);
        IEnumerable<WebsiteLinkModel> GetOnline (JArray _ClientSelection);
    }
}
