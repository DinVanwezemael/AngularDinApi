using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectDin.Models
{
    public class PollOption
    {
        public int PollOptionID { get; set; }
        public string OptionName { get; set; }
        public int PollID { get; set; }
        public Poll Poll { get; set; }
    }
}
