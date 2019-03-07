using System;
using System.Collections.Generic;

namespace Acapedia.Data.Models
{
    public partial class UniversityDiscipline
    {
        public int Id { get; set; }
        public int UniversityId { get; set; }
        public int DisciplineId { get; set; }

        public Discipline Discipline { get; set; }
        public University University { get; set; }
    }
}
