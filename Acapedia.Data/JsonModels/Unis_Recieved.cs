using System.Collections.Generic;

namespace Acapedia.Data.JsonModels
{
    public class Unis_Recieved
    {
        public string Country
        {
            get; set;
        }

        public string Discipline
        {
            get; set;
        }

        public List<string> Universities
        {
            get; set;
        }
    }
}
