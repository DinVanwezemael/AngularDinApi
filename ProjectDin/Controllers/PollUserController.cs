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
    public class PollUserController : ControllerBase
    {
        private readonly ProjectContext _context;

        public PollUserController(ProjectContext context)
        {
            _context = context;
        }

        // GET: api/PollUser
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PollUser>>> GetPollUsers()
        {
            return await _context.PollUsers.Include(p => p.User).Include(p => p.Poll).ToListAsync();
        }

        //// GET: api/PollUser/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<PollUser>> GetPollUser(int id)
        //{
        //    var pollUser = await _context.PollUsers.FindAsync(id);

        //    if (pollUser == null)
        //    {
        //        return NotFound();
        //    }

        //    return pollUser;
        //}

        // GET: api/PollUser/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IList<PollDto>>> GetPollUser(int id)
        {
            var pollContext = _context.PollUsers.Include(p => p.User).Include(p => p.Poll).Include(p => p.Poll.PollOptions).Where(p => p.UserID == id);

            if (pollContext == null)
            {
                return NotFound();
            }


            var vPolls = await pollContext.ToListAsync();

            var vPollDtos = new List<PollDto>();

            foreach (var p in vPolls)
            {
                vPollDtos.Add(new PollDto() { PollID = p.PollID, Naam = p.Poll.Naam, UserID = p.UserID, UserName = p.User.Username, PollUserID = p.PollUserID });
            }

            return vPollDtos;
        }

        // PUT: api/PollUser/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPollUser(int id, PollUser pollUser)
        {
            if (id != pollUser.PollUserID)
            {
                return BadRequest();
            }

            _context.Entry(pollUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PollUserExists(id))
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



        // POST: api/PollUser
        [HttpPost]
        public async Task<ActionResult<PollDto>> PostPollUser([FromBody]PollDto pollDto)
        {
            Console.WriteLine(pollDto.PollName);
            var vPollToInsert = new Poll() { Naam = pollDto.PollName };
            _context.Poll.Add(vPollToInsert);

            await _context.SaveChangesAsync();

            _context.PollUsers.Add(new PollUser() { PollID = vPollToInsert.PollID, UserID = pollDto.UserID });
            
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPollUser", new { id = pollDto.UserID }, pollDto);
        }

        // DELETE: api/PollUser/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PollUser>> DeletePollUser(int id)
        {
            var pollUser = await _context.PollUsers.FindAsync(id);
            if (pollUser == null)
            {
                return NotFound();
            }

            _context.PollUsers.Remove(pollUser);
            await _context.SaveChangesAsync();

            return pollUser;
        }

        private bool PollUserExists(int id)
        {
            return _context.PollUsers.Any(e => e.PollUserID == id);
        }

        [HttpPost("getpolluser")]
        public async Task<ActionResult<IList<PollUser>>> Polls(int? UserID)
        {
            var pollContext = _context.PollUsers.Include(p => p.User).Include(p => p.Poll).Where(p => p.UserID == UserID);

            return await pollContext.ToListAsync();
        }
    }
}
