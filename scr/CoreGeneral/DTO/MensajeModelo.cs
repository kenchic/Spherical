using System;

namespace CoreGeneral.Modelos
{
	public class MensajeModelo
	{
		public string Tipo { get; set; }

		public string Mensaje { get; set; }

		public string Fecha { get; set; }

		/// <summary>
		/// Constructor básico de la clase notificación
		/// </summary>
		/// <param name="tipo">tipo de notificación (1:Exito, 2:Información, 3:Alerta, 4:Error)</param>
		/// <param name="mensaje"></param>
		public MensajeModelo(string tipo, string mensaje)
		{
			Tipo = tipo;
			Mensaje = mensaje;
			Fecha = DateTime.Now.ToString("t");
		}
	}
}