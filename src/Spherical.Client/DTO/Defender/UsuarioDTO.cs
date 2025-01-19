using System.ComponentModel.DataAnnotations;
using Spherical.Core.Creative;

namespace Spherical.Client.DTO.Defender
{
	public class UsuarioDTO
	{
		#region Propiedades

		[Key]
		[Required(AllowEmptyStrings = false, ErrorMessage = Constantes.CampoRequerido)]
		[Display(Name = "EtiquetaId")]
		public string Id { get; set; }

		[Display(Name = "EtiquetaRolNombre")]
		public string idRol { get; set; }

		[StringLength(50, ErrorMessage = Constantes.TamanoMaximo)]
		[Required(AllowEmptyStrings = false, ErrorMessage = Constantes.CampoRequerido)]
		[Display(Name = "EtiquetaIdentificacion")]
		public string Identificacion { get; set; }

		[StringLength(100, ErrorMessage = Constantes.TamanoMaximo)]
		[Required(AllowEmptyStrings = false, ErrorMessage = Constantes.CampoRequerido)]
		[Display(Name = "EtiquetaNombre")]
		public string Nombre { get; set; }

		[StringLength(100, ErrorMessage = Constantes.TamanoMaximo)]
		[Required(AllowEmptyStrings = false, ErrorMessage = Constantes.CampoRequerido)]
		[Display(Name = "EtiquetaApellido")]
		public string Apellido { get; set; }

		[StringLength(15, ErrorMessage = Constantes.TamanoMaximo)]
		[Required(AllowEmptyStrings = false, ErrorMessage = Constantes.CampoRequerido)]
		[Display(Name = "EtiquetaUsuario")]
		public string Usuario { get; set; }

		[StringLength(50, ErrorMessage = Constantes.TamanoMaximo)]
		[Required(AllowEmptyStrings = false, ErrorMessage = Constantes.CampoRequerido)]
		[Display(Name = "EtiquetaClave")]
		[DataType(DataType.Password)]
		public string Clave { get; set; }

		[StringLength(100, ErrorMessage = Constantes.TamanoMaximo)]
		[Required(AllowEmptyStrings = false, ErrorMessage = Constantes.CampoRequerido)]
		[Display(Name = "EtiquetaCorreo")]
		public string Correo { get; set; }

		[Required(AllowEmptyStrings = false, ErrorMessage = Constantes.CampoRequerido)]
		[Display(Name = "EtiquetaActivo")]
		public bool Activo { get; set; }

		[Required(AllowEmptyStrings = false, ErrorMessage = Constantes.CampoRequerido)]
		[Display(Name = "EtiquetaAdmin")]
		public bool Admin { get; set; }

		#endregion
	}
}