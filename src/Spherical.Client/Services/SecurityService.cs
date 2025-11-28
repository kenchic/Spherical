using Spherical.Client.DTO.Defender;
using Spherical.Client.DTO.Spherical;
using Spherical.Core.Creative;
using System.Net.Http.Json;
using System.Text.Json;

namespace Spherical.Client.Services
{
    public interface ISecurityService
    {
        Task<ApiResponse<string>> LoginAsync(LoginDTO dto);
    }

    public class SecurityService : ApiServiceBase, ISecurityService
    {
        public SecurityService(HttpClient httpClient) : base(httpClient) { }

        public async Task<ApiResponse<string>> LoginAsync(LoginDTO dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"api/v1/security/login", dto, _jsonOptions);
               
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<ApiResponse<string>>(content, _jsonOptions);
                    return result ?? new ApiResponse<string>();
                }
                else
                {
                    return new ApiResponse<string>
                    {
                        Success = false,
                        StatusCode = (int)response.StatusCode,
                        ErrorMessage = $"Error al obtener credenciales: {response.StatusCode}"
                    };
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, "SecurityService - LoginAsync");
                return new ApiResponse<string>
                {
                    Success = false,
                    ErrorMessage = $"Error de conexión: {ex.Message}"
                };
            }
        }
    }
}
