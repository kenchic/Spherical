using Spherical.Client.DTO.Inventory;
using Spherical.Client.DTO.Spherical;
using System.Net.Http.Json;
using System.Text.Json;

namespace Spherical.Client.Services
{
    public interface IDocumentoService
    {
        Task<ApiResponse<IEnumerable<DocumentoTipoDto>>> GetTiposAsync();
        Task<ApiResponse<DocumentoDto>> CrearDocumentoAsync(DocumentoDto documento);
    }

    public class DocumentoService : IDocumentoService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public DocumentoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApiResponse<IEnumerable<DocumentoTipoDto>>> GetTiposAsync()
        {
            try
            {
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

        public async Task<ApiResponse<DocumentoDto>> CrearDocumentoAsync(DocumentoDto documento)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/v1/documentos", documento);
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
                        ErrorMessage = $"Error al crear documento: {response.StatusCode}"
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
    }
}