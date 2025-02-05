using Newtonsoft.Json;
using RestSharp;
using Spherical.Client.DTO.Defender;
using Spherical.Client.DTO.Spherical;
using Spherical.Core.Creative;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Spherical.Client.Services
{
    public class UserService
    {
        private string _urlApi = string.Empty;

        public UserService(string urlApi)
        {
            _urlApi = urlApi;
        }

        public async Task<string> GetAsync()
        {
            try
            {
                // Crear client y definir la URL base
                var client = new RestClient($"{_urlApi}/api/v1/users");

                // Crear la solicitud con el endpoint y el método
                var request = new RestRequest("login", Method.Get);

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
