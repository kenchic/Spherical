using System;
using System.Collections.Generic;
using System.Text;

namespace Creative.DTO.Captain
{
    public class MDRemisionModelo
    {
        #region Propiedades
        public RemisionModelo Remision { get; set; }
        public List<RemisionDetalleModelo> Detalle { get; set; }
        #endregion
    }
}
