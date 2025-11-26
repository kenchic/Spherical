using Spherical.Client.DTO.Lineup;
using Spherical.Client.DTO.Spherical;
using Spherical.Client.DTO.Common;
using System.Net.Http.Json;
using System.Text.Json;

namespace Spherical.Client.Services
{
    public interface IElementoService
    {
        Task<ApiResponse<IEnumerable<ElementoDto>>> GetElementosAsync();
        Task<ApiResponse<ElementoDto>> GetElementoAsync(int id);
        Task<ApiResponse<ElementoDto>> CreateElementoAsync(ElementoDto elemento);
        Task<ApiResponse<string>> UpdateElementoAsync(int id, ElementoDto elemento);
        Task<ApiResponse<string>> DeleteElementoAsync(int id);
        Task<ApiResponse<IEnumerable<ListaPrecioDetalleDto>>> GetElementoPreciosAsync(int id);
        // Nuevo: obtener detalles de catálogo por idCatalogo
        Task<ApiResponse<IEnumerable<CatalogoDetalleModelo>>> GetCatalogoDetallesAsync(string idCatalogo);
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

        public async Task<ApiResponse<IEnumerable<ElementoDto>>> GetElementosAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/v1/elementos");
                
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<ApiResponse<IEnumerable<ElementoDto>>>(content, _jsonOptions);
                    return result ?? new ApiResponse<IEnumerable<ElementoDto>>();
                }
                else
                {
                    return new ApiResponse<IEnumerable<ElementoDto>>
                    {
                        Success = false,
                        ErrorMessage = $"Error al obtener elementos: {response.StatusCode}"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse<IEnumerable<ElementoDto>>
                {
                    Success = false,
                    ErrorMessage = $"Error de conexión: {ex.Message}"
                };
            }
        }

        public async Task<ApiResponse<ElementoDto>> GetElementoAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/v1/elementos/{id}");
                
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<ApiResponse<ElementoDto>>(content, _jsonOptions);
                    return result ?? new ApiResponse<ElementoDto>();
                }
                else
                {
                    return new ApiResponse<ElementoDto>
                    {
                        Success = false,
                        ErrorMessage = $"Error al obtener elemento: {response.StatusCode}"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse<ElementoDto>
                {
                    Success = false,
                    ErrorMessage = $"Error de conexión: {ex.Message}"
                };
            }
        }

        public async Task<ApiResponse<ElementoDto>> CreateElementoAsync(ElementoDto elemento)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/v1/elementos", elemento, _jsonOptions);
                
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<ApiResponse<ElementoDto>>(content, _jsonOptions);
                    return result ?? new ApiResponse<ElementoDto>();
                }
                else
                {
                    return new ApiResponse<ElementoDto>
                    {
                        Success = false,
                        ErrorMessage = $"Error al crear elemento: {response.StatusCode}"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse<ElementoDto>
                {
                    Success = false,
                    ErrorMessage = $"Error de conexión: {ex.Message}"
                };
            }
        }

        public async Task<ApiResponse<string>> UpdateElementoAsync(int id, ElementoDto elemento)
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

        public async Task<ApiResponse<IEnumerable<ListaPrecioDetalleDto>>> GetElementoPreciosAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/v1/elementos/{id}/precios");
                
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<ApiResponse<IEnumerable<ListaPrecioDetalleDto>>>(content, _jsonOptions);
                    return result ?? new ApiResponse<IEnumerable<ListaPrecioDetalleDto>>();
                }
                else
                {
                    return new ApiResponse<IEnumerable<ListaPrecioDetalleDto>>
                    {
                        Success = false,
                        ErrorMessage = $"Error al obtener precios: {response.StatusCode}"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse<IEnumerable<ListaPrecioDetalleDto>>
                {
                    Success = false,
                    ErrorMessage = $"Error de conexión: {ex.Message}"
                };
            }
        }

        // Nuevo método para obtener detalles de catálogo
        public async Task<ApiResponse<IEnumerable<CatalogoDetalleModelo>>> GetCatalogoDetallesAsync(string idCatalogo)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/v1/catalogos/{idCatalogo}/detalles");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<ApiResponse<IEnumerable<CatalogoDetalleModelo>>>(content, _jsonOptions);
                    return result ?? new ApiResponse<IEnumerable<CatalogoDetalleModelo>>();
                }
                else
                {
                    return new ApiResponse<IEnumerable<CatalogoDetalleModelo>>
                    {
                        Success = false,
                        ErrorMessage = $"Error al obtener detalles de catálogo: {response.StatusCode}"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse<IEnumerable<CatalogoDetalleModelo>>
                {
                    Success = false,
                    ErrorMessage = $"Error de conexión: {ex.Message}"
                };
            }
        }
    }
}