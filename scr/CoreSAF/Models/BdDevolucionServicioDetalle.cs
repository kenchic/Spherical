using System;
using System.Collections.Generic;

namespace CoreSAF.Models
{
    public partial class BdDevolucionServicioDetalle
    {
        public int Id { get; set; }
        public int IdDevolucionServicio { get; set; }
        public short IdElemento { get; set; }
        public int Cantidad { get; set; }
    }
}
