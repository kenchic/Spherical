using System.Net.Http.Headers;
using System.Text.Json;

namespace Spherical.Client.Services
{
    public abstract class ApiServiceBase
    {
        protected readonly HttpClient _httpClient;
        protected readonly JsonSerializerOptions _jsonOptions;
        private string? _jwtToken;

        protected ApiServiceBase(HttpClient httpClient)
        {
            _httpClient = httpClient;

            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public void SetAuth(string token)
        {
            _jwtToken = token;
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
        }

        protected void EnsureToken()
        {
            if (string.IsNullOrWhiteSpace(_jwtToken))
                throw new InvalidOperationException("Token no configurado");
        }
    }
}
