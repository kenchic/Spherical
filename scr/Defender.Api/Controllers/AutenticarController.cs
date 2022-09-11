using Creative.DTO.Defender;
using Defender.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<ActionResult<UsuarioDTO>> Autenticar(AutenticacionDTO dto)
        {
            if (_context.Usuarios == null)
            {
                return NotFound();
            }
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(model => model.Usuario1 == dto.Usuario && model.Clave == dto.Clave);
            if (usuario == null)
            {
                return NotFound();
            }

            return EntityToDTO(usuario);
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
