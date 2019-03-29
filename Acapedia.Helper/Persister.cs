using System;
using System.IO;
using Newtonsoft.Json.Linq;
using Acapedia.Data;
using Acapedia.Data.Models;
using System.Linq;

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
            string _CurrLine, _DiscipId, _CurrCountry = "Bangladesh";
            JArray _CurrResults;
            int _Length, itr, _DoneCount = 0;

            using (FileStream _Read = new FileStream(@"..\Acapedia.Helper\DataFolder\discips.txt", FileMode.Open))
            {
                using (StreamReader _Reader = new StreamReader(_Read))
                {
                    while ((_CurrLine = _Reader.ReadLine()) != null)
                    {
                        if (DJObject["study " + _CurrLine + " universities " + _CurrCountry.ToLower()] != null
                            && DJObject["study " + _CurrLine + " universities " + _CurrCountry.ToLower()]["results"] != null)
                        {
                            _DiscipId = _Context.Discipline.Where(sel => sel.DisciplineName == _CurrLine).Select(sel => sel.DisciplineId).FirstOrDefault();
                            _CurrResults = (JArray)DJObject["study " + _CurrLine + " universities " + _CurrCountry.ToLower()]["results"];
                            _Length = _CurrResults.Count;

                            for (itr = 0; itr < _Length; itr++)
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
                                            LinkCountryName = _CurrCountry,
                                            LinkDisciplineId = _DiscipId
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
                                    LinkCountryName = _CurrCountry,
                                    LinkDisciplineId = _DiscipId
                                });
                            }
                            _Context.SaveChanges();
                            _DoneCount++;
                            Console.WriteLine(_DoneCount);
                        }

                        else
                        {
                            File.AppendAllText(@"..\Acapedia.Helper\DataFolder\leftovers.txt", "study " + _CurrLine + " universities " + _CurrCountry.ToLower() + 
                                Environment.NewLine);
                        }
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
