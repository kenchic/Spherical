using Spherical.Client.DTO.Inventory;
using Spherical.Client.DTO.Spherical;
using System.Net.Http.Json;
using System.Text.Json;

namespace Spherical.Client.Services
{
    public interface IDocumentoService
    {
        Task<ApiResponse<IEnumerable<DocumentoTipoDto>>> GetTiposAsync();
        Task<ApiResponse<IEnumerable<DocumentoDto>>> GetDocumentosAsync();
        Task<ApiResponse<DocumentoDto>> GetDocumentoAsync(int id);
        Task<ApiResponse<bool>> CrearDocumentoAsync(DocumentoDto documento);
        Task<ApiResponse<bool>> ActualizarDocumentoAsync(long id, DocumentoDto documento);
        Task<ApiResponse<bool>> AnularDocumentoAsync(int id);
        void SetAuth(string token);
    }

    public class DocumentoService : ApiServiceBase, IDocumentoService
    {
        public DocumentoService(HttpClient httpClient) : base(httpClient) { }

        public async Task<ApiResponse<IEnumerable<DocumentoTipoDto>>> GetTiposAsync()
        {
            try
            {
                EnsureToken();
                var response = await _httpClient.GetAsync("api/v1/documentos/tipos");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<ApiResponse<IEnumerable<DocumentoTipoDto>>>(content, _jsonOptions);
                    return result ?? new ApiResponse<IEnumerable<DocumentoTipoDto>>();
                }
                else
                {
                    return new ApiResponse<IEnumerable<DocumentoTipoDto>>
                    {
                        Success = false,
                        ErrorMessage = $"Error al obtener tipos de documento: {response.StatusCode}"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse<IEnumerable<DocumentoTipoDto>>
                {
                    Success = false,
                    ErrorMessage = $"Error de conexión: {ex.Message}"
                };
            }
        }

        public async Task<ApiResponse<IEnumerable<DocumentoDto>>> GetDocumentosAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/v1/documentos");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<ApiResponse<IEnumerable<DocumentoDto>>>(content, _jsonOptions);
                    return result ?? new ApiResponse<IEnumerable<DocumentoDto>>();
                }
                else
                {
                    return new ApiResponse<IEnumerable<DocumentoDto>>
                    {
                        Success = false,
                        ErrorMessage = $"Error al obtener documentos: {response.StatusCode}"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse<IEnumerable<DocumentoDto>>
                {
                    Success = false,
                    ErrorMessage = $"Error de conexión: {ex.Message}"
                };
            }
        }

        public async Task<ApiResponse<DocumentoDto>> GetDocumentoAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/v1/documentos/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<ApiResponse<DocumentoDto>>(content, _jsonOptions);
                    return result ?? new ApiResponse<DocumentoDto>();
                }
                else
                {
                    return new ApiResponse<DocumentoDto>
                    {
                        Success = false,
                        ErrorMessage = $"Error al obtener documento: {response.StatusCode}"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse<DocumentoDto>
                {
                    Success = false,
                    ErrorMessage = $"Error de conexión: {ex.Message}"
                };
            }
        }

        public async Task<ApiResponse<bool>> CrearDocumentoAsync(DocumentoDto documento)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/v1/documentos", documento);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<ApiResponse<bool>>(content, _jsonOptions);
                    return result ?? new ApiResponse<bool>();
                }
                else
                {
                    return new ApiResponse<bool>
                    {
                        Success = false,
                        ErrorMessage = $"Error al crear documento: {response.StatusCode}"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    ErrorMessage = $"Error de conexión: {ex.Message}"
                };
            }
        }

        public async Task<ApiResponse<bool>> ActualizarDocumentoAsync(long id, DocumentoDto documento)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/v1/documentos/{id}", documento);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<ApiResponse<bool>>(content, _jsonOptions);
                    return result ?? new ApiResponse<bool>();
                }
                else
                {
                    return new ApiResponse<bool>
                    {
                        Success = false,
                        ErrorMessage = $"Error al crear documento: {response.StatusCode}"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    ErrorMessage = $"Error de conexión: {ex.Message}"
                };
            }
        }

        public async Task<ApiResponse<bool>> AnularDocumentoAsync(int id)
        {
            try
            {
                EnsureToken();
                var request = new HttpRequestMessage(HttpMethod.Put, $"api/v1/documentos/{id}/anular");
                var response = await _httpClient.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<ApiResponse<bool>>(content, _jsonOptions);
                    return result ?? new ApiResponse<bool>();
                }
                else
                {
                    return new ApiResponse<bool>
                    {
                        Success = false,
                        ErrorMessage = $"Error al anular documento: {response.StatusCode}"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    ErrorMessage = $"Error de conexión: {ex.Message}"
                };
            }
        }
    }
}
