using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectDin.Models
{
    public class Optie
    {
        public int OptieID { get; set; }
        public string Naam { get; set; }

        public int AantalStemmen { get; set; }

        public int PollID { get; set; }
        public Poll Poll { get; set; }

        public ICollection<Antwoord> Antwoorden { get; set; }
    }
}
