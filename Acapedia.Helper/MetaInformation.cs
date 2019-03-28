using System;
using System.Collections.Generic;
using System.Text;

namespace Acapedia.Helper
{
    public class MetaInformation
    {
        public bool HasData { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public MetaInformation (string url)
        {
            Url = url;
            HasData = false;
        }

        public MetaInformation (string url, string title, string description)
        {
            Url = url;
            Title = title;
            Description = description;
        }
    }
}
