using System;
using System.ComponentModel.DataAnnotations;

namespace CoreGeneral.Modelos.SEG
{
	public class UsuarioMenuModelo
	{
		#region Propiedades

		[Display(Name = "Id")]
		public string Id { get; set; }

		[Display(Name = "Nombre")]
		public string Nombre { get; set; }

		[Display(Name = "Vista")]
		public string Vista { get; set; }

		[Display(Name = "Orden")]
		public Int32 Orden { get; set; }

		[Display(Name = "SubOrden")]
		public Int32? SubOrden { get; set; }

		[Display(Name = "Image")]
		public string Imagen { get; set; }		

		#endregion
	}
}
