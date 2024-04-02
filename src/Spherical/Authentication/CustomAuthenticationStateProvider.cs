using Creative.DTO.Defender;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Security.Claims;

namespace Spherical.Authentication
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {

        private readonly ProtectedLocalStorage _localStorage;

        private ClaimsPrincipal? _anonymous = new ClaimsPrincipal(new ClaimsIdentity());

        public CustomAuthenticationStateProvider(ProtectedLocalStorage localStorage)
        {
            _localStorage = localStorage;

        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var respuesta = await _localStorage.GetAsync<AutenticacionDTO>("userContext");
                var userContext = respuesta.Success ? respuesta.Value : null;
                if (userContext == null)
                    return await Task.FromResult(new AuthenticationState(_anonymous));

                var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userContext.Usuario)
                }, "CustomAuth"));
                return await Task.FromResult(new AuthenticationState(claimsPrincipal));                
            }
            catch (Exception)
            {
                return await Task.FromResult(new AuthenticationState(_anonymous));
            }
        }

        public async Task UpdateAuthenticationState(AutenticacionDTO userContext)
        {
            ClaimsPrincipal claimsPrincipal;

            if (userContext != null)
            {
                await _localStorage.SetAsync("userContext", userContext);

                claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userContext.Usuario)
                }));

            }
            else
            {
                await _localStorage.DeleteAsync("userContext");
                claimsPrincipal = _anonymous;
            }
            
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }
    }
}
