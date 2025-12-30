using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Spherical.Api.Models;
using Spherical.Client.DTO.Common;
using Spherical.Client.DTO.Spherical;
using System.Net;

namespace Spherical.Api.Controllers.Common
{
    [Route("api/v1/catalogos")]
    [ApiController]
    public class CatalogosController : ControllerBase
    {
        private readonly SphericalContext _context;

        public CatalogosController(SphericalContext context)
        {
            _context = context;
        }

        // GET: api/v1/catalogos/{id}/detalles
        [HttpGet("{id}/detalles")]
        public async Task<ActionResult<ApiResponse<IEnumerable<CatalogoDetalleModelo>>>> GetDetalles(string id)
        {
            try
            {
                var detalles = await _context.CatalogoDetalles
                    .Where(cd => cd.IdCatalogo == id && (cd.Activo ?? true))
                    .OrderBy(cd => cd.Nombre)
                    .Select(cd => new CatalogoDetalleModelo
                    {
                        Id = cd.Id,
                        Nombre = cd.Nombre,
                        ValorCadena = cd.ValorCadena,
                        ValorNumero = cd.ValorNumero,
                        ValorDecimal = cd.ValorDecimal,
                        Activo = cd.Activo
                    })
                    .ToListAsync();

                var response = new ApiResponse<IEnumerable<CatalogoDetalleModelo>>
                {
                    Data = detalles,
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

        // POST: api/v1/catalogos/{id}/detalles
        [HttpPost("{id}/detalles")]
        public async Task<ActionResult<ApiResponse<CatalogoDetalleModelo>>> CrearDetalle(string id, CatalogoDetalleModelo dto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dto.Id) || string.IsNullOrWhiteSpace(dto.Nombre))
                {
                    var bad = new ApiResponse<string>(HttpStatusCode.BadRequest, string.Empty, "Campos obligatorios faltantes");
                    return BadRequest(bad);
                }

                var exists = await _context.CatalogoDetalles.AnyAsync(cd => cd.IdCatalogo == id && cd.Id == dto.Id);
                if (exists)
                {
                    var conflict = new ApiResponse<string>(HttpStatusCode.Conflict, string.Empty, "El detalle ya existe");
                    return Conflict(conflict);
                }

                if (!System.Text.RegularExpressions.Regex.IsMatch(dto.Nombre.Trim(), "^[A-Za-zÁÉÍÓÚÑáéíóúñ\\s\\-]+$"))
                {
                    var badFormat = new ApiResponse<string>(HttpStatusCode.BadRequest, string.Empty, "Formato de nombre inválido");
                    return BadRequest(badFormat);
                }

                var entity = new CatalogoDetalle
                {
                    IdCatalogo = id,
                    Id = dto.Id,
                    Nombre = dto.Nombre.Trim(),
                    ValorCadena = dto.ValorCadena,
                    ValorNumero = dto.ValorNumero,
                    ValorDecimal = dto.ValorDecimal,
                    Activo = dto.Activo ?? true
                };

                _context.CatalogoDetalles.Add(entity);
                await _context.SaveChangesAsync();

                var response = new ApiResponse<CatalogoDetalleModelo>
                {
                    Data = dto,
                    Success = true,
                    ErrorMessage = "Detalle creado exitosamente"
                };
                return CreatedAtAction(nameof(GetDetalles), new { id }, response);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<string>(HttpStatusCode.BadRequest, string.Empty, ex.Message);
                return BadRequest(response);
            }
        }
    }
}
