using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectDin.Models
{
    public class Antwoord
    {
        public int AntwoordID { get; set; }

        public int UserID { get; set; }
        public User Users { get; set; }

        public int OptieID { get; set; }
        public Optie Optie { get; set; }

    }
}
