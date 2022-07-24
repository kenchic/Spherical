using System;
using System.Collections.Generic;

namespace CoreSAF.Models
{
    public partial class BdRemisionDetalle
    {
        public int Id { get; set; }
        public int IdRemision { get; set; }
        public short IdElemento { get; set; }
        public int Cantidad { get; set; }

        public virtual BdElemento IdElementoNavigation { get; set; } = null!;
        public virtual BdRemision IdRemisionNavigation { get; set; } = null!;
    }
}
