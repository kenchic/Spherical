using System;
using System.Collections.Generic;

namespace CoreSAF.Models
{
    public partial class BdCorteOrdenServicioDetalle
    {
        public int Id { get; set; }
        public int IdCorteOrdenServicio { get; set; }
        public short IdElemento { get; set; }
        public int Cantidad { get; set; }
    }
}
