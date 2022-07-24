using System;
using System.Collections.Generic;

namespace CoreSAF.Models
{
    public partial class BdCorteDetalle
    {
        public int Id { get; set; }
        public int IdCorte { get; set; }
        public short IdElemento { get; set; }
        public int Cantidad { get; set; }
    }
}
