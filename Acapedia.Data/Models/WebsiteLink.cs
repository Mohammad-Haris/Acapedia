using System.ComponentModel.DataAnnotations;

namespace Acapedia.Data.Models
{
    public partial class WebsiteLink
    {
        [Key]
        public string LinkId { get; set; }

        [Required]
        public string LinkUrl { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        
        [Required]
        public string LinkCountryName { get; set; }        
        [Required]
        public string LinkDisciplineId { get; set; }
    }
}
