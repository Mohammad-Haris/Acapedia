using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Acapedia.Data.Models
{
    public partial class Country
    {
        [Key]
        public string CountryName { get; set; }
    }
}
