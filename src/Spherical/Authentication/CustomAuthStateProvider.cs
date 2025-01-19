using Spherical.Client.DTO.Defender;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Spherical.Authentication
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private string _jwtToken;
        public async Task SetToken(string token)
        {
            _jwtToken = token;
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            if (string.IsNullOrEmpty(_jwtToken))
            {
                return Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));
            }

            var handler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(_jwtToken);

            var identity = new ClaimsIdentity(jwtToken.Claims, "jwt");
            var user = new ClaimsPrincipal(identity);

            return Task.FromResult(new AuthenticationState(user));
        }
    }
}
