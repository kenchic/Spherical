using Spherical.Core.Creative;
using System.ComponentModel.DataAnnotations;

namespace Spherical.Client.DTO.Defender
{
    public class AutenticacionDTO
    {
        [Required(ErrorMessage = Constantes.CampoRequerido)]
        public string Empresa { get; set; }

        [Required(ErrorMessage = Constantes.CampoRequerido)]
		public string Usuario { get; set; }

		[Required(ErrorMessage = Constantes.CampoRequerido)]
		public string Clave { get; set; }
	}
}
