using System;
using System.Collections.Generic;

namespace CoreSAF.Models
{
    public partial class BdListaPrecio
    {
        public BdListaPrecio()
        {
            BdContratos = new HashSet<BdContrato>();
            BdListaPrecioDetalles = new HashSet<BdListaPrecioDetalle>();
        }

        public byte Id { get; set; }
        public string Nombre { get; set; } = null!;
        public bool Activo { get; set; }

        public virtual ICollection<BdContrato> BdContratos { get; set; }
        public virtual ICollection<BdListaPrecioDetalle> BdListaPrecioDetalles { get; set; }
    }
}
