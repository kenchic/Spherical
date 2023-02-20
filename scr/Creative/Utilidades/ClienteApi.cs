using Creative.DTO.Spherical;
using Creative.Negocios;
using Creative.Recursos;
using RestSharp;

namespace Creative.Utilidades
{
    public static class ClienteApi
    {
        private static ApiResponse<T> GetRecursoApi<T>(string urlBase, string recurso, Dictionary<string, string>? parametros, object? cuerpo, string? token) where T : class
        {
            try
            {
                RestClient cliente = new RestClient(urlBase);
                var request = new RestRequest(recurso);
                if (parametros != null)
                {
                    foreach (var parametro in parametros)
                    {
                        request.AddParameter(parametro.Key, parametro.Value);
                    }
                }

                if (cuerpo != null)
                {
                    request.AddJsonBody(cuerpo);
                }

                if (!string.IsNullOrEmpty(token))
                {
                    request.AddHeader("autorizacion", token);
                }

                var response = cliente.GetAsync<ApiResponse<T>>(request).GetAwaiter().GetResult();
                if (response != null)
                {
                    return response;
                }
                else
                {
                    return new ApiResponse<T>();
                }
            }
            catch (Exception ex)
            {
                MensajeNegocio.EscribirLog(Constantes.MensajeError, ex.Message, "ClienteApi - GetRecursoApi");
                return new ApiResponse<T>();
            }
        }

        public static ApiResponse<T> GetRecurso<T>(string urlBase, string recurso) where T : class
        {
            return GetRecursoApi<T>(urlBase, recurso, null, null, null);
        }
        public static ApiResponse<T> GetRecurso<T>(string urlBase, string recurso, string token) where T : class
        {
            return GetRecursoApi<T>(urlBase, recurso, null, null, token);
        }

        public static ApiResponse<T> GetRecurso<T>(string urlBase, string recurso, Dictionary<string, string> parametros) where T : class
        {
            return GetRecursoApi<T>(urlBase, recurso, parametros, null, null);
        }

        public static ApiResponse<T> GetRecurso<T>(string urlBase, string recurso, Dictionary<string, string> parametros, object cuerpo) where T : class
        {
            return GetRecursoApi<T>(urlBase, recurso, parametros, cuerpo, null);
        }

        public static ApiResponse<T> GetRecurso<T>(string urlBase, string recurso, Dictionary<string, string> parametros, string token) where T : class
        {
            return GetRecursoApi<T>(urlBase, recurso, parametros, null, token);
        }

        public static ApiResponse<T> GetRecurso<T>(string urlBase, string recurso, object cuerpo, string token) where T : class
        {
            return GetRecursoApi<T>(urlBase, recurso, null, cuerpo, token);
        }

        public static ApiResponse<T> GetRecurso<T>(string urlBase, string recurso, Dictionary<string, string> parametros, object cuerpo, string token) where T : class
        {
            return GetRecursoApi<T>(urlBase, recurso, parametros, cuerpo, token);
        }

        private static string PostRecursoApi(string urlBase, string recurso, object cuerpo, string token)
        {
            var valor = string.Empty;
            try
            {
                RestClient cliente = new RestClient(urlBase);
                var request = new RestRequest(recurso);

                if (cuerpo != null)
                {
                    request.AddJsonBody(cuerpo);
                }

                if (!string.IsNullOrEmpty(token))
                {
                    request.AddHeader("autorizacion", token);
                }

                var response = cliente.PostAsync(request).GetAwaiter().GetResult();
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    valor = response.Content;
                }
            }
            catch (Exception ex)
            {
                MensajeNegocio.EscribirLog(Constantes.MensajeError, ex.Message, "ClienteApi - PostRecursoApi");
                return null;
            }
            return valor;
        }

        private static void PostRecursoApiAsync(string urlBase, string recurso, object cuerpo, string token)
        {
            try
            {
                RestClient cliente = new RestClient(urlBase);
                var request = new RestRequest(recurso);

                if (cuerpo != null)
                {
                    request.AddJsonBody(cuerpo);
                }

                if (!string.IsNullOrEmpty(token))
                {
                    request.AddHeader("autorizacion", token);
                }

                var reponse = cliente.PostAsync(request);
                if (reponse.IsCompleted)
                {

                }
            }
            catch (Exception ex)
            {
                MensajeNegocio.EscribirLog(Constantes.MensajeError, ex.Message, "ClienteApi - ObtenerRecurso");
            }
        }

        public static string PostRecurso(string urlBase, string recurso, object cuerpo)
        {
            return PostRecursoApi(urlBase, recurso, cuerpo, null);
        }

        public static string PostRecurso(string urlBase, string recurso, object cuerpo, string token)
        {
            return PostRecursoApi(urlBase, recurso, cuerpo, token);
        }

        public static void PostRecursoAsync(string urlBase, string recurso, object cuerpo, string token)
        {
            PostRecursoApiAsync(urlBase, recurso, cuerpo, token);
        }

        //public static T ObtenerRecurso<T>(string urlBase, string recurso, Dictionary<string, string> parametros = null) where T : new()
        //{
        //    T entidad = new();
        //    try
        //    {
        //        RestClient cliente = new RestClient(urlBase);
        //        var request = new RestRequest(recurso);
        //        if (parametros != null)
        //        {
        //            foreach (var parametro in parametros)
        //            {
        //                request.AddParameter(parametro.Key, parametro.Value);
        //            }
        //        }
        //        var response = cliente.GetAsync(request).GetAwaiter().GetResult();
        //        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        //        {
        //            entidad = JsonConvert.DeserializeObject<T>(response.Content);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MensajeNegocio.EscribirLog(Constantes.MensajeError, ex.Message, "ClienteApi - ObtenerRecurso");
        //        return new();
        //    }
        //    return entidad;
        //}
    }
}
