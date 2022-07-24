using System;
using System.Collections.Generic;

namespace CoreSAF.Models
{
    public partial class BdDocumentoDetalle
    {
        public int Id { get; set; }
        public short IdElemento { get; set; }
        public int IdDocumento { get; set; }
        public int Cantidad { get; set; }

        public virtual BdDocumento IdDocumentoNavigation { get; set; } = null!;
        public virtual BdElemento IdElementoNavigation { get; set; } = null!;
    }
}
