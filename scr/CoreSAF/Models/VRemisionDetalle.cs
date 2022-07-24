using System;
using System.Collections.Generic;

namespace CoreSAF.Models
{
    public partial class VRemisionDetalle
    {
        public int Id { get; set; }
        public short IdElemento { get; set; }
        public int IdRemision { get; set; }
        public int Cantidad { get; set; }
        public string ElementoNombre { get; set; } = null!;
    }
}
