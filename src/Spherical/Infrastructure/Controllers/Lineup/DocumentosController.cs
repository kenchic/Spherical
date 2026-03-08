using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Spherical.Infrastructure.EF.Models;
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
        private readonly ILogger<DocumentosController> _logger;

        public DocumentosController(SphericalContext context, ILogger<DocumentosController> logger)
        {
            _context = context;
            _logger = logger;
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
                _logger.LogError(ex, "Error al obtener tipos de documentos");
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
                        Detalles = new List<DocumentoDetalleDto>(),
                        Anulado = d.Estado == "ANULADO"
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
                _logger.LogError(ex, "Error al obtener documentos");
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
                    Detalles = detalles,
                    Anulado = d.Estado == "ANULADO"
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
                _logger.LogError(ex, "Error al obtener documento {DocumentoId}", id);
                var response = new ApiResponse<string>(HttpStatusCode.BadRequest, string.Empty, ex.Message);
                return BadRequest(response);
            }
        }

        // POST: api/v1/documentos
        [HttpPost]
        public async Task<ActionResult<ApiResponse<bool>>> CrearDocumento([FromBody] DocumentoDto dto)
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
                var numero = tipo.Consecutivo;
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

                var response = new ApiResponse<bool>
                {
                    Data = true,
                    Success = true
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                await tx.RollbackAsync();
                _logger.LogError(ex, "Error al crear documento");
                var response = new ApiResponse<string>(HttpStatusCode.BadRequest, string.Empty, ex.Message);
                return BadRequest(response);
            }
        }


        // PUT: api/v1/documentos/{id}/anular
        [HttpPut("{id}/anular")]
        public async Task<ActionResult<ApiResponse<bool>>> AnularDocumento(int id)
        {
            using var tx = await _context.Database.BeginTransactionAsync();
            try
            {
                var documento = await _context.Documentos.FirstOrDefaultAsync(x => x.Id == id);
                if (documento == null)
                {
                    var notFound = new ApiResponse<string>(HttpStatusCode.NotFound, string.Empty, "Documento no encontrado");
                    return NotFound(notFound);
                }

                if (string.Equals(documento.Estado, "ANULADO", StringComparison.OrdinalIgnoreCase))
                {
                    var bad = new ApiResponse<string>(HttpStatusCode.BadRequest, string.Empty, "El documento ya está anulado");
                    return BadRequest(bad);
                }

                documento.Estado = "ANULADO";
                _context.Documentos.Update(documento);
                await _context.SaveChangesAsync();

                await tx.CommitAsync();
                var response = new ApiResponse<bool>
                {
                    Data = true,
                    Success = true
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                await tx.RollbackAsync();
                _logger.LogError(ex, "Error al anular documento {DocumentoId}", id);
                var response = new ApiResponse<string>(HttpStatusCode.BadRequest, string.Empty, ex.Message);
                return BadRequest(response);
            }
        }

        // PUT: api/v1/documentos/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> ActualizarDocumento(int id, [FromBody] DocumentoDto dto)
        {
            if (dto == null)
            {
                var bad = new ApiResponse<string>(HttpStatusCode.BadRequest, string.Empty, "Solicitud inválida: cuerpo vacío");
                return BadRequest(bad);
            }

            using var tx = await _context.Database.BeginTransactionAsync();
            try
            {
                var documento = await _context.Documentos.FirstOrDefaultAsync(x => x.Id == id);
                if (documento == null)
                {
                    var notFound = new ApiResponse<string>(HttpStatusCode.NotFound, string.Empty, "Documento no encontrado");
                    return NotFound(notFound);
                }

                var detallesPreviosCount = await _context.DocumentoDetalles.CountAsync(dd => dd.IdDocumento == id);

                documento.IdBodegaOrigen = dto.IdBodegaOrigen;
                documento.IdBodegaDestino = dto.IdBodegaDestino;
                documento.Fecha = dto.Fecha;
                documento.Descripcion = dto.Descripcion;
                documento.Estado = dto.Estado;

                _context.Documentos.Update(documento);

                await _context.DocumentoDetalles
                    .Where(dd => dd.IdDocumento == id)
                    .ExecuteDeleteAsync();

                if (dto.Detalles != null && dto.Detalles.Count > 0)
                {
                    var nuevos = dto.Detalles.Select(d => new DocumentoDetalle
                    {
                        IdElemento = d.IdElemento,
                        Cantidad = d.Cantidad,
                        IdDocumento = id
                    }).ToList();
                    _context.DocumentoDetalles.AddRange(nuevos);
                }

                await _context.SaveChangesAsync();
                await tx.CommitAsync();

                var nuevosCount = await _context.DocumentoDetalles.CountAsync(dd => dd.IdDocumento == id);
                _logger.LogInformation(
                    "Documento {DocumentoId} actualizado. Detalles previos: {Previos}, nuevos: {Nuevos}",
                    id, detallesPreviosCount, nuevosCount);

                var ok = new ApiResponse<bool>
                {
                    Data = true,
                    Success = true
                };
                return Ok(ok);
            }
            catch (Exception ex)
            {
                await tx.RollbackAsync();
                _logger.LogError(ex, "Error al actualizar documento {DocumentoId}", id);
                var response = new ApiResponse<string>(HttpStatusCode.BadRequest, string.Empty, ex.Message);
                return BadRequest(response);
            }
        }
    }
}
