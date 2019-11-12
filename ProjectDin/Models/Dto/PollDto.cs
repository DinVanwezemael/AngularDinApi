using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectDin.Models.Dto
{
    public class PollDto
    {
        public int PollID { get; set; }
        public string Naam { get; set; }

        public int UserID { get; set; }
        public string UserName { get; set; }

        public int PollUserID { get; set; }

        public string PollName { get; set; }
        public int PollOptionID { get; set; }
    }
}
