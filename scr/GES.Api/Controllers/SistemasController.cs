using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GES.Api.Models;
using CoreGeneral.DTO.GES;

namespace GES.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SistemasController : ControllerBase
    {
        private readonly SAFContext _context;

        public SistemasController(SAFContext context)
        {
            _context = context;
        }

       //GET: api/Sistemas
       [HttpGet]
        public async Task<ActionResult<IEnumerable<SistemaDTO>>> GetSistemas()
        {
            if (_context.Sistemas == null)
            {
                return NotFound();
            }

            var listSistema = await _context.Sistemas.Select(x => EntityToDTO(x)).ToListAsync();

            if (listSistema.Count < 0)
            {
                return NotFound();
            }

            return listSistema;
        }

        //GET: api/Sistemas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SistemaDTO>> GetSistema(string id)
        {
            if (_context.Sistemas == null)
            {
                return NotFound();
            }
            var sistema = await _context.Sistemas.FindAsync(id);

            if (sistema == null)
            {
                return NotFound();
            }

            return EntityToDTO(sistema);
        }

        // PUT: api/Sistemas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSistema(string id, SistemaDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            var sistema = await _context.Sistemas.FirstOrDefaultAsync(model => model.Id == id);
            if (sistema == null)
            {
                return NotFound();
            }

            sistema.Id = dto.Id;
            sistema.Version = dto.Version;

            _context.Entry(sistema).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SistemaExists(id))
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

        // POST: api/Sistemas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SistemaDTO>> PostSistema(SistemaDTO dto)
        {
            if (_context.Sistemas == null)
            {
                return Problem("Entity set 'SAFContext.Sistemas'  is null.");
            }

            var sistema = new Sistema()
            {
                Id = dto.Id,
                Version = dto.Version
            };

            _context.Sistemas.Add(sistema);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SistemaExists(sistema.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSistema", new { Id = sistema.Id }, EntityToDTO(sistema));
        }

        // DELETE: api/Sistemas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSistema(string id)
        {
            if (_context.Sistemas == null)
            {
                return NotFound();
            }
            var sistema = await _context.Sistemas.FindAsync(id);
            if (sistema == null)
            {
                return NotFound();
            }

            _context.Sistemas.Remove(sistema);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SistemaExists(string id)
        {
            return (_context.Sistemas?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private static SistemaDTO EntityToDTO(Sistema sistema) =>
        new SistemaDTO
        {
            Id = sistema.Id,
            Version = sistema.Version
        };
    }
}
