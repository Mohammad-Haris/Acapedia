using Acapedia.Data.Models;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Acapedia.Data.Contracts
{
    public interface IExplore
    {
        IEnumerable<WebsiteLink> GetUniversities (JArray _ClientSelection);
        IEnumerable<WebsiteLink> GetOnline (JArray _ClientSelection);
    }
}
