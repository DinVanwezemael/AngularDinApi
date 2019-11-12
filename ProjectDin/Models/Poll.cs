﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectDin.Models
{
    public class Poll
    {
        public int PollID { get; set; }
        public string Naam { get; set; }
        public ICollection<PollUser> PollUsers { get; set; }
        public ICollection<PollOption> PollOptions { get; set; }
    }
}
