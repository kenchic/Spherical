using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Spherical.Api.Models;
using Spherical.Client.DTO.Lineup;
using Spherical.Client.DTO.Spherical;
using System.Net;

namespace Spherical.Api.Controllers.Lineup
{
    [Route("api/v1/elementos")]
    [ApiController]
    public class ElementosController : ControllerBase
    {
        private readonly SphericalContext _context;

        public ElementosController(SphericalContext context)
        {
            _context = context;
        }

        // GET: api/v1/elementos
        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<ElementoDTO>>>> GetElementos()
        {
            try
            {
                var elementos = await _context.Elementos
                    .Select(x => EntityToDTO(x))
                    .ToListAsync();

                var response = new ApiResponse<IEnumerable<ElementoDTO>>
                {
                    Data = elementos,
                    Success = true
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<string>(HttpStatusCode.BadRequest, string.Empty, ex.Message);
                return BadRequest(response);
            }
        }

        // GET: api/v1/elementos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<ElementoDTO>>> GetElemento(short id)
        {
            try
            {
                var elemento = await _context.Elementos.FindAsync(id);

                if (elemento == null)
                {
                    var notFoundResponse = new ApiResponse<string>(HttpStatusCode.NotFound, string.Empty, "Elemento no encontrado");
                    return NotFound(notFoundResponse);
                }

                var response = new ApiResponse<ElementoDTO>
                {
                    Data = EntityToDTO(elemento),
                    Success = true
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<string>(HttpStatusCode.BadRequest, string.Empty, ex.Message);
                return BadRequest(response);
            }
        }

        // POST: api/v1/elementos
        [HttpPost]
        public async Task<ActionResult<ApiResponse<ElementoDTO>>> PostElemento(ElementoDTO dto)
        {
            try
            {
                var elemento = new Elemento
                {
                    IdGrupoElemento = dto.IdGrupoElemento,
                    IdUnidadMedida = dto.IdUnidadMedida,
                    Empresa = dto.Empresa,
                    Referencia = dto.Referencia,
                    Nombre = dto.Nombre,
                    Mt2 = dto.Mt2,
                    Peso = dto.Peso,
                    Rotacion = dto.Rotacion,
                    Activo = dto.Activo
                };

                _context.Elementos.Add(elemento);
                await _context.SaveChangesAsync();

                dto.Id = elemento.Id;

                var response = new ApiResponse<ElementoDTO>
                {
                    Data = dto,
                    Success = true,
                    ErrorMessage = "Elemento creado exitosamente"
                };

                return CreatedAtAction(nameof(GetElemento), new { id = elemento.Id }, response);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<string>(HttpStatusCode.BadRequest, string.Empty, ex.Message);
                return BadRequest(response);
            }
        }

        // PUT: api/v1/elementos/5
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<string>>> PutElemento(short id, ElementoDTO dto)
        {
            if (id != dto.Id)
            {
                var badRequestResponse = new ApiResponse<string>(HttpStatusCode.BadRequest, string.Empty, "El ID no coincide");
                return BadRequest(badRequestResponse);
            }

            try
            {
                var elemento = await _context.Elementos.FindAsync(id);
                if (elemento == null)
                {
                    var notFoundResponse = new ApiResponse<string>(HttpStatusCode.NotFound, string.Empty, "Elemento no encontrado");
                    return NotFound(notFoundResponse);
                }

                elemento.IdGrupoElemento = dto.IdGrupoElemento;
                elemento.IdUnidadMedida = dto.IdUnidadMedida;
                elemento.Referencia = dto.Referencia;
                elemento.Nombre = dto.Nombre;
                elemento.Mt2 = dto.Mt2;
                elemento.Peso = dto.Peso;
                elemento.Rotacion = dto.Rotacion;
                elemento.Activo = dto.Activo;

                _context.Entry(elemento).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                var response = new ApiResponse<string>
                {
                    Success = true,
                    ErrorMessage = "Elemento actualizado exitosamente"
                };

                return Ok(response);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ElementoExists(id))
                {
                    var notFoundResponse = new ApiResponse<string>(HttpStatusCode.NotFound, string.Empty, "Elemento no encontrado");
                    return NotFound(notFoundResponse);
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<string>(HttpStatusCode.BadRequest, string.Empty, ex.Message);
                return BadRequest(response);
            }
        }

        // DELETE: api/v1/elementos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<string>>> DeleteElemento(short id)
        {
            try
            {
                var elemento = await _context.Elementos.FindAsync(id);
                if (elemento == null)
                {
                    var notFoundResponse = new ApiResponse<string>(HttpStatusCode.NotFound, string.Empty, "Elemento no encontrado");
                    return NotFound(notFoundResponse);
                }

                // Soft delete - marcar como inactivo en lugar de eliminar físicamente
                elemento.Activo = false;
                _context.Entry(elemento).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                var response = new ApiResponse<string>
                {
                    Success = true,
                    ErrorMessage = "Elemento eliminado exitosamente"
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<string>(HttpStatusCode.BadRequest, string.Empty, ex.Message);
                return BadRequest(response);
            }
        }

        // GET: api/v1/elementos/{id}/precios
        [HttpGet("{id}/precios")]
        public async Task<ActionResult<ApiResponse<IEnumerable<ListaPrecioDetalleModelo>>>> GetElementoPrecios(short id)
        {
            try
            {
                var precios = await _context.ListaPrecioDetalles
                    .Where(lpd => lpd.IdElemento == id)
                    .Include(lpd => lpd.IdListaPrecio)
                    .Select(lpd => new ListaPrecioDetalleModelo
                    {
                        idListaPrecio = lpd.IdListaPrecio,
                        idElemento = lpd.IdElemento,
                        PrecioAlquiler = lpd.PrecioAlquiler,
                        PrecioVenta = lpd.PrecioVenta,
                        PrecioPerdida = lpd.PrecioPerdida
                    })
                    .ToListAsync();

                var response = new ApiResponse<IEnumerable<ListaPrecioDetalleModelo>>
                {
                    Data = precios,
                    Success = true
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<string>(HttpStatusCode.BadRequest, string.Empty, ex.Message);
                return BadRequest(response);
            }
        }

        private bool ElementoExists(int id)
        {
            return _context.Elementos.Any(e => e.Id == id);
        }

        private static ElementoDTO EntityToDTO(Elemento elemento) =>
            new ElementoDTO
            {
                Id = elemento.Id,
                IdGrupoElemento = elemento.IdGrupoElemento,
                IdUnidadMedida = elemento.IdUnidadMedida,
                Referencia = elemento.Referencia,
                Empresa = elemento.Empresa,
                Nombre = elemento.Nombre,
                Mt2 = elemento.Mt2,
                Peso = elemento.Peso,
                Rotacion = elemento.Rotacion,
                Activo = elemento.Activo
            };
    }
}