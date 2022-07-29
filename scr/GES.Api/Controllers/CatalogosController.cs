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
        public async Task<ActionResult<IEnumerable<Catalogo>>> GetCatalogos()
        {
            if (_context.Catalogos == null)
            {
                return NotFound();
            }
            return await _context.Catalogos.ToListAsync();
        }

        // GET: api/Catalogos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Catalogo>> GetCatalogo(string id)
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

            return catalogo;
        }

        // PUT: api/Catalogos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCatalogo(string id, Catalogo catalogo)
        {
            if (id != catalogo.Id)
            {
                return BadRequest();
            }

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
        public async Task<ActionResult<Catalogo>> PostCatalogo(CatalogoDTO catalogoM)
        {
            Catalogo catalogo = new Catalogo
            {
                Id = catalogoM.Id,
                IdSistema = catalogoM.IdSistema,
                Descripcion = catalogoM.Descripcion
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
    }
}
