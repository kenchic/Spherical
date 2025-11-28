using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Spherical.Api.Models;
using Spherical.Client.DTO.Defender;
using Spherical.Client.DTO.Spherical;
using System.Net;

namespace Defender.Api.Controllers
{
    [Authorize]
    [Route("api/v1/permisos")]
    [ApiController]
    public class PermisosController : ControllerBase
    {
        private readonly SphericalContext _context;

        public PermisosController(SphericalContext context)
        {
            _context = context;
        }

        // GET: api/v1/permisos/{opcion}/{usuario}
        [HttpGet("{opcion}/{usuario}")]
        public async Task<ActionResult<ApiResponse<PermisoUsuarioDto>>> GetPermisos(string opcion, string usuario)
        {
            try
            {
                var registro = await _context.VusuarioPermisos
                    .AsNoTracking()
                    .FirstOrDefaultAsync(p => p.Opcion == opcion && p.Usuario == usuario);

                if (registro == null)
                {
                    var notFound = new ApiResponse<string>(HttpStatusCode.NotFound, string.Empty, "Permisos no encontrados");
                    return NotFound(notFound);
                }

                var dto = new PermisoUsuarioDto
                {
                    Usuario = registro.Usuario,
                    Opcion = registro.Opcion,
                    Consultar = registro.Consultar,
                    Crear = registro.Crear,
                    Editar = registro.Editar,
                    Eliminar = registro.Eliminar,
                    Anular = registro.Anular,
                    Activar = registro.Activar
                };

                var response = new ApiResponse<PermisoUsuarioDto>
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
    }
}