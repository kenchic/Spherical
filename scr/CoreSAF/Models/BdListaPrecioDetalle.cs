using System;
using System.Collections.Generic;

namespace CoreSAF.Models
{
    public partial class BdListaPrecioDetalle
    {
        public int Id { get; set; }
        public byte IdListaPrecio { get; set; }
        public short IdElemento { get; set; }
        public int PrecioAlquiler { get; set; }
        public int PrecioVenta { get; set; }
        public int PrecioPerdida { get; set; }

        public virtual BdElemento IdElementoNavigation { get; set; } = null!;
        public virtual BdListaPrecio IdListaPrecioNavigation { get; set; } = null!;
    }
}
