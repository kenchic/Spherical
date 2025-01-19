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
    }
}
