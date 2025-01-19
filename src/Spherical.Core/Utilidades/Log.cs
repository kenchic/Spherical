using NLog;

namespace Spherical.Core.Creative
{	public static class Log
	{
		/// <summary>
		/// Escribir en el archivo de log 
		/// </summary>
		/// <param name="tipo">tipo de mensaje (1:Exito, 2:Información, 3:Alerta, 4:Error)</param>
		/// <param name="mensaje"></param>
		private static void EscribirLog(TipoLog tipo, string mensaje, string origen)
		{
			Logger LogMensajes = LogManager.GetCurrentClassLogger();
			mensaje = string.Concat("Origen: ", origen, " Mensaje:", mensaje, "Hora:" + DateTime.Now);			
			if (tipo == TipoLog.Error)
			{
				LogMensajes.Error(mensaje);
			}
			else if (tipo == TipoLog.Alerta)
			{
				LogMensajes.Warn(mensaje);
			}
			else if (tipo == TipoLog.Info)
			{
				LogMensajes.Info(mensaje);
			}
		}

		public static void Error(string mensaje, string origen)
        {
            EscribirLog(TipoLog.Error, mensaje, "Spherical");
        }

        public static void Info(string mensaje, string origen)
        {
            EscribirLog(TipoLog.Info, mensaje, "Spherical");
        }

        public static void Alerta(string mensaje, string origen)
        {
            EscribirLog(TipoLog.Alerta, mensaje, "Spherical");
        }
    }

	public enum TipoLog
    {
        Error = 1,
        Info = 2,
        Alerta = 3
    }
}