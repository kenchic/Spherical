using System;
using System.Collections.Generic;

namespace CoreSAF.Models
{
    public partial class BdReposicionServicioDetalle
    {
        public int Id { get; set; }
        public int IdReposicion { get; set; }
        public short IdElemento { get; set; }
        public int Cantidad { get; set; }
    }
}
