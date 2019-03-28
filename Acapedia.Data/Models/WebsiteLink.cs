using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Acapedia.Data.Models
{
    public partial class WebsiteLink
    {
        [Key]
        public string LinkId { get; set; }
        public string LinkUrl { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        
        public string LinkCountryName { get; set; }        
        public string LinkDisciplineId { get; set; }
    }
}
