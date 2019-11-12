using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectDin.Models
{
    public class Friend
    {
        public int FriendID { get; set; }
        public int UserID { get; set; }
        public int Status { get; set; }

        public int UserIDFriend { get; set; }
        public User UserFriend { get; set; }

    }
}
