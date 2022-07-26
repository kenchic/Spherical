using System;
using System.ComponentModel.DataAnnotations;
using Creative.Recursos;

namespace Creative.DTO.Tactic
{
	public class TicketDTO
	{
		#region Propiedades
		[Required(AllowEmptyStrings = false, ErrorMessageResourceName = "MensajeRequerido", ErrorMessageResourceType = typeof(Idioma))]
		[StringLength(50, ErrorMessageResourceName = "MensajeTamanoMaximo", ErrorMessageResourceType = typeof(Idioma))]
		[Display(Name = "EtiquetaCodigo", ResourceType = typeof(Idioma))]
		public int Id { get; set; }

		[Required(AllowEmptyStrings = false, ErrorMessageResourceName = "MensajeRequerido", ErrorMessageResourceType = typeof(Idioma))]
		[StringLength(50, ErrorMessageResourceName = "MensajeTamanoMaximo", ErrorMessageResourceType = typeof(Idioma))]
		[Display(Name = "EtiquetaTitulo", ResourceType = typeof(Idioma))]
		public string Titulo { get; set; }

		[StringLength(4000, ErrorMessageResourceName = "MensajeTamanoMaximo", ErrorMessageResourceType = typeof(Idioma))]
		[Required(AllowEmptyStrings = false, ErrorMessageResourceName = "MensajeRequerido", ErrorMessageResourceType = typeof(Idioma))]
		[Display(Name = "EtiquetaDescripcion", ResourceType = typeof(Idioma))]
		public string Descripcion { get; set; }

		[Required(AllowEmptyStrings = false, ErrorMessageResourceName = "MensajeRequerido", ErrorMessageResourceType = typeof(Idioma))]
		[StringLength(5, ErrorMessageResourceName = "MensajeTamanoMaximo", ErrorMessageResourceType = typeof(Idioma))]
		[Display(Name = "EtiquetaTipo", ResourceType = typeof(Idioma))]
		public string Tipo { get; set; } = null!;

		[Required(AllowEmptyStrings = false, ErrorMessageResourceName = "MensajeRequerido", ErrorMessageResourceType = typeof(Idioma))]
		[StringLength(5, ErrorMessageResourceName = "MensajeTamanoMaximo", ErrorMessageResourceType = typeof(Idioma))]
		[Display(Name = "EtiquetaPrioridad", ResourceType = typeof(Idioma))]
		public string Prioridad { get; set; } = null!;

		[Required(AllowEmptyStrings = false, ErrorMessageResourceName = "MensajeRequerido", ErrorMessageResourceType = typeof(Idioma))]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		[Display(Name = "EtiquetaFecha", ResourceType = typeof(Idioma))]
		public DateTime? FechaCreacion { get; set; }

		[Required(AllowEmptyStrings = false, ErrorMessageResourceName = "MensajeRequerido", ErrorMessageResourceType = typeof(Idioma))]
		[StringLength(5, ErrorMessageResourceName = "MensajeTamanoMaximo", ErrorMessageResourceType = typeof(Idioma))]
		[Display(Name = "EtiquetaEstado", ResourceType = typeof(Idioma))]
		public string Estado { get; set; } = null!;

		#endregion
	}
}