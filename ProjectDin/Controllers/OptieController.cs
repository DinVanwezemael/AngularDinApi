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
    public class OptieController : ControllerBase
    {
        private readonly ProjectContext _context;

        public OptieController(ProjectContext context)
        {
            _context = context;
        }

        // GET: api/Optie
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Optie>>> GetOpties()
        {
            return await _context.Opties.ToListAsync();
        }

        // GET: api/Optie/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Optie>> GetOptie(int id)
        {
            var optie = await _context.Opties.FindAsync(id);

            if (optie == null)
            {
                return NotFound();
            }

            return optie;
        }

        // PUT: api/Optie/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOptie(int id, Optie optie)
        {
            if (id != optie.OptieID)
            {
                return BadRequest();
            }
            optie.AantalStemmen++;

            _context.Entry(optie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OptieExists(id))
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

        // POST: api/Optie
        [HttpPost]
        public async Task<ActionResult<Optie>> PostOptie(Optie optie)
        {
            _context.Opties.Add(optie);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOptie", new { id = optie.OptieID }, optie);
        }

        // DELETE: api/Optie/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Optie>> DeleteOptie(int id)
        {
            var optie = await _context.Opties.FindAsync(id);
            if (optie == null)
            {
                return NotFound();
            }

            _context.Opties.Remove(optie);
            await _context.SaveChangesAsync();

            return optie;
        }

        private bool OptieExists(int id)
        {
            return _context.Opties.Any(e => e.OptieID == id);
        }
    }
}
