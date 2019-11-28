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
    public class UserBeheerController : ControllerBase
    {
        private readonly ProjectContext _context;

        public UserBeheerController(ProjectContext context)
        {
            _context = context;
        }

        // GET: api/UserBeheer
        [HttpGet("getall")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/UserBeheer/5
        [HttpGet("getuser{id}")]
        public async Task<ActionResult<UserDto>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            var UserDto = new UserDto{ UserID = user.UserID, Email = user.Email, Firstname = user.Email, Lastname = user.LastName, Password = user.Password, Username = user.Username };

            if (UserDto == null)
            {
                return NotFound();
            }

            return UserDto;
        }

        // PUT: api/UserBeheer/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id,[FromBody] User user)
        {

            var userOld = _context.Users.Find(id);

            user.Password = userOld.Password;

            _context.Entry(userOld).State = EntityState.Detached;

            _context.Entry(user).State = EntityState.Modified;


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(user);
        }

        // PUT: api/UserBeheer/5
        [HttpPut("password/{id}")]
        public async Task<IActionResult> PutUserPassword(int id,[FromBody] User user)
        {
            if (id != user.UserID)
            {
                return BadRequest();
            }


            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/UserBeheer
        [HttpPost("insert")]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.UserID }, user);
        }

        // DELETE: api/UserBeheer/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return user;
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserID == id);
        }
    }
}
