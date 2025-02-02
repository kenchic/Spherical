﻿using Spherical.Client.DTO.Defender;
using Spherical.Client.DTO.Spherical;
using Spherical.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Spherical.Api.Controllers.Defender
{
    [Route("api/v1/security")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private readonly SphericalContext _context;

        private readonly string _key = "Una_Clave_Secreta_Para_Generar_JWT"; // Clave secreta

        public SecurityController(SphericalContext context)
        {
            _context = context;
        }

        [Route("login")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Autenticar(LoginDTO dto)
        {
            if (_context.Usuarios == null)
            {
                var res = new ApiResponse<string>(HttpStatusCode.NotFound, string.Empty);
                return NotFound(res);
            }
            else
            {
                var user = await _context.Usuarios.FirstOrDefaultAsync(
                                    model => model.Empresa.ToUpper() == dto.Empresa.ToUpper() &&
                                    model.Usuario1.ToUpper() == dto.Usuario.ToUpper() &&
                                    model.Clave.ToUpper() == dto.Clave.ToUpper());
                if (user == null)
                {
                    var res = new ApiResponse<string>(HttpStatusCode.NotFound, string.Empty, "Usuario o Clave incorrecto");
                    return NotFound(res);
                }
                else
                {
                    var claims = new[]
                     {
                        new Claim(JwtRegisteredClaimNames.Name , dto.Usuario),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(
                        issuer: "TuDominio.com",
                        audience: "TuDominio.com",
                        claims: claims,
                        expires: DateTime.Now.AddMinutes(30),
                        signingCredentials: creds);

                    var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
                    ApiResponse<string> res = new ApiResponse<string>(HttpStatusCode.OK, tokenString);
                    return Ok(res);
                }
            }
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok();
        }
    }
}
