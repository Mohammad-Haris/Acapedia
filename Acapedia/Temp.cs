using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Acapedia.Data;
using Acapedia.Data.Models;

namespace Acapedia
{
    public class Temp
    {
        private AcapediaDbContext _DbContext;

        public Temp (AcapediaDbContext context)
        {
            _DbContext = context;
        }

        public void AddCountries ()
        {

        }
    }
}
