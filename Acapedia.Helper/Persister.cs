using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;
using Acapedia.Data;
using Acapedia.Data.Models;

namespace Acapedia.Helper
{
    public class Persister
    {
        private AcapediaDbContext _Context;

        public Persister (AcapediaDbContext context)
        {
            _Context = context;
        }

        public void PersistLinks ()
        {
            var DJObject = JObject.Parse(File.ReadAllText(@"..\Acapedia.Helper\DataFolder\country-links.json"));
            string _CurrLine;
            JArray _CurrResults;
            int _Length;

            using (FileStream _Read = new FileStream(@"..\Acapedia.Helper\DataFolder\discips.txt", FileMode.Open))
            {
                using (StreamReader _Reader = new StreamReader(_Read))
                {
                    _CurrLine = _Reader.ReadLine();

                    if (DJObject["study " + _CurrLine + " universities australia"] != null
                        && DJObject["study " + _CurrLine + " universities australia"]["results"] != null)
                    {
                        _CurrResults = (JArray)DJObject["study " + _CurrLine + " universities australia"]["results"];
                        _Length = _CurrResults.Count;
                        Console.WriteLine("Inside");

                        for (int itr = 0; itr < _Length; itr++)
                        {
                            _Context.Add(new WebsiteLink
                            {
                                LinkUrl = _CurrResults[itr]["link"].ToString(),
                                Title = _CurrResults[itr]["title"].ToString(),
                                Description = _CurrResults[itr]["title"].ToString(),
                                LinkCountryName = "Australia",
                                LinkDisciplineId = "SomeID"
                            });
                        }

                        _Context.SaveChanges();
                    }

                    else
                    {
                        File.AppendAllText(@"..\Acapedia.Helper\DataFolder\leftovers.txt", "study " + _CurrLine + " universities australia\n");
                        //MetaInformation MetaData = MetaScraper.GetMetaDataFromUrl(DJObject["study " + _Reader.ReadLine() + " universities australia"]["results"]
                        //    [0]["link"].ToString());
                    }
                }
            }
        }
    }
}
