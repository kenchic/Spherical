using System;

namespace CoreGeneral.Modelos.SEG
{
	public class SesionModelo
	{
        #region Propiedades

        public long IdSession { get; set; }

        public UsuarioModelo Usuario { get; set; }

        public string Token { get; set; }

        public DateTime FechaInicio { get; set; }

        public DateTime FechaFin { get; set; }

        #endregion
    }
}