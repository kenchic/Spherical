using Spherical.Client.DTO.Lineup;
using Spherical.Client.DTO.Spherical;
using System.Text.Json;

namespace Spherical.Client.Services
{
    public interface IBodegaService
    {
        Task<ApiResponse<IEnumerable<BodegaDto>>> GetBodegasAsync();
        Task<ApiResponse<BodegaDto>> GetBodegaAsync(int id);
        void SetAuth(string token);
    }

    public class BodegaService : ApiServiceBase, IBodegaService
    {
        public BodegaService(HttpClient httpClient) : base(httpClient) { }

        public async Task<ApiResponse<IEnumerable<BodegaDto>>> GetBodegasAsync()
        {
            try
            {
                EnsureToken();
                var response = await _httpClient.GetAsync("api/v1/bodegas");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<ApiResponse<IEnumerable<BodegaDto>>>(content, _jsonOptions);
                    return result ?? new ApiResponse<IEnumerable<BodegaDto>>();
                }
                else
                {
                    return new ApiResponse<IEnumerable<BodegaDto>>
                    {
                        Success = false,
                        ErrorMessage = $"Error al obtener bodegas: {response.StatusCode}"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse<IEnumerable<BodegaDto>>
                {
                    Success = false,
                    ErrorMessage = $"Error de conexión: {ex.Message}"
                };
            }
        }

        public async Task<ApiResponse<BodegaDto>> GetBodegaAsync(int id)
        {
            try
            {
                EnsureToken();
                var response = await _httpClient.GetAsync($"api/v1/bodegas/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<ApiResponse<BodegaDto>>(content, _jsonOptions);
                    return result ?? new ApiResponse<BodegaDto>();
                }
                else
                {
                    return new ApiResponse<BodegaDto>
                    {
                        Success = false,
                        ErrorMessage = $"Error al obtener bodega: {response.StatusCode}"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse<BodegaDto>
                {
                    Success = false,
                    ErrorMessage = $"Error de conexión: {ex.Message}"
                };
            }
        }
    }
}