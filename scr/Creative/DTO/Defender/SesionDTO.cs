using System;

namespace Creative.DTO.Defender
{ 
    public class SesionDTO
	{
        #region Propiedades

        public long IdSession { get; set; }

        public UsuarioDTO Usuario { get; set; }

        public string Token { get; set; }

        public DateTime FechaInicio { get; set; }

        public DateTime FechaFin { get; set; }

        #endregion
    }
}