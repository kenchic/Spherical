using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Spherical.Api.Models;
using Spherical.Client.DTO.Inventory;
using Spherical.Client.DTO.Spherical;
using System.Net;

namespace Spherical.Api.Controllers.Lineup
{
    [Authorize]
    [Route("api/v1/documentos")]
    [ApiController]
    public class DocumentosController : ControllerBase
    {
        private readonly SphericalContext _context;

        public DocumentosController(SphericalContext context)
        {
            _context = context;
        }

        // GET: api/v1/documentos/tipos
        [HttpGet("tipos")]
        public async Task<ActionResult<ApiResponse<IEnumerable<DocumentoTipoDto>>>> GetTipos()
        {
            try
            {
                var tipos = await _context.DocumentoTipos
                    .Where(dt => dt.Activo)
                    .OrderBy(dt => dt.Nombre)
                    .Select(dt => new DocumentoTipoDto
                    {
                        Id = dt.Id,
                        Empresa = dt.Empresa,
                        Nombre = dt.Nombre,
                        Consecutivo = dt.Consecutivo,
                        Operacion = dt.Operacion,
                        CantidadFilas = dt.CantidadFilas,
                        EsSistema = dt.EsSistema,
                        Activo = dt.Activo
                    })
                    .ToListAsync();

                var response = new ApiResponse<IEnumerable<DocumentoTipoDto>>
                {
                    Data = tipos,
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

        // GET: api/v1/documentos
        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<DocumentoDto>>>> GetDocumentos()
        {
            try
            {
                var documentos = await _context.Documentos
                    .OrderByDescending(d => d.Fecha)
                    .Select(d => new DocumentoDto
                    {
                        Id = d.Id,
                        IdDocumentoTipo = d.IdDocumentoTipo,
                        IdBodegaOrigen = d.IdBodegaOrigen,
                        IdBodegaDestino = d.IdBodegaDestino,
                        Empresa = d.Empresa,
                        Numero = d.Numero,
                        Fecha = d.Fecha,
                        Descripcion = d.Descripcion,
                        Estado = d.Estado,
                        Detalles = new List<DocumentoDetalleDto>()
                    })
                    .ToListAsync();

                var response = new ApiResponse<IEnumerable<DocumentoDto>>
                {
                    Data = documentos,
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

        // GET: api/v1/documentos/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<DocumentoDto>>> GetDocumento(int id)
        {
            try
            {
                var d = await _context.Documentos.FirstOrDefaultAsync(x => x.Id == id);
                if (d == null)
                {
                    var notFound = new ApiResponse<string>(HttpStatusCode.NotFound, string.Empty, "Documento no encontrado");
                    return NotFound(notFound);
                }

                var detalles = await _context.DocumentoDetalles
                    .Where(dd => dd.IdDocumento == id)
                    .Select(dd => new DocumentoDetalleDto
                    {
                        Id = dd.Id,
                        IdElemento = (short)dd.IdElemento,
                        IdDocumento = dd.IdDocumento,
                        Cantidad = dd.Cantidad,
                        ElementoNombre = _context.Elementos.Where(e => e.Id == dd.IdElemento).Select(e => e.Nombre).FirstOrDefault()
                    })
                    .ToListAsync();

                var dto = new DocumentoDto
                {
                    Id = d.Id,
                    IdDocumentoTipo = d.IdDocumentoTipo,
                    IdBodegaOrigen = d.IdBodegaOrigen,
                    IdBodegaDestino = d.IdBodegaDestino,
                    Empresa = d.Empresa,
                    Numero = d.Numero,
                    Fecha = d.Fecha,
                    Descripcion = d.Descripcion,
                    Estado = d.Estado,
                    Detalles = detalles
                };

                var response = new ApiResponse<DocumentoDto>
                {
                    Data = dto,
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

        // POST: api/v1/documentos
        [HttpPost]
        public async Task<ActionResult<ApiResponse<DocumentoDto>>> CrearDocumento([FromBody] DocumentoDto dto)
        {
            using var tx = await _context.Database.BeginTransactionAsync();
            try
            {
                // Obtener tipo y consecutivo
                var tipo = await _context.DocumentoTipos.FirstOrDefaultAsync(t => t.Id == dto.IdDocumentoTipo);
                if (tipo == null)
                {
                    var notFound = new ApiResponse<string>(HttpStatusCode.NotFound, string.Empty, "Tipo de documento no encontrado");
                    return NotFound(notFound);
                }

                // Asignar número y fecha
                var numero = (int)(tipo.Consecutivo + 1);
                var documento = new Documento
                {
                    IdDocumentoTipo = dto.IdDocumentoTipo,
                    IdBodegaOrigen = dto.IdBodegaOrigen,
                    IdBodegaDestino = dto.IdBodegaDestino,
                    Empresa = string.IsNullOrWhiteSpace(dto.Empresa) ? tipo.Empresa : dto.Empresa,
                    Numero = numero,
                    Fecha = dto.Fecha == default ? DateTime.UtcNow : dto.Fecha,
                    Descripcion = dto.Descripcion,
                    Estado = string.IsNullOrWhiteSpace(dto.Estado) ? "ABIERTO" : dto.Estado
                };

                _context.Documentos.Add(documento);
                await _context.SaveChangesAsync();

                // Guardar detalles
                if (dto.Detalles != null && dto.Detalles.Count > 0)
                {
                    foreach (var d in dto.Detalles)
                    {
                        var detalle = new DocumentoDetalle
                        {
                            IdElemento = d.IdElemento,
                            Cantidad = d.Cantidad,
                            IdDocumento = documento.Id
                        };
                        _context.DocumentoDetalles.Add(detalle);
                    }
                    await _context.SaveChangesAsync();
                }

                // Actualizar consecutivo del tipo
                tipo.Consecutivo = tipo.Consecutivo + 1;
                _context.DocumentoTipos.Update(tipo);
                await _context.SaveChangesAsync();

                await tx.CommitAsync();

                // Respuesta con documento creado
                var creado = new DocumentoDto
                {
                    Id = documento.Id,
                    IdDocumentoTipo = documento.IdDocumentoTipo,
                    IdBodegaOrigen = documento.IdBodegaOrigen,
                    IdBodegaDestino = documento.IdBodegaDestino,
                    Empresa = documento.Empresa,
                    Numero = documento.Numero,
                    Fecha = documento.Fecha,
                    Descripcion = documento.Descripcion,
                    Estado = documento.Estado,
                    Detalles = dto.Detalles ?? new List<DocumentoDetalleDto>()
                };

                var response = new ApiResponse<DocumentoDto>
                {
                    Data = creado,
                    Success = true
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                await tx.RollbackAsync();
                var response = new ApiResponse<string>(HttpStatusCode.BadRequest, string.Empty, ex.Message);
                return BadRequest(response);
            }
        }
    }
}