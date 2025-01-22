using Spherical.Core.Creative;
using System.ComponentModel.DataAnnotations;

namespace Spherical.Client.DTO.Defender
{
    public class LoginDTO
    {
        [Required(ErrorMessage = Constants.RequiredFieldMsg)]
        public string Empresa { get; set; }

        [Required(ErrorMessage = Constants.RequiredFieldMsg)]
		public string Usuario { get; set; }

		[Required(ErrorMessage = Constants.RequiredFieldMsg)]
		public string Clave { get; set; }
	}
}
