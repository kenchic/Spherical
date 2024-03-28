using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lineup.Api.Models;
using Microsoft.AspNetCore.Authorization;

namespace Lineup.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ElementosController : ControllerBase
    {
        private readonly SphericalContext _context;

        public ElementosController(SphericalContext context)
        {
            _context = context;
        }

        // GET: api/Elementoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Elemento>>> GetElementos()
        {
          if (_context.Elementos == null)
          {
              return NotFound();
          }
            return await _context.Elementos.ToListAsync();
        }

        // GET: api/Elementoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Elemento>> GetElemento(short id)
        {
          if (_context.Elementos == null)
          {
              return NotFound();
          }
            var elemento = await _context.Elementos.FindAsync(id);

            if (elemento == null)
            {
                return NotFound();
            }

            return elemento;
        }

        // PUT: api/Elementoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutElemento(short id, Elemento elemento)
        {
            if (id != elemento.Id)
            {
                return BadRequest();
            }

            _context.Entry(elemento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ElementoExists(id))
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

        // POST: api/Elementoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Elemento>> PostElemento(Elemento elemento)
        {
          if (_context.Elementos == null)
          {
              return Problem("Entity set 'SphericalContext.Elementos'  is null.");
          }
            _context.Elementos.Add(elemento);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetElemento", new { id = elemento.Id }, elemento);
        }

        // DELETE: api/Elementoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteElemento(short id)
        {
            if (_context.Elementos == null)
            {
                return NotFound();
            }
            var elemento = await _context.Elementos.FindAsync(id);
            if (elemento == null)
            {
                return NotFound();
            }

            _context.Elementos.Remove(elemento);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ElementoExists(short id)
        {
            return (_context.Elementos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
