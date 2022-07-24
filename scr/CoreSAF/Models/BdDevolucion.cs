using System;
using System.Collections.Generic;

namespace CoreSAF.Models
{
    public partial class BdDevolucion
    {
        public BdDevolucion()
        {
            BdDevolucionDetalles = new HashSet<BdDevolucionDetalle>();
            BdDevolucionServicios = new HashSet<BdDevolucionServicio>();
            BdMantenimientos = new HashSet<BdMantenimiento>();
            BdReposicions = new HashSet<BdReposicion>();
        }

        public int Id { get; set; }
        public int IdBodegaOrigen { get; set; }
        public int IdBodegaDestino { get; set; }
        public int IdProyecto { get; set; }
        public string IdDocumentoTipo { get; set; } = null!;
        public string? IdConductor { get; set; }
        public int Numero { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime FechaSistema { get; set; }
        public decimal? ValorTransporte { get; set; }
        public decimal? PesoEquipo { get; set; }
        public decimal? ValorEquipo { get; set; }
        public bool? EntregaCliente { get; set; }
        public bool? EntregaParcial { get; set; }
        public string? Estado { get; set; }

        public virtual BdBodega IdBodegaDestinoNavigation { get; set; } = null!;
        public virtual BdBodega IdBodegaOrigenNavigation { get; set; } = null!;
        public virtual BdProyecto IdProyectoNavigation { get; set; } = null!;
        public virtual ICollection<BdDevolucionDetalle> BdDevolucionDetalles { get; set; }
        public virtual ICollection<BdDevolucionServicio> BdDevolucionServicios { get; set; }
        public virtual ICollection<BdMantenimiento> BdMantenimientos { get; set; }
        public virtual ICollection<BdReposicion> BdReposicions { get; set; }
    }
}
