using System;
using System.Collections.Generic;

namespace CoreSAF.Models
{
    public partial class BdElemento
    {
        public BdElemento()
        {
            BdDevolucionDetalles = new HashSet<BdDevolucionDetalle>();
            BdDocumentoDetalles = new HashSet<BdDocumentoDetalle>();
            BdListaPrecioDetalles = new HashSet<BdListaPrecioDetalle>();
            BdRemisionDetalles = new HashSet<BdRemisionDetalle>();
            BdVentaDetalles = new HashSet<BdVentaDetalle>();
        }

        public short Id { get; set; }
        public byte IdGrupoElemento { get; set; }
        public byte IdUnidadMedida { get; set; }
        public string Referencia { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public double Mt2 { get; set; }
        public double Peso { get; set; }
        public bool Rotacion { get; set; }
        public bool Activo { get; set; }

        public virtual BdGrupoElemento IdGrupoElementoNavigation { get; set; } = null!;
        public virtual BdUnidadMedidum IdUnidadMedidaNavigation { get; set; } = null!;
        public virtual ICollection<BdDevolucionDetalle> BdDevolucionDetalles { get; set; }
        public virtual ICollection<BdDocumentoDetalle> BdDocumentoDetalles { get; set; }
        public virtual ICollection<BdListaPrecioDetalle> BdListaPrecioDetalles { get; set; }
        public virtual ICollection<BdRemisionDetalle> BdRemisionDetalles { get; set; }
        public virtual ICollection<BdVentaDetalle> BdVentaDetalles { get; set; }
    }
}
