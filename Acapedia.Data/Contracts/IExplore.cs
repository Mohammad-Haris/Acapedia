using Acapedia.Data.Models;
using System.Collections.Generic;

namespace Acapedia.Data.Contracts
{
    public interface IExplore
    {
        IEnumerable<WebsiteLink> GetLinks (object _ClientSelection);
    }
}
