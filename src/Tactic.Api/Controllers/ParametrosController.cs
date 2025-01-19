using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tactic.Api.Models;
using Creative.DTO.Tactic;

namespace Tactic.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParametrosController : ControllerBase
    {
        private readonly SphericalContext _context;

        public ParametrosController(SphericalContext context)
        {
            _context = context;
        }

        // GET: api/Parametros
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParametroDTO>>> GetParametros()
        {
            if (_context.Parametros == null)
            {
                return NotFound();
            }

            var listParametro = await _context.Parametros.Select(x => EntityToDTO(x)).ToListAsync();

            if (listParametro.Count < 0)
            {
                return NotFound();
            }

            return listParametro;
        }

        // GET: api/Parametros/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ParametroDTO>> GetParametro(string id)
        {
            if (_context.Parametros == null)
            {
                return NotFound();
            }

            var parametro = await _context.Parametros.FindAsync(id);

            if (parametro == null)
            {
                return NotFound();
            }

            return EntityToDTO(parametro);
        }

        // PUT: api/Parametros/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutParametro(string id, ParametroDTO dto)
        {
            if (id != dto.Codigo)
            {
                return BadRequest();
            }

            var parametro = await _context.Parametros.FirstOrDefaultAsync(model => model.Codigo == id);
            if (parametro == null)
            {
                return NotFound();
            }

            parametro.IdSistema = dto.Sistema;
            parametro.Nombre = dto.Nombre;
            parametro.Descripcion = dto.Descripcion;
            parametro.Valor = dto.Valor;
            parametro.Activo = dto.Activo;

            _context.Entry(parametro).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParametroExists(id))
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

        // POST: api/Parametros
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ParametroDTO>> PostParametro(ParametroDTO dto)
        {
            if (_context.Parametros == null)
            {
                return Problem("Entity set 'SphericalContext.Parametros'  is null.");
            }

            var parametro = new Parametro()
            {
                Codigo = dto.Codigo,
                IdSistema = dto.Sistema,
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
                Valor = dto.Valor,
                Activo = dto.Activo
            };

            _context.Parametros.Add(parametro);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ParametroExists(parametro.Codigo))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetParametro", new { id = parametro.Codigo }, parametro);
        }

        // DELETE: api/Parametros/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParametro(string id)
        {
            if (_context.Parametros == null)
            {
                return NotFound();
            }
            var parametro = await _context.Parametros.FindAsync(id);
            if (parametro == null)
            {
                return NotFound();
            }

            _context.Parametros.Remove(parametro);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ParametroExists(string id)
        {
            return (_context.Parametros?.Any(e => e.Codigo == id)).GetValueOrDefault();
        }

        private static ParametroDTO EntityToDTO(Parametro parametro) =>
        new ParametroDTO
        {           
            Codigo = parametro.Codigo,
            Sistema = parametro.IdSistema,
            Descripcion = parametro.Descripcion,
            Nombre = parametro.Nombre,
            Valor = parametro.Valor,
            Activo = parametro.Activo
        };
    }
}
