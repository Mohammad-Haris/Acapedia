using System;
using System.Collections.Generic;

namespace Acapedia.Data.Models
{
    public partial class Contribution
    {
        public string ContributionId { get; set; }
        public string ContributionSubject { get; set; }
        public string ContributionContent { get; set; }
        public string AuthorId { get; set; }

        public ApplicationUser Author { get; set; }
    }
}
