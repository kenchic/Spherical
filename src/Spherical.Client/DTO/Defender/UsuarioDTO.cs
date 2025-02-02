using System.ComponentModel.DataAnnotations;
using Spherical.Core.Creative;

namespace Spherical.Client.DTO.Defender
{
	public class UsuarioDTO
	{
		#region Propiedades

		[Key]
		[Required(AllowEmptyStrings = false, ErrorMessage = Constants.RequiredFieldMsg)]
		[Display(Name = "EtiquetaId")]
		public string Id { get; set; }

		[Display(Name = "EtiquetaRolNombre")]
		public string idRol { get; set; }

		[StringLength(50, ErrorMessage = Constants.MaxSizeMsg)]
		[Required(AllowEmptyStrings = false, ErrorMessage = Constants.RequiredFieldMsg)]
		[Display(Name = "EtiquetaIdentificacion")]
		public string Identificacion { get; set; }

		[StringLength(100, ErrorMessage = Constants.MaxSizeMsg)]
		[Required(AllowEmptyStrings = false, ErrorMessage = Constants.RequiredFieldMsg)]
		[Display(Name = "EtiquetaNombre")]
		public string Nombre { get; set; }

		[StringLength(100, ErrorMessage = Constants.MaxSizeMsg)]
		[Required(AllowEmptyStrings = false, ErrorMessage = Constants.RequiredFieldMsg)]
		[Display(Name = "EtiquetaApellido")]
		public string Apellido { get; set; }

		[StringLength(15, ErrorMessage = Constants.MaxSizeMsg)]
		[Required(AllowEmptyStrings = false, ErrorMessage = Constants.RequiredFieldMsg)]
		[Display(Name = "EtiquetaUsuario")]
		public string Usuario { get; set; }

		[StringLength(50, ErrorMessage = Constants.MaxSizeMsg)]
		[Required(AllowEmptyStrings = false, ErrorMessage = Constants.RequiredFieldMsg)]
		[Display(Name = "EtiquetaClave")]
		[DataType(DataType.Password)]
		public string Clave { get; set; }

		[StringLength(100, ErrorMessage = Constants.MaxSizeMsg)]
		[Required(AllowEmptyStrings = false, ErrorMessage = Constants.RequiredFieldMsg)]
		[Display(Name = "EtiquetaCorreo")]
		public string Correo { get; set; }

		[Required(AllowEmptyStrings = false, ErrorMessage = Constants.RequiredFieldMsg)]
		[Display(Name = "EtiquetaActivo")]
		public bool Activo { get; set; }

		[Required(AllowEmptyStrings = false, ErrorMessage = Constants.RequiredFieldMsg)]
		[Display(Name = "EtiquetaAdmin")]
		public bool Admin { get; set; }

		#endregion
	}
}