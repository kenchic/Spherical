using System;
using System.ComponentModel.DataAnnotations;
using CoreGeneral.Recursos;

namespace CoreGeneral.DTO.GES
{
	public class CatalogoDetalleDTO
	{
		#region Propiedades

		[Key]
		[StringLength(20, ErrorMessageResourceName = "MensajeTamanoMaximo", ErrorMessageResourceType = typeof(Idioma))]
		[Required(AllowEmptyStrings = false, ErrorMessageResourceName = "MensajeRequerido", ErrorMessageResourceType = typeof(Idioma))]
		[Display(Name = "EtiquetaId", ResourceType = typeof(Idioma))]
		public string IdCatalogo { get; set; }

		[StringLength(20, ErrorMessageResourceName = "MensajeTamanoMaximo", ErrorMessageResourceType = typeof(Idioma))]
		[Display(Name = "EtiquetaId", ResourceType = typeof(Idioma))]
		public string Id { get; set; }

		[StringLength(100, ErrorMessageResourceName = "MensajeTamanoMaximo", ErrorMessageResourceType = typeof(Idioma))]
		[Required(AllowEmptyStrings = false, ErrorMessageResourceName = "MensajeRequerido", ErrorMessageResourceType = typeof(Idioma))]
		[Display(Name = "EtiquetaDescripcion", ResourceType = typeof(Idioma))]
		public String Nombre { get; set; }

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

		#endregion
	}
}