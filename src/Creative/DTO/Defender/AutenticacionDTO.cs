using System;
using System.ComponentModel.DataAnnotations;

namespace Creative.DTO.Defender
{
    public class AutenticacionDTO
    {
        [Required(ErrorMessage = "Empresa es obligatorio")]
        public string Empresa { get; set; }

        [Required(ErrorMessage = "Usuario es obligatorio")]
		public string Usuario { get; set; }

		[Required(ErrorMessage = "Clave es obligatorio")]
		public string Clave { get; set; }

		public string Token { get; set; }

		public string Terminal { get; set; }

		public DateTime FechaInicio { get; set; }

		public DateTime FechaFin { get; set; }
	}
}
