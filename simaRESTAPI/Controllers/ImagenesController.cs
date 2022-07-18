using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using simaRESTAPI.Models;

namespace simaRESTAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagenesController : ControllerBase
    {
        private readonly simaServiceContext _context;

        public ImagenesController(simaServiceContext context)
        {
            _context = context;
        }

        // GET: api/Imagenes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Imagenes>>> GetImagenes()
        {
            return await _context.Imagenes.ToListAsync();
        }

        // GET: api/Imagenes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Imagenes>> GetImagenes(int id)
        {
            var imagenes = await _context.Imagenes.FindAsync(id);

            if (imagenes == null)
            {
                return NotFound();
            }

            return imagenes;
        }

        // PUT: api/Imagenes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutImagenes(int id, Imagenes imagenes)
        {
            if (id != imagenes.Id)
            {
                return BadRequest();
            }

            _context.Entry(imagenes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImagenesExists(id))
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

        // POST: api/Imagenes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Imagenes>> PostImagenes(Imagenes imagenes)
        {
            _context.Imagenes.Add(imagenes);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetImagenes", new { id = imagenes.Id }, imagenes);
        }

        // DELETE: api/Imagenes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Imagenes>> DeleteImagenes(int id)
        {
            var imagenes = await _context.Imagenes.FindAsync(id);
            if (imagenes == null)
            {
                return NotFound();
            }

            _context.Imagenes.Remove(imagenes);
            await _context.SaveChangesAsync();

            return imagenes;
        }

        private bool ImagenesExists(int id)
        {
            return _context.Imagenes.Any(e => e.Id == id);
        }
    }
}
