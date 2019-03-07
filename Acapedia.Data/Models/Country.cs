using System;
using System.Collections.Generic;

namespace Acapedia.Data.Models
{
    public partial class Country
    {
        public Country()
        {
            University = new HashSet<University>();
        }

        public string CountryName { get; set; }

        public ICollection<University> University { get; set; }
    }
}
