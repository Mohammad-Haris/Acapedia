using System.Linq;
using Acapedia.Data.Contracts;
using Acapedia.Data;
using Acapedia.Data.Models;
using System.Collections.Generic;
using System;
using Newtonsoft.Json.Linq;
using Acapedia.Service.JsonModels;

namespace Acapedia.Service
{
    public class ExploreService : IExplore
    {
        private readonly AcapediaDbContext _Context;

        public ExploreService (AcapediaDbContext context)
        {
            _Context = context;
        }

        public IEnumerable<University> GetUniversities (object _ClientSelection)
        {
            JObject toconvert = _ClientSelection as JObject;

            CD_Recieved _Recieved = toconvert.ToObject<CD_Recieved>();

            string[] discips = { "Arts", "Performing Arts", "Music", "Accompanying", "Chamber Music", "Church Music", "Conducting", "Choral conducting", "Orchestral conducting", "Wind ensemble conducting", "Early music", "Jazz Studies", "Musical Composition", "Music Education", "Music History", "Musicology", "Historical musicology", "Systematic musicology", "Ethnomusicology", "Music theory", "Orchestral studies", "Organology", "Organ and historical keyboards", "Piano", "Strings, harp, oud, and guitar (outline)", "Singing", "Woodwinds, brass, and percussion", "Recording", "Dance", "Choreography", "Dance notation", "Ethnochoreology", "History of dance", "Television", "Television studies", "Theatre", "Acting", "Directing", "Dramaturgy", "History", "Musical theatre", "Playwrighting", "Puppetry", "Scenography", "Stage design", "Ventriloquism", "Film", "Animation", "Film criticism", "Film Making", "Film theory", "Live action", "Visual Arts", "Fine Arts", "Graphic Arts", "Drawing", "Painting", "Photography", "Sculpture", "Applied Arts", "Animation", "Calligraphy", "Decorative arts", "Mixed media", "Printmaking", "Studio art", "Architecture", "Interior architecture", "Landscape architecture", "Architectural analytics", "Historic preservation", "Interior design", "Landscape architecture", "Landscape design", "Technical drawing", "Information architecture", "Urban planning", "Outline of design", "Fashion design", "Textile design", "User experience design", "Interaction design", "User experience evaluation", "User interface design", "Visual communication", "Graphic design", "Typography", "Type design", "Industrial design", "Ergonomics", "Game design", "Toy and amusement design", "History", "African history", "American history", "Ancient history", "Ancient Carthage", "Ancient Egypt", "Ancient Greek history", "Ancient Roman history", "History of the Indus Valley Civilization", "History of the ancient Maya", "History of Mesopotamia", "History of the Yangtze civilization", "History of the Yellow River civillization", "Asian history", "Chinese history", "Indian history", "Indonesian history", "Iranian history", "History of the Indus Valley Civilization", "History of the ancient Maya", "History of Mesopotamia", "History of the Yangtze civilization", "History of the Yellow River civillization", "Australian history", "Ecclesiastical history of the Catholic Church", "Economic history", "Environmental history", "European history", "Intellectual history", "Latin American history", "Modern history", "Political history", "Pre-Columbian era", "Russian history", "History of Culture", "Scientific history", "Technological history", "World history", "Languages and Literature", "Linguistics", "Applied linguistics", "Composition studies", "Computational linguistics", "Discourse analysis", "English studies", "Etymology", "Grammar", "Historical linguistics", "History of linguistics", "Interlinguistics", "Lexicology", "Linguistic typology", "Morphology (linguistics)", "Natural language processing", "Philology", "Phonetics", "Phonology", "Pragmatics", "Psycholinguistics", "Rhetoric", "Semantics", "Semiotics (outline)", "Sociolinguistics", "Syntax", "Usage", "Word usage", "Comparative Literature", "Creative Writing", "Fiction", "Non-Fiction", "English literature", "History of literature", "Medieval literature", "Post-colonial literature", "Post-modern literature", "Literary theory", "Critical theory", "Literary criticism", "Poetics", "Rhetoric", "Poetry", "World literature", "African-American literature", "American literature", "British literature", "Philosophy", "Aesthetics", "Applied Philosophy", "Philosophy of economics", "Philosophy of education", "Philosophy of engineering", "Philosophy of history", "Philosophy of language", "Philosophy of law", "Philosophy of mathematics", "Philosophy of music", "Philosophy of psychology", "Philosophy of religion", "Philosophy of Physical Sciences", "Philosophy of biology", "Philosophy of chemistry", "Philosophy of physics", "Philosophy of social science", "Philosophy of technology", "Systems philosophy", "Epistemology", "Justification", "Reasoning errors", "Ethics", "Applied Ethics", "Animal rights", "Bioethics", "Environmental ethics", "Meta-ethics", "Moral psychology, Descriptive ethics, Value theory", "Normative ethics", "Virtue ethics", "History of Philosophy", "Ancient philosophy", "Contemporary philosophy", "Medieval philosophy", "Humanism", "Scholasticism", "Modern philosophy", "Logic", "Mathematical logic", "Philosophical logic", "Meta-philosophy", "Metaphysics", "Philosophy of Action", "Determinism and Free will", "Ontology", "Philosophy of mind", "Philosophy of pain", "Philosophy of artificial intelligence", "Philosophy of perception", "Philosophy of space and time", "Teleology", "Theism and Atheism", "Philosophical traditions and schools", "African philosophy", "Analytic philosophy", "Aristotelianism", "Continental philosophy", "Eastern philosophy", "Feminist philosophy", "Platonism", "Social philosophy and political philosophy", "African philosophy", "Anarchism", "Libertarianism", "Marxism", "Theology", "Biblical studies", "Biblical Hebrew, Biblical Greek, Aramaic", "Buddhist theology", "Christian theology", "Anglican theology", "Baptist theology", "Catholic theology", "Eastern Orthodox theology", "Protestant theology", "Hindu theology", "Jewish theology", "Muslim theology" };
            string[] couns = { "Afghanistan", "Albania", "Algeria", "American Samoa", "Andorra", "Angola", "Anguilla", "Antarctica", "Antigua and Barbuda", "Argentina", "Armenia", "Aruba", "Australia", "Austria", "Azerbaijan", "Bahamas", "Bahrain", "Bangladesh", "Barbados", "Belarus", "Belgium", "Belize", "Benin", "Bermuda", "Bhutan", "Bolivia", "Bosnia and Herzegovina", "Botswana", "Bouvet Island", "Brazil", "British Indian Ocean Territory", "Brunei Darussalam", "Bulgaria", "Burkina Faso", "Burundi", "Cambodia", "Cameroon", "Canada", "Cape Verde", "Cayman Islands", "Central African Republic", "Chad", "Chile", "China", "Christmas Island", "Cocos (Keeling) Islands", "Colombia", "Comoros", "Congo", "Cook Islands", "Costa Rica", "Croatia (Hrvatska)", "Cuba", "Cyprus", "Czech Republic", "Denmark", "Djibouti", "Dominica", "Dominican Republic", "East Timor", "Ecuador", "Egypt", "El Salvador", "Equatorial Guinea", "Eritrea", "Estonia", "Ethiopia", "Falkland Islands (Malvinas)", "Faroe Islands", "Fiji", "Finland", "France", "France, Metropolitan", "French Guiana", "French Polynesia", "French Southern Territories", "Gabon", "Gambia", "Georgia", "Germany", "Ghana", "Gibraltar", "Greece", "Greenland", "Grenada", "Guadeloupe", "Guam", "Guatemala", "Guernsey", "Guinea", "Guinea-Bissau", "Guyana", "Haiti", "Heard and Mc Donald Islands", "Honduras", "Hong Kong", "Hungary", "Iceland", "India", "Indonesia", "Iran (Islamic Republic of)", "Iraq", "Ireland", "Isle of Man", "Israel", "Italy", "Ivory Coast", "Jamaica", "Japan", "Jersey", "Jordan", "Kazakhstan", "Kenya", "Kiribati", "Korea, Democratic People's Republic of", "Korea, Republic of", "Kosovo", "Kuwait", "Kyrgyzstan", "Lao People's Democratic Republic", "Latvia", "Lebanon", "Lesotho", "Liberia", "Libyan Arab Jamahiriya", "Liechtenstein", "Lithuania", "Luxembourg", "Macau", "Macedonia", "Madagascar", "Malawi", "Malaysia", "Maldives", "Mali", "Malta", "Marshall Islands", "Martinique", "Mauritania", "Mauritius", "Mayotte", "Mexico", "Micronesia, Federated States of", "Moldova, Republic of", "Monaco", "Mongolia", "Montenegro", "Montserrat", "Morocco", "Mozambique", "Myanmar", "Namibia", "Nauru", "Nepal", "Netherlands", "Netherlands Antilles", "New Caledonia", "New Zealand", "Nicaragua", "Niger", "Nigeria", "Niue", "Norfolk Island", "Northern Mariana Islands", "Norway", "Oman", "Pakistan", "Palau", "Palestine", "Panama", "Papua New Guinea", "Paraguay", "Peru", "Philippines", "Pitcairn", "Poland", "Portugal", "Puerto Rico", "Qatar", "Reunion", "Romania", "Russian Federation", "Rwanda", "Saint Kitts and Nevis", "Saint Lucia", "Saint Vincent and the Grenadines", "Samoa", "San Marino", "Sao Tome and Principe", "Saudi Arabia", "Senegal", "Serbia", "Seychelles", "Sierra Leone", "Singapore", "Slovakia", "Slovenia", "Solomon Islands", "Somalia", "South Africa", "South Georgia South Sandwich Islands", "South Sudan", "Spain", "Sri Lanka", "St. Helena", "St. Pierre and Miquelon", "Sudan", "Suriname", "Svalbard and Jan Mayen Islands", "Swaziland", "Sweden", "Switzerland", "Syrian Arab Republic", "Taiwan", "Tajikistan", "Tanzania, United Republic of", "Thailand", "Togo", "Tokelau", "Tonga", "Trinidad and Tobago", "Tunisia", "Turkey", "Turkmenistan", "Turks and Caicos Islands", "Tuvalu", "Uganda", "Ukraine", "United Arab Emirates", "United Kingdom", "United States", "United States minor outlying islands", "Uruguay", "Uzbekistan", "Vanuatu", "Vatican City State", "Venezuela", "Vietnam", "Virgin Islands (British)", "Virgin Islands (U.S.)", "Wallis and Futuna Islands", "Western Sahara", "Yemen", "Zaire", "Zambia", "Zimbabwe" };

            if (string.IsNullOrEmpty(_Recieved.Discipline) || string.IsNullOrEmpty(_Recieved.Country) ||
                Array.IndexOf(discips, _Recieved.Discipline) < 0 || Array.IndexOf(couns, _Recieved.Country) < 0)
            {
                return null;
            }

            var _DID = _Context.Discipline.Where(Discipline => Discipline.DisciplineName == _Recieved.Discipline)
                .Select(Discipline => Discipline.DisciplineId).FirstOrDefault();

            return _Context.University.Join(_Context.UniversityDiscipline,
                uni => uni.UniversityId,
                unidis => unidis.UniversityId,
                (uni, unidis) => new
                {
                    uni,
                    unidis
                }).Where(ret => ret.unidis.DisciplineId == _DID)
                .Where(ret => ret.uni.CountryName == _Recieved.Country)
                .Select(ret => ret.uni);
        }

        public IEnumerable<University> InsertUniversities (object _Universities)
        {
            List<University> _Added = new List<University>();
            JObject toconvert = _Universities as JObject;
            Unis_Recieved _Recieved = toconvert.ToObject<Unis_Recieved>();

            var _DID = _Context.Discipline.Where(Discipline => Discipline.DisciplineName == _Recieved.Discipline)
            .Select(Discipline => Discipline.DisciplineId).FirstOrDefault();            

            foreach (var uni in _Recieved.Universities)
            {
                University toadd = new University()
                {
                    UniversityName = uni,
                    CountryName = _Recieved.Country
                };

                _Added.Add(toadd);
                _Context.Add(toadd);                                
            }

            _Context.SaveChanges();

            for (int itr = 0; itr < _Recieved.Universities.Count; itr++)
            {
                var _UID = _Context.University.Where(curr => curr.UniversityName == _Recieved.Universities[itr]).Select(curr => curr.UniversityId).FirstOrDefault();

                _Context.Add(new UniversityDiscipline
                {
                    UniversityId = _UID,
                    DisciplineId = _DID
                });
            }

            _Context.SaveChangesAsync();

            return _Added;
        }
    }
}
