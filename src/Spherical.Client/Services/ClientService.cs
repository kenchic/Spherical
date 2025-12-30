using System.Net.Http.Json;
using System.Text.Json;
using Creative.Modelos.Lineup;
using Spherical.Client.DTO.Spherical;

namespace Spherical.Client.Services
{
    public interface IClientService
    {
        Task<ApiResponse<IEnumerable<VClienteModeloDto>>> GetClientesAsync();
        Task<ApiResponse<VClienteModeloDto>> GetClienteAsync(int id);
        Task<ApiResponse<ClienteModeloDto>> CreateClienteAsync(ClienteModeloDto cliente);
        void SetAuth(string token);
    }

    public class ClientService : ApiServiceBase, IClientService
    {
        public ClientService(HttpClient httpClient) : base(httpClient) { }

        public async Task<ApiResponse<IEnumerable<VClienteModeloDto>>> GetClientesAsync()
        {
            try
            {
                EnsureToken();
                var response = await _httpClient.GetAsync("api/v1/clientes");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<ApiResponse<IEnumerable<VClienteModeloDto>>>(content, _jsonOptions);
                    return result ?? new ApiResponse<IEnumerable<VClienteModeloDto>>();
                }
                return new ApiResponse<IEnumerable<VClienteModeloDto>> { Success = false, ErrorMessage = $"Error: {response.StatusCode}" };
            }
            catch (Exception ex)
            {
                return new ApiResponse<IEnumerable<VClienteModeloDto>> { Success = false, ErrorMessage = ex.Message };
            }
        }

        public async Task<ApiResponse<VClienteModeloDto>> GetClienteAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/v1/clientes/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<ApiResponse<VClienteModeloDto>>(content, _jsonOptions);
                    return result ?? new ApiResponse<VClienteModeloDto>();
                }
                return new ApiResponse<VClienteModeloDto> { Success = false, ErrorMessage = $"Error: {response.StatusCode}" };
            }
            catch (Exception ex)
            {
                return new ApiResponse<VClienteModeloDto> { Success = false, ErrorMessage = ex.Message };
            }
        }

        public async Task<ApiResponse<ClienteModeloDto>> CreateClienteAsync(ClienteModeloDto cliente)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/v1/clientes", cliente, _jsonOptions);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<ApiResponse<ClienteModeloDto>>(content, _jsonOptions);
                    return result ?? new ApiResponse<ClienteModeloDto>();
                }
                return new ApiResponse<ClienteModeloDto> { Success = false, ErrorMessage = $"Error: {response.StatusCode}" };
            }
            catch (Exception ex)
            {
                return new ApiResponse<ClienteModeloDto> { Success = false, ErrorMessage = ex.Message };
            }
        }
    }
}
