using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Acapedia.Data.Models
{
    public class Country
    {
        public Country()
        {
            University = new HashSet<University>();
        }

        [Key]
        public string CountryName { get; set; }

        public ICollection<University> University { get; set; }
    }
}
