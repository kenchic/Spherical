using NLog;

namespace Spherical.Core.Creative
{	public static class Log
	{
		/// <summary>
		/// Escribir en el archivo de log 
		/// </summary>
		/// <param name="type">type de message (1:Exito, 2:Información, 3:Advertencia, 4:Error)</param>
		/// <param name="message"></param>
		private static void Write(LogType type, string message, string source)
		{
			Logger LogMensajes = LogManager.GetCurrentClassLogger();
			message = string.Concat("Origen: ", source, " Mensaje:", message, "Hora:" + DateTime.Now);			
			if (type == LogType.Error)
			{
				LogMensajes.Error(message);
			}
			else if (type == LogType.Warning)
			{
				LogMensajes.Warn(message);
			}
			else if (type == LogType.Info)
			{
				LogMensajes.Info(message);
			}
		}

		public static void Error(string message, string source)
        {
            Write(LogType.Error, message, source);
        }

        public static void Info(string message, string source)
        {
            Write(LogType.Info, message, source);
        }

        public static void Alerta(string message, string source)
        {
            Write(LogType.Warning, message, source);
        }
    }

	public enum LogType
    {
        Error = 1,
        Info = 2,
        Warning = 3
    }
}