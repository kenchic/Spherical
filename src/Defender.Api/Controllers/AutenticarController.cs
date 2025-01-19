using Creative.DTO.Defender;
using Creative.DTO.Spherical;
using Defender.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace Defender.Api.Controllers
{
    [Route("api/v1/seguridad")]
    [ApiController]
    public class AutenticarController : ControllerBase
    {
        private readonly SphericalContext _context;

        public AutenticarController(SphericalContext context)
        {
            _context = context;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<string>>> Autenticar(AutenticacionDTO dto)
        {
            if (_context.Usuarios == null)
            {
                var res = new ApiResponse<string>(HttpStatusCode.NotFound, string.Empty);
                return NotFound(res);
            }
            else
            {
                var usuario = await _context.Usuarios.FirstOrDefaultAsync(
                                    model => model.Empresa.ToUpper() == dto.Empresa.ToUpper() && 
                                    model.Usuario1.ToUpper() == dto.Usuario.ToUpper() && 
                                    model.Clave.ToUpper() == dto.Clave.ToUpper());
                if (usuario == null)
                {
                    var res = new ApiResponse<string>(HttpStatusCode.NotFound, string.Empty, "Usuario o Clave incorrecto");
                    return NotFound(res);
                }
                else
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes("Una_Clave_Secreta_Para_Generar_JWT");
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                            new Claim(ClaimTypes.Name, $"{usuario.Usuario1};{usuario.Identificacion};{usuario.Empresa}"),
                        }),
                        Expires = DateTime.UtcNow.AddHours(24),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    var tokenString = tokenHandler.WriteToken(token);

                    var res = new ApiResponse<string>()
                    {
                        Data = tokenString
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
