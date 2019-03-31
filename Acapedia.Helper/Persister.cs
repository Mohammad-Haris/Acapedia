using System;
using System.IO;
using Newtonsoft.Json.Linq;
using Acapedia.Data;
using Acapedia.Data.Models;
using System.Linq;
using System.Text.RegularExpressions;
using System.Globalization;

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
            string _CurrLine, _DiscipId, _CurrCountry = "United States";
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

        /// <summary>
        /// Needs to be modified according to input text
        /// </summary>
        public void PersistLinksLeftOvers ()
        {
            var DJObject = JObject.Parse(File.ReadAllText(@"..\Acapedia.Helper\DataFolder\results.json"));
            string _CurrLine, _DiscipId, _CurrCountry;
            string[] _Discips = { "Indian history", "Indonesian history", "Applies Philosophy", "Geology", "Indian history", "Applies Philosophy", "Philosophy of history", "Philosophy of Physical Sciences", "Systems philosophy", "Contemporary philosophy", "Meta-philosophy", "Ontology", "Geology", "Indian history", "Applies Philosophy", "Geology", "Arts", "Performing Arts", "Music", "Accompanying", "Chamber Music", "Church Music", "Conducting", "Choral conducting", "Orchestral conducting", "Wind ensemble conducting", "Early music", "Jazz Studies", "Musical Composition", "Music Education", "Music History", "Musicology", "Historical musicology", "Systematic musicology", "Ethnomusicology", "Music theory", "Orchestral studies", "Organology", "Organ and historical keyboards", "Piano", "Strings, harp, oud, and guitar (outline)", "Singing", "Woodwinds, brass, and percussion", "Recording", "Dance", "Choreography", "Dance notation", "Ethnochoreology", "History of dance", "Television", "Theatre", "Acting", "Directing", "Dramaturgy", "Musical theatre", "Playwrighting", "Puppetry", "Scenography", "Stage design", "Indian history", "Applies Philosophy", "Epistemology", "Justification", "Scholasticism", "Philosophical logic", "Australian studies", "Geology", "Indian history germany", "Applies Philosophy germany", "Geology germany", "Geology", "Indian history", "Applies Philosophy", "Ecology", "Agroecology", "Ethnoecology", "Landscape ecology", "Endocrinology", "Evolutionary biology", "Genetics", "Behavioural genetics", "Molecular genetics", "Population genetics", "Histology", "Human biology", "Immunology (outline)", "Limnology", "Linnaean taxonomy", "Marine biology", "Mathematical biology", "Microbiology", "Molecular biology", "Mycology", "Neuroscience", "Nutrition", "Paleobiology", "Paleontology", "Parasitology", "Pathology", "Anatomical pathology", "Clinical pathology", "Dermatopathology", "Forensic pathology", "Hematopathology", "Histopathology", "Molecular pathology", "Surgical pathology", "Physiology", "Human physiology", "Exercise physiology", "Structural Biology", "Systematics", "Systems biology", "Virology", "Molecular virology", "Xenobiology", "Zoology", "Animal communications", "Apiology", "Arachnology", "Carcinology", "Cetology", "Entomology", "Forensic entomology", "Ethnozoology", "Ethology", "Geology", "Stochastic process", "Geometry and Topology", "Affine geometry", "Algebraic geometry", "Algebraic topology", "Convex geometry", "Differential topology", "Discrete geometry", "Finite geometry", "Galois geometry", "General topology", "Geometric topology", "Noncommutative geometry", "Non-Euclidean geometry", "Projective geometry", "Number theory", "Algebraic number theory", "Analytic number theory", "Arithmetic combinatorics", "Geometric number theory", "Applied mathematics", "Approximation theory", "Combinatorics", "Coding theory", "Dynamical systems", "Chaos theory", "Fractal geometry", "Game theory", "Graph theory", "Quantum field theory", "Quantum gravity", "String theory", "Quantum mechanics", "Operations research", "Assignment problem", "Decision analysis", "Dynamic programming", "Inventory theory", "Linear programming", "Mathematical optimization", "Optimal maintenance", "Real options analysis", "SchedulingIndian history", "Indonesian history", "Applies Philosophy", "Philosophy of economics", "Ethics", "Bioethics", "Moral psychology, Descriptive ethics, Value theory", "Normative ethics", "Ancient philosophy", "Geology", "Applies Philosophy", "Australian studies", "Geology" };
            JArray _CurrResults;
            int _Length, itr, _DoneCount = 0, _DiscipCount = 0;

            using (FileStream _Read = new FileStream(@"..\Acapedia.Helper\DataFolder\disciplines-remaining-to-scrape.txt", FileMode.Open))
            {
                using (StreamReader _Reader = new StreamReader(_Read))
                {
                    while ((_CurrLine = _Reader.ReadLine()) != null)
                    {
                        if (DJObject[_CurrLine] != null
                            && DJObject[_CurrLine]["results"] != null)
                        {
                            _DiscipId = _Context.Discipline.Where(sel => sel.DisciplineName == _Discips[_DiscipCount]).Select(sel => sel.DisciplineId).FirstOrDefault();

                            if (String.IsNullOrEmpty(_DiscipId))
                            {
                                _DiscipCount++;
                                continue;
                            }

                            _DiscipCount++;
                            _CurrResults = (JArray)DJObject[_CurrLine]["results"];
                            _Length = _CurrResults.Count;
                            _CurrCountry = Regex.Match(_CurrLine, "(?<=study .* universities )(.*)", RegexOptions.Multiline).ToString();
                            _CurrCountry = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(_CurrCountry);


                            for (itr = 0; itr < _Length; itr++)
                            {
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
                            _DoneCount++;
                            Console.WriteLine(_DoneCount);
                        }

                        else
                        {
                            File.AppendAllText(@"..\Acapedia.Helper\DataFolder\leftovers.txt", _CurrLine);
                        }
                    }
                }
            }
        }
    }
}
