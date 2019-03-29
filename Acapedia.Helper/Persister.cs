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
                            if (String.IsNullOrEmpty(_CurrResults[itr]["snippet"].ToString()) || String.IsNullOrEmpty(_CurrResults[itr]["title"].ToString()))
                            {
                                MetaInformation MetaData = MetaScraper.GetMetaDataFromUrl(_CurrResults[itr]["link"].ToString());

                                if (MetaData.HasData)
                                {
                                    _Context.Add(new WebsiteLink
                                    {
                                        LinkUrl = _CurrResults[itr]["link"].ToString(),
                                        Title = _CurrResults[itr]["title"].ToString().Length > 27 ? _CurrResults[itr]["title"].ToString().Substring(0, 26) + " ..." :
                                        _CurrResults[itr]["title"].ToString(),
                                        Description = MetaData.Description,
                                        LinkCountryName = "Australia",
                                        LinkDisciplineId = "SomeID"
                                    });
                                    continue;
                                }
                            }


                            _Context.Add(new WebsiteLink
                            {
                                LinkUrl = _CurrResults[itr]["link"].ToString(),
                                Title = _CurrResults[itr]["title"].ToString().Length > 27 ? _CurrResults[itr]["title"].ToString().Substring(0, 26) + " ..." :
                                _CurrResults[itr]["title"].ToString(),
                                Description = _CurrResults[itr]["snippet"].ToString(),
                                LinkCountryName = "Australia",
                                LinkDisciplineId = "SomeID"
                            });
                        }

                        _Context.SaveChanges();
                    }

                    else
                    {
                        File.AppendAllText(@"..\Acapedia.Helper\DataFolder\leftovers.txt", "study " + _CurrLine + " universities australia\n");
                    }
                }
            }
        }

        public void PersistDisciplines ()
        {
            using (FileStream _Read = new FileStream(@"..\Acapedia.Helper\DataFolder\discips.txt", FileMode.Open))
            {
                string _CurrLine;

                using (StreamReader __Reader = new StreamReader(_Read))
                {
                    while ((_CurrLine = __Reader.ReadLine()) != null)
                    {
                        _Context.Add(new Discipline
                        {
                            DisciplineName = _CurrLine
                        });
                    }

                    _Context.SaveChanges();
                }
            }
        }
    }
}
