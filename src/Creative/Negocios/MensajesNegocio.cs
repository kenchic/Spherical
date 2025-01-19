using Creative.Modelos;
using Creative.Recursos;
using NLog;
using System;

namespace Creative.Negocios
{	public static class MensajeNegocio
	{
		/// <summary>
		/// Escribir en el archivo de log 
		/// </summary>
		/// <param name="tipo">tipo de mensaje (1:Exito, 2:Información, 3:Alerta, 4:Error)</param>
		/// <param name="mensaje"></param>
		public static void EscribirLog(string tipo, string mensaje, string origen)
		{
			Logger LogMensajes = LogManager.GetCurrentClassLogger();
			mensaje = string.Concat("Origen: ", origen, " Mensaje:", mensaje, "Hora:" + DateTime.Now);			
			if (tipo == Constantes.MensajeError)
			{
				LogMensajes.Error(mensaje);
			}
			else if (tipo == Constantes.MensajeAlerta)
			{
				LogMensajes.Warn(mensaje);
			}
			else if (tipo == Constantes.MensajeInfo)
			{
				LogMensajes.Info(mensaje);
			}
		}
	}
}