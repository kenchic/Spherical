using System;
using System.ComponentModel.DataAnnotations;

namespace CoreGeneral.Modelos.SEG
{
    public class AutenticacionModelo
    {
		[Required()]
		public string Usuario { get; set; }

		[Required()]
		public string Clave { get; set; }

		public string Token { get; set; }

		public string Terminal { get; set; }

		public DateTime FechaInicio { get; set; }

		public DateTime FechaFin { get; set; }
	}
}
