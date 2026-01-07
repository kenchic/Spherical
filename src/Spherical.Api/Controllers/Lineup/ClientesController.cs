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
    [Route("api/v1/clientes")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly SphericalContext _context;

        public ClientesController(SphericalContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<VClienteModeloDto>>>> GetClientes()
        {
            try
            {
                var clientes = await _context.Clientes
                    .Select(c => new VClienteModeloDto
                    {
                        Id = c.Id,
                        idCiudad = c.IdCiudad,
                        CiudadNombre = c.IdCiudad,
                        Identificacion = c.Identificacion,
                        Nombre1 = c.Nombre1,
                        Nombre2 = c.Nombre2 ?? string.Empty,
                        Apellido1 = c.Apellido1,
                        Apellido2 = c.Apellido2 ?? string.Empty,
                        Nombre = c.Nombre1 + " " + c.Apellido1,
                        Direccion = c.Direccion,
                        Telefono = c.Telefono,
                        Celular = c.Celular ?? string.Empty,
                        Correo = c.Correo ?? string.Empty,
                        Activo = c.Activo
                    })
                    .ToListAsync();

                var response = new ApiResponse<IEnumerable<VClienteModeloDto>>
                {
                    Data = clientes,
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
        public async Task<ActionResult<ApiResponse<VClienteModeloDto>>> GetCliente(int id)
        {
            try
            {
                var c = await _context.Clientes.FindAsync(id);
                if (c == null)
                {
                    var notFound = new ApiResponse<string>(HttpStatusCode.NotFound, string.Empty, "Cliente no encontrado");
                    return NotFound(notFound);
                }

                var response = new ApiResponse<VClienteModeloDto>
                {
                    Data = new VClienteModeloDto
                    {
                        Id = c.Id,
                        idCiudad = c.IdCiudad,
                        CiudadNombre = c.IdCiudad,
                        Identificacion = c.Identificacion,
                        Nombre1 = c.Nombre1,
                        Nombre2 = c.Nombre2 ?? string.Empty,
                        Apellido1 = c.Apellido1,
                        Apellido2 = c.Apellido2 ?? string.Empty,
                        Nombre = c.Nombre1 + " " + c.Apellido1,
                        Direccion = c.Direccion,
                        Telefono = c.Telefono,
                        Celular = c.Celular ?? string.Empty,
                        Correo = c.Correo ?? string.Empty,
                        Activo = c.Activo
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
        public async Task<ActionResult<ApiResponse<ClienteModeloDto>>> PostCliente(ClienteModeloDto dto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dto.Identificacion) || string.IsNullOrWhiteSpace(dto.Nombre1) || string.IsNullOrWhiteSpace(dto.Apellido1) || string.IsNullOrWhiteSpace(dto.Direccion) || string.IsNullOrWhiteSpace(dto.Telefono) || string.IsNullOrWhiteSpace(dto.IdCiudad))
                {
                    var bad = new ApiResponse<string>(HttpStatusCode.BadRequest, string.Empty, "Campos obligatorios faltantes");
                    return BadRequest(bad);
                }

                var dup = await _context.Clientes.AnyAsync(x => x.Identificacion == dto.Identificacion);
                if (dup)
                {
                    var conflict = new ApiResponse<string>(HttpStatusCode.Conflict, string.Empty, "Ya existe un cliente con esa identificación");
                    return Conflict(conflict);
                }

                var entity = new Cliente
                {
                    IdCiudad = dto.IdCiudad,
                    Empresa = "Spherical",
                    Identificacion = dto.Identificacion,
                    Nombre1 = dto.Nombre1,
                    Nombre2 = dto.Nombre2,
                    Apellido1 = dto.Apellido1,
                    Apellido2 = dto.Apellido2,
                    Direccion = dto.Direccion,
                    Telefono = dto.Telefono,
                    Celular = dto.Celular,
                    Correo = dto.Correo,
                    Activo = dto.Activo
                };

                _context.Clientes.Add(entity);
                await _context.SaveChangesAsync();

                dto.Id = entity.Id;
                var response = new ApiResponse<ClienteModeloDto>
                {
                    Data = dto,
                    Success = true,
                    ErrorMessage = "Cliente creado exitosamente"
                };
                return CreatedAtAction(nameof(GetCliente), new { id = entity.Id }, response);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<string>(HttpStatusCode.BadRequest, string.Empty, ex.Message);
                return BadRequest(response);
            }
        }
    }
}
