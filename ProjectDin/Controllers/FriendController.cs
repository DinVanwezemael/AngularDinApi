﻿using System;
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
        public async Task<ActionResult<IList<FriendDto>>> GetFriends(int id)
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
                vFriendsDtos.Add(new FriendDto() { UserID = f.UserID, FriendID = f.FriendID, Status = f.Status, UserFriendID = f.UserFriendID, Username = f.UserFriend.Username });
            }

            return vFriendsDtos;
        }

        // GET: api/Friend/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IList<FriendDto>>> GetFriend(int id)
        {
            //vrienden hebben de status 2
            var friendsContext = _context.Friends.Include(f => f.UserFriend).Where(f => f.UserID == id).Where(f => f.Status == 2);

            if (friendsContext == null)
            {
                return NotFound();
            }

            var vFriends = await friendsContext.ToListAsync();

            var vFriendsDtos = new List<FriendDto>();

            foreach(var f in vFriends)
            {
                vFriendsDtos.Add(new FriendDto() { UserID = f.UserID, FriendID = f.FriendID, Status = f.Status, UserFriendID = f.UserFriendID, Username = f.UserFriend.Username, Reference = f.Reference });
            }


            return vFriendsDtos;
        }

        [HttpGet("getverzoeken{id}")]
        public async Task<ActionResult<IList<FriendDto>>> GetVerzoeken(int id)
        {
            //een vriendschapsverzoek heeft de status 1
            var friendsContext = _context.Friends.Include(f => f.UserFriend).Where(f => f.UserFriendID == id).Where(f => f.Status == 1);

            if (friendsContext == null)
            {
                return NotFound();
            }

            var vFriends = await friendsContext.ToListAsync();

            var vFriendsDtos = new List<FriendDto>();

            foreach (var f in vFriends)

            {
                var user = _context.Users.Find(f.UserID);

                vFriendsDtos.Add(new FriendDto() { UserID = f.UserID, FriendID = f.FriendID, Status = f.Status, UserFriendID = f.UserFriendID, Username = user.Username });
            }

            vFriendsDtos.Reverse();

            return vFriendsDtos;
        }

        // PUT: api/Friend/5
        [HttpPut("accept{id}")]
        public async Task<IActionResult> PutFriend(int id, Friend friend)
        {
            if (id != friend.FriendID)
            {
                return BadRequest();
            }
            friend.Reference = friend.FriendID;

            _context.Entry(friend).State = EntityState.Modified;

            //status op 2 zetten
            var newFriend = new Friend() { Status = 2, UserID = friend.UserFriendID, UserFriendID = friend.UserID, Reference = friend.FriendID };

            _context.Friends.Add(newFriend);

            await _context.SaveChangesAsync();

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
        [HttpPost]
        public async Task<ActionResult<FriendDto>> PostFriend([FromBody]FriendDto friend)
        {

            var newFriendDto = new Friend(){ Status = friend.Status, UserID = friend.UserID, UserFriendID = friend.UserFriendID};

            _context.Friends.Add(newFriendDto);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFriend", new { id = friend.FriendID }, friend);
        }

        // DELETE: api/Friend/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Friend>> DeleteFriend(int id)
        {

            var friends = await _context.Friends.Where(f => f.Reference == id).ToListAsync();
            if (friends == null)
            {
                return NotFound();
            }


            foreach(var f in friends)
            {

                _context.Friends.Remove(f);
            }
            await _context.SaveChangesAsync();

            return null;
        }

        // DELETE: api/Friend/5
        [HttpDelete("decline{id}")]
        public async Task<ActionResult<Friend>> DeleteFriendDecline(int id)
        {

            var friend = _context.Friends.Find(id);

            _context.Friends.Remove(friend);

            await _context.SaveChangesAsync();

            return null;
        }

        private bool FriendExists(int id)
        {
            return _context.Friends.Any(e => e.FriendID == id);
        }
    }
}
