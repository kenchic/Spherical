using System.Net.Http.Json;
using System.Text.Json;
using Creative.Modelos.Lineup;
using Spherical.Client.DTO.Spherical;

namespace Spherical.Client.Services
{
    public interface IProjectService
    {
        Task<ApiResponse<IEnumerable<VProyectoModeloDto>>> GetProyectosAsync();
        Task<ApiResponse<VProyectoModeloDto>> GetProyectoAsync(int id);
        Task<ApiResponse<ProyectoModeloDto>> CreateProyectoAsync(ProyectoModeloDto proyecto);
        void SetAuth(string token);
    }

    public class ProjectService : ApiServiceBase, IProjectService
    {
        public ProjectService(HttpClient httpClient) : base(httpClient) { }

        public async Task<ApiResponse<IEnumerable<VProyectoModeloDto>>> GetProyectosAsync()
        {
            try
            {
                EnsureToken();
                var response = await _httpClient.GetAsync("api/v1/proyectos");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<ApiResponse<IEnumerable<VProyectoModeloDto>>>(content, _jsonOptions);
                    return result ?? new ApiResponse<IEnumerable<VProyectoModeloDto>>();
                }
                return new ApiResponse<IEnumerable<VProyectoModeloDto>> { Success = false, ErrorMessage = $"Error: {response.StatusCode}" };
            }
            catch (Exception ex)
            {
                return new ApiResponse<IEnumerable<VProyectoModeloDto>> { Success = false, ErrorMessage = ex.Message };
            }
        }

        public async Task<ApiResponse<VProyectoModeloDto>> GetProyectoAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/v1/proyectos/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<ApiResponse<VProyectoModeloDto>>(content, _jsonOptions);
                    return result ?? new ApiResponse<VProyectoModeloDto>();
                }
                return new ApiResponse<VProyectoModeloDto> { Success = false, ErrorMessage = $"Error: {response.StatusCode}" };
            }
            catch (Exception ex)
            {
                return new ApiResponse<VProyectoModeloDto> { Success = false, ErrorMessage = ex.Message };
            }
        }

        public async Task<ApiResponse<ProyectoModeloDto>> CreateProyectoAsync(ProyectoModeloDto proyecto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/v1/proyectos", proyecto, _jsonOptions);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<ApiResponse<ProyectoModeloDto>>(content, _jsonOptions);
                    return result ?? new ApiResponse<ProyectoModeloDto>();
                }
                return new ApiResponse<ProyectoModeloDto> { Success = false, ErrorMessage = $"Error: {response.StatusCode}" };
            }
            catch (Exception ex)
            {
                return new ApiResponse<ProyectoModeloDto> { Success = false, ErrorMessage = ex.Message };
            }
        }
    }
}
