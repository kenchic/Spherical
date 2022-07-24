using System;
using System.Collections.Generic;

namespace CoreSAF.Models
{
    public partial class BdMantenimientoDetalle
    {
        public int Id { get; set; }
        public int IdMantenimiento { get; set; }
        public short IdElemento { get; set; }
        public string TipoMantenimiento { get; set; } = null!;
        public int Cantidad { get; set; }

        public virtual BdMantenimiento IdMantenimientoNavigation { get; set; } = null!;
    }
}
