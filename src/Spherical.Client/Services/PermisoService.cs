using Spherical.Client.DTO.Defender;
using Spherical.Client.DTO.Spherical;
using System.Text.Json;

namespace Spherical.Client.Services
{
    public interface IPermisoService 
    {
        Task<ApiResponse<PermisoUsuarioDto>> GetPermisosAsync(string opcion, string usuario);
        void SetAuth(string token);
    }

    public class PermisoService : ApiServiceBase, IPermisoService
    {
        public PermisoService(HttpClient httpClient) : base(httpClient) { }
        
        public async Task<ApiResponse<PermisoUsuarioDto>> GetPermisosAsync(string opcion, string usuario)
        {
            try
            {
                EnsureToken();

                var response = await _httpClient.GetAsync($"api/v1/permisos/{opcion}/{usuario}");
                var content = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonSerializer.Deserialize<ApiResponse<PermisoUsuarioDto>>(content, _jsonOptions);
                    return result ?? new ApiResponse<PermisoUsuarioDto>();
                }
                else
                {
                    return new ApiResponse<PermisoUsuarioDto>
                    {
                        Success = false,
                        StatusCode = (int)response.StatusCode,
                        ErrorMessage = $"Error al obtener permisos: {response.StatusCode}"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse<PermisoUsuarioDto>
                {
                    Success = false,
                    ErrorMessage = $"Error de conexión: {ex.Message}"
                };
            }
        }
    }
}