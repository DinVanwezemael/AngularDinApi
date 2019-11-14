using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectDin.Models.Dto
{
    public class FriendDto
    {
        public int FriendID { get; set; }
        public int UserID { get; set; }
        public int Status { get; set; }
        public string Username { get; set; }
        public int UserFriendID { get; set; }
    }
}
