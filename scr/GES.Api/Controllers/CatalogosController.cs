using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GES.Api.Models;
using CoreGeneral.Negocios;
using CoreGeneral.Recursos;
using CoreGeneral.DTO.GES;

namespace GES.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogosController : ControllerBase
    {
        private readonly SAFContext _context;

        public CatalogosController(SAFContext context)
        {
            _context = context;
        }

        // GET: api/Catalogos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CatalogoDTO>>> GetCatalogos()
        {
            if (_context.Catalogos == null)
            {
                return NotFound();
            }

            var listCatalogos = await _context.Catalogos.Select(x => EntityToDTO(x)).ToListAsync();

            if (listCatalogos.Count < 0)
            {
                return NotFound();
            }

            return listCatalogos;
        }

        // GET: api/Catalogos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CatalogoDTO>> GetCatalogo(string id)
        {
            if (_context.Catalogos == null)
            {
                return NotFound();
            }
            var catalogo = await _context.Catalogos.FindAsync(id);

            if (catalogo == null)
            {
                return NotFound();
            }

            return EntityToDTO(catalogo);
        }

        // PUT: api/Catalogos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCatalogo(string id, CatalogoDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            var catalogo = await _context.Catalogos.FirstOrDefaultAsync(model => model.Id == id);
            if (catalogo == null)
            {
                return NotFound();
            }

            catalogo.Id = dto.Id;
            catalogo.Descripcion = dto.Descripcion;

            _context.Entry(catalogo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CatalogoExists(id))
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

        // POST: api/Catalogos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CatalogoDTO>> PostCatalogo(CatalogoDTO dto)
        {
            Catalogo catalogo = new Catalogo
            {
                Id = dto.Id,
                IdSistema = dto.Sistema,
                Descripcion = dto.Descripcion
            };

            if (_context.Catalogos == null)
            {
                return Problem("Entity set 'SAFContext.Catalogos'  is null.");
            }

            _context.Catalogos.Add(catalogo);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                if (CatalogoExists(catalogo.Id))
                {
                    return Conflict();
                }
                else
                {
                    MensajeNegocio.EscribirLog(Constantes.MensajeError, ex.Message, "CatalogosController - PostCatalogo");
                    throw;
                }
            }

            return CreatedAtAction("GetCatalogo", new { id = catalogo.Id }, catalogo);
        }

        // DELETE: api/Catalogos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCatalogo(string id)
        {
            if (_context.Catalogos == null)
            {
                return NotFound();
            }
            var catalogo = await _context.Catalogos.FindAsync(id);
            if (catalogo == null)
            {
                return NotFound();
            }

            _context.Catalogos.Remove(catalogo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CatalogoExists(string id)
        {
            return (_context.Catalogos?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private static CatalogoDTO EntityToDTO(Catalogo catalogo) =>
        new CatalogoDTO
        {
            Id = catalogo.Id,
            Descripcion = catalogo.Descripcion,
            Sistema = catalogo.IdSistema
        };
    }
}
