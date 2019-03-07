using Acapedia.Data.Models;
using System.Collections.Generic;

namespace Acapedia.Data.Contracts
{
    public interface IExplore
    {
        IEnumerable<University> GetUniversities (object _ClientSelection);
        IEnumerable<University> InsertUniversities (object _Universities);
    }
}
