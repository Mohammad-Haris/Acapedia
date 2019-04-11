using System.ComponentModel.DataAnnotations;

namespace Acapedia.Data.Models
{
    public partial class Discipline
    {
        public string DisciplineId { get; set; }
        [Required]
        public string DisciplineName { get; set; }
    }
}
