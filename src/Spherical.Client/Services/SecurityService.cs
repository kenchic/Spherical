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

        public string Login(LoginDTO autenticacionDTO)
        {
            try
            {
                RestClient cliente = new RestClient($"{_urlApi}/api/v1/security");
                var peticion = new RestRequest("login", Method.Post);
                peticion.AddJsonBody(autenticacionDTO);
                RestResponse response = cliente.Execute(peticion);

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    string respuesta = response.ErrorException.Message;
                    Log.Error(respuesta, "SecurityService - Login");
                    throw new Exception(respuesta);
                }

                ApiResponse<string> resultado = JsonConvert.DeserializeObject<ApiResponse<string>>(response.Content);

                return resultado.Data;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, "SecurityService - Login");
                throw;
            }
        }

        public async Task<string> LoginAsync(LoginDTO autenticacionDTO)
        {
            try
            {
                // Crear client y definir la URL base
                var client = new RestClient($"{_urlApi}/api/v1/security");

                // Crear la solicitud con el endpoint y el método
                var request = new RestRequest("login", Method.Post);
                request.AddJsonBody(autenticacionDTO);

                // Ejecutar la solicitud de forma asíncrona
                RestResponse response = await client.ExecuteAsync(request);

                // Validar la messageError
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    string messageError = response.ErrorException?.Message ?? "Error desconocido";
                    Log.Error(messageError, "SecurityService - LoginAsync");
                    throw new Exception(messageError);
                }

                // Deserializar y retornar el resultado
                var result = JsonConvert.DeserializeObject<ApiResponse<string>>(response.Content);
                return result?.Data ?? throw new Exception("La messageError del servidor fue nula o inválida.");
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
