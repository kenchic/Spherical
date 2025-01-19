using System;
using System.ComponentModel.DataAnnotations;

namespace Creative.DTO.Defender
{
	public class UsuarioMenuDTO222
	{
		#region Propiedades

		[Display(Name = "Id")]
		public string Id { get; set; }

		[Display(Name = "Nombre")]
		public string Nombre { get; set; }

		[Display(Name = "Vista")]
		public string Vista { get; set; }

		[Display(Name = "Orden")]
		public int Orden { get; set; }

		[Display(Name = "SubOrden")]
		public string Padre { get; set; }

		[Display(Name = "Image")]
		public string Imagen { get; set; }		

		#endregion
	}
}
