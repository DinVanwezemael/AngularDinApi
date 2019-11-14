using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectDin.Models
{
    public class User
    {
        public int UserID{ get; set; }
        public string FirstName { get; set; }
        public string LastName{ get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        [NotMapped]
        public string Token { get; set; }
        public ICollection<PollUser> PollUsers { get; set; }

        public ICollection<Friend> Friends { get; set; }
        public ICollection<Antwoord> Antwoorden { get; set; }

        public ICollection<Uitnodiging> Uitnodigingen { get; set; }
    }
}
