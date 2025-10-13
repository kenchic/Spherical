using Spherical.Client.DTO.Lineup;
using Spherical.Client.DTO.Spherical;
using System.Net.Http.Json;
using System.Text.Json;

namespace Spherical.Client.Services
{
    public interface IElementoService
    {
        Task<ApiResponse<IEnumerable<ElementoDTO>>> GetElementosAsync();
        Task<ApiResponse<ElementoDTO>> GetElementoAsync(int id);
        Task<ApiResponse<ElementoDTO>> CreateElementoAsync(ElementoDTO elemento);
        Task<ApiResponse<string>> UpdateElementoAsync(int id, ElementoDTO elemento);
        Task<ApiResponse<string>> DeleteElementoAsync(int id);
        Task<ApiResponse<IEnumerable<ListaPrecioDetalleModelo>>> GetElementoPreciosAsync(int id);
    }

    public class ElementoService : IElementoService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public ElementoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<ApiResponse<IEnumerable<ElementoDTO>>> GetElementosAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/v1/elementos");
                
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<ApiResponse<IEnumerable<ElementoDTO>>>(content, _jsonOptions);
                    return result ?? new ApiResponse<IEnumerable<ElementoDTO>>();
                }
                else
                {
                    return new ApiResponse<IEnumerable<ElementoDTO>>
                    {
                        Success = false,
                        ErrorMessage = $"Error al obtener elementos: {response.StatusCode}"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse<IEnumerable<ElementoDTO>>
                {
                    Success = false,
                    ErrorMessage = $"Error de conexión: {ex.Message}"
                };
            }
        }

        public async Task<ApiResponse<ElementoDTO>> GetElementoAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/v1/elementos/{id}");
                
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<ApiResponse<ElementoDTO>>(content, _jsonOptions);
                    return result ?? new ApiResponse<ElementoDTO>();
                }
                else
                {
                    return new ApiResponse<ElementoDTO>
                    {
                        Success = false,
                        ErrorMessage = $"Error al obtener elemento: {response.StatusCode}"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse<ElementoDTO>
                {
                    Success = false,
                    ErrorMessage = $"Error de conexión: {ex.Message}"
                };
            }
        }

        public async Task<ApiResponse<ElementoDTO>> CreateElementoAsync(ElementoDTO elemento)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/v1/elementos", elemento, _jsonOptions);
                
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<ApiResponse<ElementoDTO>>(content, _jsonOptions);
                    return result ?? new ApiResponse<ElementoDTO>();
                }
                else
                {
                    return new ApiResponse<ElementoDTO>
                    {
                        Success = false,
                        ErrorMessage = $"Error al crear elemento: {response.StatusCode}"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse<ElementoDTO>
                {
                    Success = false,
                    ErrorMessage = $"Error de conexión: {ex.Message}"
                };
            }
        }

        public async Task<ApiResponse<string>> UpdateElementoAsync(int id, ElementoDTO elemento)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/v1/elementos/{id}", elemento, _jsonOptions);
                
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<ApiResponse<string>>(content, _jsonOptions);
                    return result ?? new ApiResponse<string> { Success = true };
                }
                else
                {
                    return new ApiResponse<string>
                    {
                        Success = false,
                        ErrorMessage = $"Error al actualizar elemento: {response.StatusCode}"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse<string>
                {
                    Success = false,
                    ErrorMessage = $"Error de conexión: {ex.Message}"
                };
            }
        }

        public async Task<ApiResponse<string>> DeleteElementoAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/v1/elementos/{id}");
                
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<ApiResponse<string>>(content, _jsonOptions);
                    return result ?? new ApiResponse<string> { Success = true };
                }
                else
                {
                    return new ApiResponse<string>
                    {
                        Success = false,
                        ErrorMessage = $"Error al eliminar elemento: {response.StatusCode}"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse<string>
                {
                    Success = false,
                    ErrorMessage = $"Error de conexión: {ex.Message}"
                };
            }
        }

        public async Task<ApiResponse<IEnumerable<ListaPrecioDetalleModelo>>> GetElementoPreciosAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/v1/elementos/{id}/precios");
                
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<ApiResponse<IEnumerable<ListaPrecioDetalleModelo>>>(content, _jsonOptions);
                    return result ?? new ApiResponse<IEnumerable<ListaPrecioDetalleModelo>>();
                }
                else
                {
                    return new ApiResponse<IEnumerable<ListaPrecioDetalleModelo>>
                    {
                        Success = false,
                        ErrorMessage = $"Error al obtener precios: {response.StatusCode}"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse<IEnumerable<ListaPrecioDetalleModelo>>
                {
                    Success = false,
                    ErrorMessage = $"Error de conexión: {ex.Message}"
                };
            }
        }
    }
}