using System.ComponentModel.DataAnnotations;
using Creative.Recursos;

namespace Creative.DTO.Tactic
{
	public class SistemaDTO
	{
		#region Propiedades

		[Key]
		[Required(AllowEmptyStrings = false, ErrorMessageResourceName = "MensajeRequerido", ErrorMessageResourceType = typeof(Idioma))]
		[Display(Name = "EtiquetaCodigo", ResourceType = typeof(Idioma))]
		public string Id { get; set; }
       
		[StringLength(maximumLength: 20, ErrorMessageResourceName = "MensajeTamanoMaximo", ErrorMessageResourceType = typeof(Idioma), MinimumLength = 1)]
		[Required(AllowEmptyStrings = false, ErrorMessageResourceName = "MensajeRequerido", ErrorMessageResourceType = typeof(Idioma))]
		[Display(Name = "EtiquetaVersion", ResourceType = typeof(Idioma))]
		public string Version { get; set; }

		#endregion
	}
}