using Newtonsoft.Json;
using RestSharp;
using Spherical.Client.DTO.Defender;
using Spherical.Client.DTO.Spherical;
using Spherical.Core.Creative;
using System.Net;

namespace Spherical.Client
{
    public class AutenticacionService
    {
        private string _urlApi = string.Empty;

        public AutenticacionService(string urlApi)
        {
            _urlApi = urlApi;
        }

        public string Autenticar(AutenticacionDTO autenticacionDTO)
        {
            try
            {
                RestClient cliente = new RestClient($"{_urlApi}/api/v1/seguridad");
                var peticion = new RestRequest("autenticar", Method.Post);
                peticion.AddJsonBody(autenticacionDTO);
                RestResponse response = cliente.Execute(peticion);

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    string respuesta = response.ErrorException.Message;
                    Log.Error(respuesta, "AutenticacionService - Autenticar");
                    throw new Exception(respuesta);
                }

                ApiResponse<string> resultado = JsonConvert.DeserializeObject<ApiResponse<string>>(response.Content);

                return resultado.Data;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, "AutenticacionService - Autenticar");
                throw;
            }
        }

        public async Task<string> AutenticarAsync(AutenticacionDTO autenticacionDTO)
        {
            try
            {
                // Crear cliente y definir la URL base
                var cliente = new RestClient($"{_urlApi}/api/v1/seguridad");

                // Crear la solicitud con el endpoint y el método
                var peticion = new RestRequest("autenticar", Method.Post);
                peticion.AddJsonBody(autenticacionDTO);

                // Ejecutar la solicitud de forma asíncrona
                RestResponse response = await cliente.ExecuteAsync(peticion);

                // Validar la respuesta
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    string respuesta = response.ErrorException?.Message ?? "Error desconocido";
                    Log.Error(respuesta, "AutenticacionService - AutenticarAsync");
                    throw new Exception(respuesta);
                }

                // Deserializar y retornar el resultado
                var resultado = JsonConvert.DeserializeObject<ApiResponse<string>>(response.Content);
                return resultado?.Data ?? throw new Exception("La respuesta del servidor fue nula o inválida.");
            }
            catch (Exception ex)
            {
                // Manejo de errores
                Log.Error(ex.Message, "AutenticacionService - AutenticarAsync");
                throw;
            }
        }

    }
}
