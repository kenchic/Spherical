using CoreGeneral.Negocios;
using CoreGeneral.Recursos;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreGeneral.Utilidades
{
    public static class ClienteApi
    {
        private static string GetRecursoApi(string urlBase, string recurso, Dictionary<string, string> parametros, object cuerpo, string token)
        {
            var valor = string.Empty;
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

                var response = cliente.GetAsync(request).GetAwaiter().GetResult();
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    valor = response.Content;
                }
            }
            catch (Exception ex)
            {
                MensajeNegocio.EscribirLog(Constantes.MensajeError, ex.Message, "ClienteApi - GetRecursoApi");
                return null;
            }
            return valor;
        }

        public static string GetRecurso(string urlBase, string recurso)
        {
            return GetRecursoApi(urlBase, recurso, null, null, null);
        }
        public static string GetRecurso(string urlBase, string recurso, string token)
        {
            return GetRecursoApi(urlBase, recurso, null, null, token);
        }

        public static string GetRecurso(string urlBase, string recurso, Dictionary<string, string> parametros)
        {
            return GetRecursoApi(urlBase, recurso, parametros, null, null);
        }

        public static string GetRecurso(string urlBase, string recurso, Dictionary<string, string> parametros, object cuerpo)
        {
            return GetRecursoApi(urlBase, recurso, parametros, cuerpo, null);
        }

        public static string GetRecurso(string urlBase, string recurso, Dictionary<string, string> parametros, string token)
        {
            return GetRecursoApi(urlBase, recurso, parametros, null, token);
        }

        public static string GetRecurso(string urlBase, string recurso, object cuerpo, string token)
        {
            return GetRecursoApi(urlBase, recurso, null, cuerpo, token);
        }

        public static string GetRecurso(string urlBase, string recurso, Dictionary<string, string> parametros, object cuerpo, string token)
        {
            return GetRecursoApi(urlBase, recurso, parametros, cuerpo, token);
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
                if(reponse.IsCompleted)
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
