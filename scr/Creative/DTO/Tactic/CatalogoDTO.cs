using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Creative.Recursos;

namespace Creative.DTO.Tactic
{
	public class CatalogoDTO
	{
		#region Propiedades

		[Key]
		[StringLength(20, ErrorMessageResourceName = "MensajeTamanoMaximo", ErrorMessageResourceType = typeof(Idioma))]
		[Required(AllowEmptyStrings = false, ErrorMessageResourceName = "MensajeRequerido", ErrorMessageResourceType = typeof(Idioma))]
		[Display(Name = "EtiquetaId", ResourceType = typeof(Idioma))]
		public string Id { get; set; }

		[StringLength(10, ErrorMessageResourceName = "MensajeTamanoMaximo", ErrorMessageResourceType = typeof(Idioma))]
		[Display(Name = "EtiquetaCodigo", ResourceType = typeof(Idioma))]
		public string Sistema { get; set; }

		[StringLength(100, ErrorMessageResourceName = "MensajeTamanoMaximo", ErrorMessageResourceType = typeof(Idioma))]
		[Required(AllowEmptyStrings = false, ErrorMessageResourceName = "MensajeRequerido", ErrorMessageResourceType = typeof(Idioma))]
		[Display(Name = "EtiquetaDescripcion", ResourceType = typeof(Idioma))]
		public string Descripcion { get; set; }

		public List<CatalogoDetalleDTO> Detalle { get; set; }

		#endregion
	}
}