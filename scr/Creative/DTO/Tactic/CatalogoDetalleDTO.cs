using System;
using System.ComponentModel.DataAnnotations;
using Creative.Recursos;

namespace Creative.DTO.Tactic
{
	public class CatalogoDetalleDTO
	{
		#region Propiedades
		
		[Key]
		[StringLength(20, ErrorMessageResourceName = "MensajeTamanoMaximo", ErrorMessageResourceType = typeof(Idioma))]
		[Display(Name = "EtiquetaId", ResourceType = typeof(Idioma))]
		public string Codigo { get; set; }

		[StringLength(100, ErrorMessageResourceName = "MensajeTamanoMaximo", ErrorMessageResourceType = typeof(Idioma))]
		[Required(AllowEmptyStrings = false, ErrorMessageResourceName = "MensajeRequerido", ErrorMessageResourceType = typeof(Idioma))]
		[Display(Name = "EtiquetaDescripcion", ResourceType = typeof(Idioma))]
		public string Nombre { get; set; }

		[StringLength(10, ErrorMessageResourceName = "MensajeTamanoMaximo", ErrorMessageResourceType = typeof(Idioma))]
		[Display(Name = "EtiquetaValor", ResourceType = typeof(Idioma))]
		public string ValorCadena{ get; set; }

		[Display(Name = "EtiquetaValor", ResourceType = typeof(Idioma))]
		public int? ValorNumero { get; set; }

		[Display(Name = "EtiquetaValor", ResourceType = typeof(Idioma))]
		public decimal? ValorDecimal { get; set; }

		[Required(AllowEmptyStrings = false, ErrorMessageResourceName = "MensajeRequerido", ErrorMessageResourceType = typeof(Idioma))]
		[Display(Name = "EtiquetaActivo", ResourceType = typeof(Idioma))]
		public bool Activo { get; set; }
				
		[Display(Name = "EtiquetaActivo", ResourceType = typeof(Idioma))]
		public string Action { get; set; }

		#endregion
	}
}