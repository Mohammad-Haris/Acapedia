using System;
using System.Collections.Generic;

namespace Acapedia.Data.Models
{
    public class Discipline
    {
        public Discipline()
        {
            UniversityDiscipline = new HashSet<UniversityDiscipline>();
        }

        public int DisciplineId { get; set; }
        public string DisciplineName { get; set; }

        public ICollection<UniversityDiscipline> UniversityDiscipline { get; set; }
    }
}
