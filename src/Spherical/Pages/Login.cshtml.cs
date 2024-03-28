using System.Security.Claims;
using Creative.DTO.Defender;
using Creative.Modelos;
using Creative.Utilidades;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Spherical.Helper;

namespace Spherical.Pages
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        [Inject]
        private ContextoUsuario Contexto { get; set; }

        public async Task<IActionResult> OnGetAsync(string empresa, string usuario, string clave, string retornoUrl)
        {
            try
            {
                // Clear the existing external cookie
                await HttpContext
                    .SignOutAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme);
            }
            catch { }
            AutenticacionDTO autenticacionDTO = new AutenticacionDTO
            {
                Empresa = empresa,
                Usuario = usuario,
                Clave = clave,
                Token = string.Empty,
                Terminal = string.Empty,
                FechaInicio = DateTime.Now,
                FechaFin = DateTime.MinValue
            };

            var resultado = ClienteApi.PostRecurso(Configuracion.UrlApiDefender(), "api/Autenticar", autenticacionDTO);
            if (!string.IsNullOrEmpty(resultado))
            {
                var usuarioDTO = JsonConvert.DeserializeObject<UsuarioDTO>(resultado);
                if (usuarioDTO != null)
                {
                    Contexto.Usuario = usuarioDTO;
                    Contexto.Empresa = empresa;
                    retornoUrl = Url.Content($"~/{retornoUrl}");

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, usuarioDTO.Nombre),
                        new Claim(ClaimTypes.Locality, empresa),
                        new Claim(ClaimTypes.Email, usuarioDTO.Correo),
                        new Claim(ClaimTypes.NameIdentifier, usuario)
                    };
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true,
                        RedirectUri = this.Request.Host.Value
                    };
                    try
                    {
                        await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);

                        return LocalRedirect(retornoUrl);
                    }
                    catch (Exception ex)
                    {
                        string error = ex.Message;
                    }
                }
            }

            return NotFound();
        }
    }
}