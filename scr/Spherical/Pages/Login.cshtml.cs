using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Xml.Linq;
using Creative.DTO.Defender;
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
        public async Task<IActionResult> OnGetAsync(string paramUsername, string paramPassword, string returnUrl)
        {            
            try
            {
                // Clear the existing external cookie
                await HttpContext
                    .SignOutAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme);
            }
            catch { }
            AutenticacionDTO autenticacionDTO = new AutenticacionDTO{
                Usuario = paramUsername,
                Clave = paramPassword,
                Token = string.Empty,
                Terminal = string.Empty,
                FechaInicio = DateTime.Now,
                FechaFin = DateTime.MinValue
            };

            var resultado = ClienteApi.PostRecurso(Configuracion.UrlApiDefender(), "api/Autenticar", autenticacionDTO);
            if (!string.IsNullOrEmpty(resultado))
            {
                var usuario = JsonConvert.DeserializeObject<UsuarioDTO>(resultado);
                if (usuario != null)
                {
                    returnUrl = Url.Content($"~/{returnUrl}");

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, usuario.Nombre),
                        new Claim(ClaimTypes.Email, usuario.Correo),
                        new Claim(ClaimTypes.NameIdentifier, paramUsername)
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

                        return LocalRedirect(returnUrl);
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
