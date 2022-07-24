using System;
using System.Collections.Generic;

namespace CoreSAF.Models
{
    public partial class BdOrdenServicioDetalle
    {
        public int Id { get; set; }
        public int IdOrdenServicio { get; set; }
        public short IdElemento { get; set; }
        public int Cantidad { get; set; }
    }
}
