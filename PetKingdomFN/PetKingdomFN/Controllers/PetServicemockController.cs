using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetKingdomFN.Models;

namespace PetKingdomFN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetServicemockController : ControllerBase
    {
        private readonly PetKingdomContext _context;

        public PetServicemockController(PetKingdomContext context)
        {
            _context = context;
        }

        // GET: api/PetServicemock
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PetService>>> GetPetServices()
        {
          if (_context.PetServices == null)
          {
              return NotFound();
          }
            return await _context.PetServices.ToListAsync();
        }

        // GET: api/PetServicemock/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PetService>> GetPetService(string id)
        {
          if (_context.PetServices == null)
          {
              return NotFound();
          }
            var petService = await _context.PetServices.FindAsync(id);

            if (petService == null)
            {
                return NotFound();
            }

            return petService;
        }

        // PUT: api/PetServicemock/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPetService(string id, PetService petService)
        {
            if (id != petService.id)
            {
                return BadRequest();
            }

            _context.Entry(petService).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PetServiceExists(id))
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

        // POST: api/PetServicemock
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PetService>> PostPetService(PetService petService)
        {
          if (_context.PetServices == null)
          {
              return Problem("Entity set 'PetKingdomContext.PetServices'  is null.");
          }
            _context.PetServices.Add(petService);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PetServiceExists(petService.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPetService", new { id = petService.id }, petService);
        }

        // DELETE: api/PetServicemock/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePetService(string id)
        {
            if (_context.PetServices == null)
            {
                return NotFound();
            }
            var petService = await _context.PetServices.FindAsync(id);
            if (petService == null)
            {
                return NotFound();
            }

            _context.PetServices.Remove(petService);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PetServiceExists(string id)
        {
            return (_context.PetServices?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
