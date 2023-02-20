using Creative.DTO.Defender;
using Creative.DTO.Spherical;
using Defender.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Defender.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticarController : ControllerBase
    {
        private readonly SphericalContext _context;

        public AutenticarController(SphericalContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<UsuarioDTO>>> Autenticar(AutenticacionDTO dto)
        {
            if (_context.Usuarios == null)
            {
                var res = new ApiResponse<string>(HttpStatusCode.NotFound, string.Empty);
                return NotFound(res);
            }
            else
            {
                var usuario = await _context.Usuarios.FirstOrDefaultAsync(model => model.Usuario1 == dto.Usuario && model.Clave == dto.Clave);
                if (usuario == null)
                {
                    var res = new ApiResponse<string>(HttpStatusCode.NotFound, string.Empty, "Usuario o Clave incorrecto");
                    return NotFound(res);
                }
                else
                {
                    var res = new ApiResponse<UsuarioDTO>()
                    {
                        Data = EntityToDTO(usuario)
                    };
                    return Ok(res);
                }
            }
        }

        private static UsuarioDTO EntityToDTO(Usuario usuario) =>
        new UsuarioDTO
        {
            Id = usuario.Id,
            Nombre = usuario.Nombre,
            Apellido = usuario.Apellido,
            Correo = usuario.Correo,
            Identificacion = usuario.Identificacion,
            Admin = usuario.Admin
        };
    }
}
