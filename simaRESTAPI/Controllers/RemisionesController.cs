using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using simaRESTAPI.Models;

namespace simaRESTAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RemisionesController : ControllerBase
    {
        private readonly simaServiceContext _context;

        public RemisionesController(simaServiceContext context)
        {
            _context = context;
        }

        // GET: api/Remisiones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Remisiones>>> GetRemisiones()
        {
            return await _context.Remisiones.ToListAsync();
        }

        // GET: api/Remisiones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Remisiones>> GetRemisiones(string id)
        {
            var remisiones = await _context.Remisiones.FindAsync(id);

            if (remisiones == null)
            {
                return NotFound();
            }

            return remisiones;
        }

        // PUT: api/Remisiones/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRemisiones(string id, Remisiones remisiones)
        {
            if (id != remisiones.Remision)
            {
                return BadRequest();
            }

            _context.Entry(remisiones).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RemisionesExists(id))
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

        // POST: api/Remisiones
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Remisiones>> PostRemisiones(Remisiones remisiones)
        {
            _context.Remisiones.Add(remisiones);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (RemisionesExists(remisiones.Remision))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetRemisiones", new { id = remisiones.Remision }, remisiones);
        }

        // DELETE: api/Remisiones/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Remisiones>> DeleteRemisiones(string id)
        {
            var remisiones = await _context.Remisiones.FindAsync(id);
            if (remisiones == null)
            {
                return NotFound();
            }

            _context.Remisiones.Remove(remisiones);
            await _context.SaveChangesAsync();

            return remisiones;
        }

        private bool RemisionesExists(string id)
        {
            return _context.Remisiones.Any(e => e.Remision == id);
        }
    }
}
