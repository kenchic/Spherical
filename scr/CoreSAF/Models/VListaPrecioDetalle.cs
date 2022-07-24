using System;
using System.Collections.Generic;

namespace CoreSAF.Models
{
    public partial class VListaPrecioDetalle
    {
        public int Id { get; set; }
        public byte IdListaPrecio { get; set; }
        public short IdElemento { get; set; }
        public int PrecioAlquiler { get; set; }
        public int PrecioVenta { get; set; }
        public int PrecioPerdida { get; set; }
        public string ListaPrecioNombre { get; set; } = null!;
        public string ElementoNombre { get; set; } = null!;
    }
}
