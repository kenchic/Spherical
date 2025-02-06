using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using Spherical.Client.DTO.Defender;
using Spherical.Client.DTO.Spherical;
using Spherical.Core.Creative;
using System.Net;

namespace Spherical.Client.Services
{
    public class MenuService
    {
        private string _urlApi = string.Empty;
        
        private string _token= string.Empty;


        public MenuService(string urlApi, string token)
        {
            _urlApi = urlApi;
            _token = token;
        }

        public async Task<List<UserMenuDTO>> GetAsync(string user, string company)
        {
            try
            {
                // Crear client y definir la URL base
                var client = new RestClient($"{_urlApi}/api/v1/menu/{user}/{company}");                
                client.AddDefaultHeader("Authorization", $"Bearer {_token}");
                // Crear la solicitud con el endpoint y el método
                var request = new RestRequest("", Method.Get);

                // Ejecutar la solicitud de forma asíncrona
                RestResponse response = await client.ExecuteAsync(request);

                // Validar la errorMessage
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    string errorMessage = response.ErrorException?.Message ?? "Error desconocido";
                    Log.Error(errorMessage, "SecurityService - LoginAsync");
                    throw new Exception(errorMessage);
                }
                else
                {
                    if (response.Content != null)
                    {
                        var menu = JsonConvert.DeserializeObject<ApiResponse<List<UserMenuDTO>>>(response.Content);
                        return menu!.Data;
                    }
                }
                return new List<UserMenuDTO>();
                
            }
            catch (Exception ex)
            {
                // Manejo de errores
                Log.Error(ex.Message, "SecurityService - LoginAsync");
                throw;
            }
        }
    }
}
