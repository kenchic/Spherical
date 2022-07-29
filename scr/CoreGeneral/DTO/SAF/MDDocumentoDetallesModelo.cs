using System;
using System.Collections.Generic;
using System.Text;

namespace CoreGeneral.Modelos.SAF
{
    public class MDDocumentoDetallesModelo
    {
        #region Propiedades
        public VDocumentoModelo Documento { get; set; }
        public List<VDocumentoDetalleModelo> Detalle { get; set; }
        #endregion
    }
}
