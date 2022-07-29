using System.Collections.Generic;
using System.Reflection;
using System;
using CoreGeneral.Recursos;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace CoreGeneral.Negocios
{
    public static class UtilidadesNegocio
    {
        public static SelectList ListaSeleccion<T>(List<T> ienLista, string strValorCampo, string strTextoCampo, string strValorEntidad, string strItemVacio = "")
        {
            try
            {
                List<SelectListItem> list = new List<SelectListItem>();

                if (!string.IsNullOrEmpty(strItemVacio))
                {
                    list.Add(new SelectListItem()
                    {
                        Text = strItemVacio,
                        Value = "-1",
                        Selected = true
                    });
                }

                foreach (var item in ienLista)
                {
                    string strValor = string.Empty;
                    string strTexto = string.Empty;
                    foreach (PropertyInfo piInfo in typeof(T).GetProperties())
                    {
                        if (piInfo.Name == strTextoCampo)
                            strTexto = piInfo.GetValue(item, null).ToString();

                        if (piInfo.Name == strValorCampo)
                            strValor = piInfo.GetValue(item, null).ToString();
                    }

                    list.Add(new SelectListItem()
                    {
                        Text = strTexto,
                        Value = strValor,
                        Selected = strValorEntidad == strValor
                    });
                }
                
                return new SelectList(list, "Value", "Text");
            }
            catch (Exception ex)
            {
                MensajeNegocio.EscribirLog(Constantes.MensajeError, ex.Message, " CoreGeneral.Utilidades.ListaSeleccion ");
                throw;
            }
        }

        public static string CodigoAleatorio(int longitud)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, longitud)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}