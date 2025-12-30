using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Spherical.Api.Models;
using Creative.Modelos.Lineup;
using Spherical.Client.DTO.Spherical;
using System.Net;

namespace Spherical.Api.Controllers.Lineup
{
    [Authorize]
    [Route("api/v1/proyectos")]
    [ApiController]
    public class ProyectosController : ControllerBase
    {
        private readonly SphericalContext _context;

        public ProyectosController(SphericalContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<VProyectoModeloDto>>>> GetProyectos()
        {
            try
            {
                var proyectos = await _context.Proyectos
                    .Select(p => new VProyectoModeloDto
                    {
                        Id = p.Id,
                        idCiudad = p.Ciudad,
                        CiudadNombre = p.Ciudad,
                        idCliente = p.IdCliente,
                        ClienteNombre = p.IdClienteNavigation.Nombre1 + " " + p.IdClienteNavigation.Apellido1,
                        idContrato = 0,
                        Nombre = p.Nombre,
                        Tipo = p.Tipo,
                        Direccion = p.Direccion ?? string.Empty,
                        Telefono = p.Telefono ?? string.Empty,
                        Observacion = p.Observacion ?? string.Empty,
                        Fecha = new DateTime(p.Fecha.Year, p.Fecha.Month, p.Fecha.Day),
                        FormaContacto = p.FormaContacto ?? string.Empty,
                        SistemaMedida = p.SistemaMedida ?? string.Empty,
                        IdentificacionResponsable = p.IdentificacionResponsable ?? string.Empty,
                        NombreResponsable = p.NombreResponsable ?? string.Empty,
                        TelResponsable = p.TelResponsable ?? string.Empty,
                        Activo = p.Activo,
                        Estado = (byte)(p.Estado == "ACTIVO" ? 1 : 0)
                    })
                    .ToListAsync();

                var response = new ApiResponse<IEnumerable<VProyectoModeloDto>>
                {
                    Data = proyectos,
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

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<VProyectoModeloDto>>> GetProyecto(int id)
        {
            try
            {
                var p = await _context.Proyectos.Include(x => x.IdClienteNavigation).FirstOrDefaultAsync(x => x.Id == id);
                if (p == null)
                {
                    var notFound = new ApiResponse<string>(HttpStatusCode.NotFound, string.Empty, "Proyecto no encontrado");
                    return NotFound(notFound);
                }
                var response = new ApiResponse<VProyectoModeloDto>
                {
                    Data = new VProyectoModeloDto
                    {
                        Id = p.Id,
                        idCiudad = p.Ciudad,
                        CiudadNombre = p.Ciudad,
                        idCliente = p.IdCliente,
                        ClienteNombre = p.IdClienteNavigation.Nombre1 + " " + p.IdClienteNavigation.Apellido1,
                        idContrato = 0,
                        Nombre = p.Nombre,
                        Tipo = p.Tipo,
                        Direccion = p.Direccion ?? string.Empty,
                        Telefono = p.Telefono ?? string.Empty,
                        Observacion = p.Observacion ?? string.Empty,
                        Fecha = new DateTime(p.Fecha.Year, p.Fecha.Month, p.Fecha.Day),
                        FormaContacto = p.FormaContacto ?? string.Empty,
                        SistemaMedida = p.SistemaMedida ?? string.Empty,
                        IdentificacionResponsable = p.IdentificacionResponsable ?? string.Empty,
                        NombreResponsable = p.NombreResponsable ?? string.Empty,
                        TelResponsable = p.TelResponsable ?? string.Empty,
                        Activo = p.Activo,
                        Estado = (byte)(p.Estado == "ACTIVO" ? 1 : 0)
                    },
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

        [HttpPost]
        public async Task<ActionResult<ApiResponse<ProyectoModeloDto>>> PostProyecto(ProyectoModeloDto dto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dto.Nombre) || string.IsNullOrWhiteSpace(dto.Tipo) || dto.idCliente <= 0 || string.IsNullOrWhiteSpace(dto.idCiudad))
                {
                    var bad = new ApiResponse<string>(HttpStatusCode.BadRequest, string.Empty, "Campos obligatorios faltantes");
                    return BadRequest(bad);
                }

                var cliente = await _context.Clientes.FindAsync(dto.idCliente);
                if (cliente == null)
                {
                    var notFound = new ApiResponse<string>(HttpStatusCode.NotFound, string.Empty, "Cliente no existe");
                    return NotFound(notFound);
                }

                var entity = new Proyecto
                {
                    IdCliente = dto.idCliente,
                    Ciudad = dto.idCiudad,
                    Empresa = "Spherical",
                    Nombre = dto.Nombre,
                    Tipo = dto.Tipo,
                    Direccion = dto.Direccion,
                    Telefono = dto.Telefono,
                    Observacion = dto.Observacion,
                    Fecha = new DateOnly(dto.Fecha.Year, dto.Fecha.Month, dto.Fecha.Day),
                    FormaContacto = dto.FormaContacto,
                    SistemaMedida = dto.SistemaMedida,
                    IdentificacionResponsable = dto.IdentificacionResponsable,
                    NombreResponsable = dto.NombreResponsable,
                    TelResponsable = dto.TelResponsable,
                    Estado = "ACTIVO",
                    Activo = dto.Activo
                };

                _context.Proyectos.Add(entity);
                await _context.SaveChangesAsync();

                dto.Id = entity.Id;
                var response = new ApiResponse<ProyectoModeloDto>
                {
                    Data = dto,
                    Success = true,
                    ErrorMessage = "Proyecto creado exitosamente"
                };
                return CreatedAtAction(nameof(GetProyecto), new { id = entity.Id }, response);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<string>(HttpStatusCode.BadRequest, string.Empty, ex.Message);
                return BadRequest(response);
            }
        }
    }
}
