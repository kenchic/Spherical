using System;
using System.Collections.Generic;

namespace CoreSAF.Models
{
    public partial class BdRemision
    {
        public BdRemision()
        {
            BdOrdenServicios = new HashSet<BdOrdenServicio>();
            BdRemisionDetalles = new HashSet<BdRemisionDetalle>();
            BdVenta = new HashSet<BdVentum>();
        }

        public int Id { get; set; }
        public int IdBodegaOrigen { get; set; }
        public int IdBodegaDestino { get; set; }
        public int IdProyecto { get; set; }
        public string IdDocumentoTipo { get; set; } = null!;
        /// <summary>
        /// CATALOGO [CONDUCTORES]
        /// </summary>
        public string? IdConductor { get; set; }
        public int Numero { get; set; }
        public DateTime FechaEntrega { get; set; }
        public DateTime FechaPedido { get; set; }
        public bool? Transporte { get; set; }
        public decimal? ValorTransporte { get; set; }
        public bool? Despachado { get; set; }
        public bool? EquipoAdecuado { get; set; }
        public decimal? PesoEquipo { get; set; }
        public decimal? ValorEquipo { get; set; }
        public string? Estado { get; set; }

        public virtual BdBodega IdBodegaDestinoNavigation { get; set; } = null!;
        public virtual BdBodega IdBodegaOrigenNavigation { get; set; } = null!;
        public virtual BdDocumentoTipo IdDocumentoTipoNavigation { get; set; } = null!;
        public virtual BdProyecto IdProyectoNavigation { get; set; } = null!;
        public virtual ICollection<BdOrdenServicio> BdOrdenServicios { get; set; }
        public virtual ICollection<BdRemisionDetalle> BdRemisionDetalles { get; set; }
        public virtual ICollection<BdVentum> BdVenta { get; set; }
    }
}
