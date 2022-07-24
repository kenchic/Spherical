using System;
using System.Collections.Generic;

namespace CoreSAF.Models
{
    public partial class BdVentaDetalle
    {
        public int Id { get; set; }
        public int IdVenta { get; set; }
        public short IdElemento { get; set; }
        public int Cantidad { get; set; }

        public virtual BdElemento IdElementoNavigation { get; set; } = null!;
        public virtual BdVentum IdVentaNavigation { get; set; } = null!;
    }
}
