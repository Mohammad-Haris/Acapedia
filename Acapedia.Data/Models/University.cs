using System;
using System.Collections.Generic;

namespace Acapedia.Data.Models
{
    public class University
    {
        public University()
        {
            UniversityDiscipline = new HashSet<UniversityDiscipline>();
        }

        public int UniversityId { get; set; }
        public string UniversityName { get; set; }
        public string CountryName { get; set; }

        public Country CountryNameNavigation { get; set; }
        public ICollection<UniversityDiscipline> UniversityDiscipline { get; set; }
    }
}
