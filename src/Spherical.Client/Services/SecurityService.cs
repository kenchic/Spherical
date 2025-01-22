using Newtonsoft.Json;
using RestSharp;
using Spherical.Client.DTO.Defender;
using Spherical.Client.DTO.Spherical;
using Spherical.Core.Creative;
using System.Net;

namespace Spherical.Client
{
    public class SecurityService
    {
        private string _urlApi = string.Empty;

        public SecurityService(string urlApi)
        {
            _urlApi = urlApi;
        }

        public string Login(LoginDTO dto)
        {
            try
            {
                RestClient client = new RestClient($"{_urlApi}/api/v1/security");
                var peticion = new RestRequest("login", Method.Post);
                peticion.AddJsonBody(dto);
                RestResponse response = client.Execute(peticion);

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    string resp = response.ErrorException.Message;
                    Log.Error(resp, "SecurityService - Login");
                    throw new Exception(resp);
                }

                ApiResponse<string> result = JsonConvert.DeserializeObject<ApiResponse<string>>(response.Content);

                return result.Data;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, "SecurityService - Login");
                throw;
            }
        }

        public async Task<string> LoginAsync(LoginDTO dto)
        {
            try
            {
                // Crear client y definir la URL base
                var client = new RestClient($"{_urlApi}/api/v1/security");

                // Crear la solicitud con el endpoint y el método
                var request = new RestRequest("login", Method.Post);
                request.AddJsonBody(dto);

                // Ejecutar la solicitud de forma asíncrona
                RestResponse response = await client.ExecuteAsync(request);

                // Validar la errorMessage
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    string errorMessage = response.ErrorException?.Message ?? "Error desconocido";
                    Log.Error(errorMessage, "SecurityService - LoginAsync");
                    throw new Exception(errorMessage);
                }

                // Deserializar y retornar el result
                var result = JsonConvert.DeserializeObject<ApiResponse<string>>(response.Content);
                return result?.Data ?? throw new Exception("La errorMessage del servidor fue nula o inválida.");
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
