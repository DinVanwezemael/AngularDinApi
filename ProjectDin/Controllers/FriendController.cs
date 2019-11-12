using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectDin.Models;
using ProjectDin.Models.Dto;

namespace ProjectDin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendController : ControllerBase
    {
        private readonly ProjectContext _context;

        public FriendController(ProjectContext context)
        {
            _context = context;
        }

        // GET: api/Friend
        [HttpGet]
        public async Task<ActionResult<IList<FriendDto>>> GetFriends()
        {
            var friendsContext = _context.Friends.Include(f => f.UserFriend);

            if (friendsContext == null)
            {
                return NotFound();
            }

            var vFriends = await friendsContext.ToListAsync();

            var vFriendsDtos = new List<FriendDto>();

            foreach (var f in vFriends)
            {
                vFriendsDtos.Add(new FriendDto() { UserID = f.UserID, FriendID = f.FriendID, Status = f.Status, UserIDFriend = f.UserIDFriend, Username = f.UserFriend.Username });
            }

            return vFriendsDtos;
        }

        // GET: api/Friend/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IList<FriendDto>>> GetFriend(int id)
        {
            var friendsContext = _context.Friends.Include(f => f.UserFriend).Where(f => f.UserID == id).Where(f => f.Status == 2);

            if (friendsContext == null)
            {
                return NotFound();
            }

            var vFriends = await friendsContext.ToListAsync();

            var vFriendsDtos = new List<FriendDto>();

            foreach(var f in vFriends)
            {
                vFriendsDtos.Add(new FriendDto() { UserID = f.UserID, FriendID = f.FriendID, Status = f.Status, UserIDFriend = f.UserIDFriend, Username = f.UserFriend.Username });
            }

            return vFriendsDtos;
        }

        //[HttpGet("{id}")]
        //public async Task<ActionResult<IList<FriendDto>>> GetVerzoeken(int id)
        //{
        //    var friendsContext = _context.Friends.Include(f => f.UserFriend).Where(f => f.UserID == id).Where(f => f.Status == 1);

        //    if (friendsContext == null)
        //    {
        //        return NotFound();
        //    }

        //    var vFriends = await friendsContext.ToListAsync();

        //    var vFriendsDtos = new List<FriendDto>();

        //    foreach (var f in vFriends)
        //    {
        //        vFriendsDtos.Add(new FriendDto() { UserID = f.UserID, FriendID = f.FriendID, Status = f.Status, UserIDFriend = f.UserIDFriend, Username = f.UserFriend.Username });
        //    }

        //    return vFriendsDtos;
        //}

        // PUT: api/Friend/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFriend(int id, Friend friend)
        {
            if (id != friend.FriendID)
            {
                return BadRequest();
            }

            _context.Entry(friend).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FriendExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Friend
        [HttpPost("insert")]
        public async Task<ActionResult<Friend>> PostFriend(Friend friend)
        {
            friend.Status = 1;
            _context.Friends.Add(friend);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFriend", new { id = friend.FriendID }, friend);
        }

        // DELETE: api/Friend/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Friend>> DeleteFriend(int id)
        {
            var friend = await _context.Friends.FindAsync(id);
            if (friend == null)
            {
                return NotFound();
            }

            _context.Friends.Remove(friend);
            await _context.SaveChangesAsync();

            return friend;
        }

        private bool FriendExists(int id)
        {
            return _context.Friends.Any(e => e.FriendID == id);
        }
    }
}
