using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectDin.Models
{
    public class Gebruiker
    {
        public long GebruikerID { get; set; }
        public string Email { get; set; }
        public string Wachtwoord { get; set; }
        public string Gebruikersnaam { get; set; }

        //public ICollection<PollGebruiker> pollgebruikers { get; set; }

        //public ICollection<Stem> stemmen { get; set; }
    }
}
