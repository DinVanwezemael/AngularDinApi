using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectDin.Models;

namespace ProjectDin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UitnodigingController : ControllerBase
    {
        private readonly ProjectContext _context;

        public UitnodigingController(ProjectContext context)
        {
            _context = context;
        }

        // GET: api/Uitnodiging
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Uitnodiging>>> GetUitnodigingen()
        {
            return await _context.Uitnodigingen.ToListAsync();
        }

        // GET: api/Uitnodiging/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Uitnodiging>> GetUitnodiging(int id)
        {
            var uitnodiging = await _context.Uitnodigingen.FindAsync(id);

            if (uitnodiging == null)
            {
                return NotFound();
            }

            return uitnodiging;
        }

        // PUT: api/Uitnodiging/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUitnodiging(int id, Uitnodiging uitnodiging)
        {
            if (id != uitnodiging.UitnodigingID)
            {
                return BadRequest();
            }

            _context.Entry(uitnodiging).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UitnodigingExists(id))
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

        // POST: api/Uitnodiging
        [HttpPost]
        public async Task<ActionResult<Uitnodiging>> PostUitnodiging([FromBody]Uitnodiging uitnodiging)
        {
            _context.Uitnodigingen.Add(new Uitnodiging {PollID = uitnodiging.PollID, UserID = uitnodiging.UserID });
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUitnodiging", new { id = uitnodiging.UitnodigingID }, uitnodiging);
        }

        // DELETE: api/Uitnodiging/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Uitnodiging>> DeleteUitnodiging(int id)
        {
            var uitnodiging = await _context.Uitnodigingen.FindAsync(id);
            if (uitnodiging == null)
            {
                return NotFound();
            }

            _context.Uitnodigingen.Remove(uitnodiging);
            await _context.SaveChangesAsync();

            return uitnodiging;
        }

        private bool UitnodigingExists(int id)
        {
            return _context.Uitnodigingen.Any(e => e.UitnodigingID == id);
        }
    }
}
