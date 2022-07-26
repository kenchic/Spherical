using System.ComponentModel.DataAnnotations;
using Creative.Recursos;

namespace Creative.DTO.Defender
{
	public class UsuarioDTO
	{
		#region Propiedades

		[Key]
		[Required(AllowEmptyStrings = false, ErrorMessageResourceName = "MensajeRequerido", ErrorMessageResourceType = typeof(Idioma))]
		[Display(Name = "EtiquetaId", ResourceType = typeof(Idioma))]
		public string Id { get; set; }

		[Display(Name = "EtiquetaRolNombre", ResourceType = typeof(Idioma))]
		public string idRol { get; set; }

		[StringLength(50, ErrorMessageResourceName = "MensajeTamanoMaximo", ErrorMessageResourceType = typeof(Idioma))]
		[Required(AllowEmptyStrings = false, ErrorMessageResourceName = "MensajeRequerido", ErrorMessageResourceType = typeof(Idioma))]
		[Display(Name = "EtiquetaIdentificacion", ResourceType = typeof(Idioma))]
		public string Identificacion { get; set; }

		[StringLength(100, ErrorMessageResourceName = "MensajeTamanoMaximo", ErrorMessageResourceType = typeof(Idioma))]
		[Required(AllowEmptyStrings = false, ErrorMessageResourceName = "MensajeRequerido", ErrorMessageResourceType = typeof(Idioma))]
		[Display(Name = "EtiquetaNombre", ResourceType = typeof(Idioma))]
		public string Nombre { get; set; }

		[StringLength(100, ErrorMessageResourceName = "MensajeTamanoMaximo", ErrorMessageResourceType = typeof(Idioma))]
		[Required(AllowEmptyStrings = false, ErrorMessageResourceName = "MensajeRequerido", ErrorMessageResourceType = typeof(Idioma))]
		[Display(Name = "EtiquetaApellido", ResourceType = typeof(Idioma))]
		public string Apellido { get; set; }

		[StringLength(15, ErrorMessageResourceName = "MensajeTamanoMaximo", ErrorMessageResourceType = typeof(Idioma))]
		[Required(AllowEmptyStrings = false, ErrorMessageResourceName = "MensajeRequerido", ErrorMessageResourceType = typeof(Idioma))]
		[Display(Name = "EtiquetaUsuario", ResourceType = typeof(Idioma))]
		public string Usuario { get; set; }

		[StringLength(50, ErrorMessageResourceName = "MensajeTamanoMaximo", ErrorMessageResourceType = typeof(Idioma))]
		[Required(AllowEmptyStrings = false, ErrorMessageResourceName = "MensajeRequerido", ErrorMessageResourceType = typeof(Idioma))]
		[Display(Name = "EtiquetaClave", ResourceType = typeof(Idioma))]
		[DataType(DataType.Password)]
		public string Clave { get; set; }

		[StringLength(100, ErrorMessageResourceName = "MensajeTamanoMaximo", ErrorMessageResourceType = typeof(Idioma))]
		[Required(AllowEmptyStrings = false, ErrorMessageResourceName = "MensajeRequerido", ErrorMessageResourceType = typeof(Idioma))]
		[Display(Name = "EtiquetaCorreo", ResourceType = typeof(Idioma))]
		public string Correo { get; set; }

		[Required(AllowEmptyStrings = false, ErrorMessageResourceName = "MensajeRequerido", ErrorMessageResourceType = typeof(Idioma))]
		[Display(Name = "EtiquetaActivo", ResourceType = typeof(Idioma))]
		public bool Activo { get; set; }

		[Required(AllowEmptyStrings = false, ErrorMessageResourceName = "MensajeRequerido", ErrorMessageResourceType = typeof(Idioma))]
		[Display(Name = "EtiquetaAdmin", ResourceType = typeof(Idioma))]
		public bool Admin { get; set; }

		#endregion
	}
}