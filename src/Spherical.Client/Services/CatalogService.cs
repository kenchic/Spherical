using System.Net.Http.Json;
using System.Text.Json;
using Spherical.Client.DTO.Common;
using Spherical.Client.DTO.Spherical;

namespace Spherical.Client.Services
{
    public interface ICatalogService
    {
        Task<ApiResponse<IEnumerable<CatalogoDetalleModelo>>> GetDetallesAsync(string idCatalogo);
        Task<ApiResponse<CatalogoDetalleModelo>> CreateDetalleAsync(string idCatalogo, CatalogoDetalleModelo detalle);
        void SetAuth(string token);
    }

    public class CatalogService : ApiServiceBase, ICatalogService
    {
        public CatalogService(HttpClient httpClient) : base(httpClient) { }

        public async Task<ApiResponse<IEnumerable<CatalogoDetalleModelo>>> GetDetallesAsync(string idCatalogo)
        {
            try
            {
                EnsureToken();
                var response = await _httpClient.GetAsync($"api/v1/catalogos/{idCatalogo}/detalles");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<ApiResponse<IEnumerable<CatalogoDetalleModelo>>>(content, _jsonOptions);
                    return result ?? new ApiResponse<IEnumerable<CatalogoDetalleModelo>>();
                }
                return new ApiResponse<IEnumerable<CatalogoDetalleModelo>> { Success = false, ErrorMessage = $"Error: {response.StatusCode}" };
            }
            catch (Exception ex)
            {
                return new ApiResponse<IEnumerable<CatalogoDetalleModelo>> { Success = false, ErrorMessage = ex.Message };
            }
        }

        public async Task<ApiResponse<CatalogoDetalleModelo>> CreateDetalleAsync(string idCatalogo, CatalogoDetalleModelo detalle)
        {
            try
            {
                EnsureToken();
                var response = await _httpClient.PostAsJsonAsync($"api/v1/catalogos/{idCatalogo}/detalles", detalle, _jsonOptions);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<ApiResponse<CatalogoDetalleModelo>>(content, _jsonOptions);
                    return result ?? new ApiResponse<CatalogoDetalleModelo>();
                }
                return new ApiResponse<CatalogoDetalleModelo> { Success = false, ErrorMessage = $"Error: {response.StatusCode}" };
            }
            catch (Exception ex)
            {
                return new ApiResponse<CatalogoDetalleModelo> { Success = false, ErrorMessage = ex.Message };
            }
        }
    }
}
