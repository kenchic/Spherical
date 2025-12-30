using Spherical.Client.DTO.Defender;
using Spherical.Client.DTO.Spherical;
using System.Text.Json;

namespace Spherical.Client.Services
{
    public interface IMenuService
    {
        Task<ApiResponse<List<UserMenuDTO>>> GetAsync(string user, string company);
        void SetAuth(string token);
    }

    public class MenuService : ApiServiceBase, IMenuService
    {
        public MenuService(HttpClient httpClient) : base(httpClient) { }

        public async Task<ApiResponse<List<UserMenuDTO>>> GetAsync(string user, string company)
        {
            try
            {
                EnsureToken();
                var response = await _httpClient.GetAsync($"api/v1/menu/{user}/{company}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<ApiResponse<List<UserMenuDTO>>>(content, _jsonOptions);
                    return result ?? new ApiResponse<List<UserMenuDTO>>();
                }
                else
                {
                    return new ApiResponse<List<UserMenuDTO>>
                    {
                        Success = false,
                        StatusCode = (int)response.StatusCode,
                        ErrorMessage = $"Error al obtener menu: {response.StatusCode}"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<UserMenuDTO>>
                {
                    Success = false,
                    ErrorMessage = $"Error de conexión: {ex.Message}"
                };
            }
        }
    }
}
