using Creative.DTO.Defender;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Security.Claims;

namespace Spherical.Authentication
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private ProtectedLocalStorage LocalStorage { get; set; }

        public CustomAuthenticationStateProvider(ProtectedLocalStorage localStorage)
        {
            LocalStorage = localStorage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                ClaimsIdentity identity;
                var appUsuario = await LocalStorage.GetAsync<UsuarioDTO>("appUsuario");

                if (appUsuario.Success && appUsuario.Value != null)
                {
                    identity = new ClaimsIdentity(
                    [
                        new Claim(ClaimTypes.Name, appUsuario.Value.Usuario),                        
                    ], "auth_type");
                }
                else
                {
                    identity = new ClaimsIdentity();
                }

                var user = new ClaimsPrincipal(identity);
                return await Task.FromResult(new AuthenticationState(user));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));
            }
        }

        public async Task MarkUserAsAuthentication(UsuarioDTO appUsuario)
        {
                await LocalStorage.SetAsync("appUsuario", appUsuario);
                var identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, appUsuario.Nombre),
                }, "auth_type");
            var claimsPrincipal = new ClaimsPrincipal(identity);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }
    }
}
