using System;
using System.Collections.Generic;

namespace CoreSAF.Models
{
    public partial class BdDevolucionDetalle
    {
        public int Id { get; set; }
        public int IdDevolucion { get; set; }
        public short IdElemento { get; set; }
        public int Cantidad { get; set; }

        public virtual BdDevolucion IdDevolucionNavigation { get; set; } = null!;
        public virtual BdElemento IdElementoNavigation { get; set; } = null!;
    }
}
