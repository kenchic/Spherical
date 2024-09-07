using System;
using System.Collections.Generic;
using System.Text;

namespace Creative.Modelos.Lineup
{
    public class MDListaPrecioDetallesModelo
    {
        #region Propiedades
        public ListaPrecioModelo ListaPrecio { get; set; }
        public List<VListaPrecioDetalleModelo> Detalle { get; set; }
        #endregion
    }
}
