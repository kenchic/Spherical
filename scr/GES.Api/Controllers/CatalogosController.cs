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

            var listCatalogos = await _context.Catalogos
            .Include(cd => cd.CatalogoDetalles)
            .Select(c => EntityToDTO(c)).ToListAsync();

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
            var catalogo = await _context.Catalogos.Include(d => d.CatalogoDetalles).FirstOrDefaultAsync(c => c.Id == id);

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
            using (var transaction = _context.Database.BeginTransaction())
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
                    foreach (var detalle in dto.Detalle)
                    {
                        CatalogoDetalle catalogoDetalle;

                        if (detalle.Action.Equals("Insertar"))
                        {
                            catalogoDetalle = new CatalogoDetalle
                            {
                                IdCatalogo = catalogo.Id,
                                Id = detalle.Codigo,
                                ValorDecimal = detalle.ValorDecimal,
                                ValorCadena = detalle.ValorCadena,
                                ValorNumero = detalle.ValorNumero,
                                Nombre = detalle.Nombre,
                                Activo = detalle.Activo,
                            };

                            _context.CatalogoDetalles.Add(catalogoDetalle);
                        }
                        else
                        {
                            catalogoDetalle = await _context.CatalogoDetalles.FirstOrDefaultAsync(model => model.Id == detalle.Codigo && model.IdCatalogo == id);

                            if (catalogoDetalle != null)
                            {
                                if (detalle.Action.Equals("Borrar"))
                                {
                                    _context.Entry(catalogoDetalle).State = EntityState.Deleted;
                                }
                                else if (detalle.Action.Equals("Editar"))
                                {
                                    catalogoDetalle.ValorDecimal = detalle.ValorDecimal;
                                    catalogoDetalle.ValorCadena = detalle.ValorCadena;
                                    catalogoDetalle.ValorNumero = detalle.ValorNumero;
                                    catalogoDetalle.Nombre = detalle.Nombre;
                                    catalogoDetalle.Activo = detalle.Activo;
                                    _context.Entry(catalogoDetalle).State = EntityState.Modified;
                                }
                            }
                        }                        
                    }
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    await transaction.RollbackAsync();
                    if (!CatalogoExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

            }
            return NoContent();
        }

        // POST: api/Catalogos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CatalogoDTO>> PostCatalogo(CatalogoDTO dto)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                Catalogo catalogo = new Catalogo
                {
                    Id = dto.Id,
                    IdSistema = dto.Sistema,
                    Descripcion = dto.Descripcion
                };

                if (_context.Catalogos == null)
                {
                    return Problem("Entity set 'SAFContext.Catalogos' is null.");
                }

                _context.Catalogos.Add(catalogo);
                try
                {                    
                    foreach (var detalle in dto.Detalle)
                    {
                        CatalogoDetalle catalogoDetalle = new CatalogoDetalle
                        {
                            IdCatalogo = catalogo.Id,
                            Id = detalle.Codigo,
                            ValorDecimal = detalle.ValorDecimal,
                            ValorCadena = detalle.ValorCadena,
                            ValorNumero = detalle.ValorNumero,
                            Nombre = detalle.Nombre,
                            Activo = detalle.Activo,
                        };

                        _context.CatalogoDetalles.Add(catalogoDetalle);                        
                    }
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (DbUpdateException ex)
                {
                    await transaction.RollbackAsync();
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

                return EntityToDTO(catalogo);
            }
        }

        // DELETE: api/Catalogos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCatalogo(string id)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    if (_context.Catalogos == null)
                    {
                        return NotFound();
                    }

                    var catalogo = await _context.Catalogos.Include(d => d.CatalogoDetalles).FirstOrDefaultAsync(c => c.Id == id);
                    if (catalogo == null)
                    {
                        return NotFound();
                    }

                    foreach(var detalle in catalogo.CatalogoDetalles)
                    {
                        _context.CatalogoDetalles.Remove(detalle);
                    }

                    _context.Catalogos.Remove(catalogo);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    MensajeNegocio.EscribirLog(Constantes.MensajeError, ex.Message, "CatalogosController - DeleteCatalogo");
                    throw;
                }
            }

            return NoContent();
        }

        private bool CatalogoExists(string id)
        {
            return (_context.Catalogos?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private static CatalogoDTO EntityToDTO(Catalogo catalogo)
        {
            var dto = new CatalogoDTO
            {
                Id = catalogo.Id,
                Descripcion = catalogo.Descripcion,
                Sistema = catalogo.IdSistema
            };

            dto.Detalle = new List<CatalogoDetalleDTO>();

            foreach (var detalle in catalogo.CatalogoDetalles)
            {
                var item = new CatalogoDetalleDTO
                {
                    Codigo = detalle.Id,
                    Nombre = detalle.Nombre,
                    ValorCadena = detalle.ValorCadena,
                    ValorDecimal = detalle.ValorDecimal,
                    ValorNumero = detalle.ValorNumero
                };

                dto.Detalle.Add(item);
            }

            return dto;
        }
    }
}