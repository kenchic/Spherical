using System;
using System.Collections.Generic;

namespace CoreSAF.Models
{
    public partial class BdBodega
    {
        public BdBodega()
        {
            BdCorteIdBodegaDestinoNavigations = new HashSet<BdCorte>();
            BdCorteIdBodegaOrigenNavigations = new HashSet<BdCorte>();
            BdDevolucionIdBodegaDestinoNavigations = new HashSet<BdDevolucion>();
            BdDevolucionIdBodegaOrigenNavigations = new HashSet<BdDevolucion>();
            BdDevolucionServicioIdBodegaDestinoNavigations = new HashSet<BdDevolucionServicio>();
            BdDevolucionServicioIdBodegaOrigenNavigations = new HashSet<BdDevolucionServicio>();
            BdDocumentoIdBodegaDestinoNavigations = new HashSet<BdDocumento>();
            BdDocumentoIdBodegaOrigenNavigations = new HashSet<BdDocumento>();
            BdMantenimientoIdBodegaDestinoNavigations = new HashSet<BdMantenimiento>();
            BdMantenimientoIdBodegaOrigenNavigations = new HashSet<BdMantenimiento>();
            BdOrdenServicioIdBodegaDestinoNavigations = new HashSet<BdOrdenServicio>();
            BdOrdenServicioIdBodegaOrigenNavigations = new HashSet<BdOrdenServicio>();
            BdRemisionIdBodegaDestinoNavigations = new HashSet<BdRemision>();
            BdRemisionIdBodegaOrigenNavigations = new HashSet<BdRemision>();
            BdReposicionIdBodegaDestinoNavigations = new HashSet<BdReposicion>();
            BdReposicionIdBodegaOrigenNavigations = new HashSet<BdReposicion>();
            BdReposicionServicioIdBodegaDestinoNavigations = new HashSet<BdReposicionServicio>();
            BdReposicionServicioIdBodegaOrigenNavigations = new HashSet<BdReposicionServicio>();
            BdVentumIdBodegaDestinoNavigations = new HashSet<BdVentum>();
            BdVentumIdBodegaOrigenNavigations = new HashSet<BdVentum>();
        }

        public int Id { get; set; }
        public int? IdProyecto { get; set; }
        public short? IdProveedor { get; set; }
        public string Codigo { get; set; } = null!;
        public string? Nombre { get; set; }
        public bool EsSistema { get; set; }
        public bool Activo { get; set; }

        public virtual BdProveedor? IdProveedorNavigation { get; set; }
        public virtual ICollection<BdCorte> BdCorteIdBodegaDestinoNavigations { get; set; }
        public virtual ICollection<BdCorte> BdCorteIdBodegaOrigenNavigations { get; set; }
        public virtual ICollection<BdDevolucion> BdDevolucionIdBodegaDestinoNavigations { get; set; }
        public virtual ICollection<BdDevolucion> BdDevolucionIdBodegaOrigenNavigations { get; set; }
        public virtual ICollection<BdDevolucionServicio> BdDevolucionServicioIdBodegaDestinoNavigations { get; set; }
        public virtual ICollection<BdDevolucionServicio> BdDevolucionServicioIdBodegaOrigenNavigations { get; set; }
        public virtual ICollection<BdDocumento> BdDocumentoIdBodegaDestinoNavigations { get; set; }
        public virtual ICollection<BdDocumento> BdDocumentoIdBodegaOrigenNavigations { get; set; }
        public virtual ICollection<BdMantenimiento> BdMantenimientoIdBodegaDestinoNavigations { get; set; }
        public virtual ICollection<BdMantenimiento> BdMantenimientoIdBodegaOrigenNavigations { get; set; }
        public virtual ICollection<BdOrdenServicio> BdOrdenServicioIdBodegaDestinoNavigations { get; set; }
        public virtual ICollection<BdOrdenServicio> BdOrdenServicioIdBodegaOrigenNavigations { get; set; }
        public virtual ICollection<BdRemision> BdRemisionIdBodegaDestinoNavigations { get; set; }
        public virtual ICollection<BdRemision> BdRemisionIdBodegaOrigenNavigations { get; set; }
        public virtual ICollection<BdReposicion> BdReposicionIdBodegaDestinoNavigations { get; set; }
        public virtual ICollection<BdReposicion> BdReposicionIdBodegaOrigenNavigations { get; set; }
        public virtual ICollection<BdReposicionServicio> BdReposicionServicioIdBodegaDestinoNavigations { get; set; }
        public virtual ICollection<BdReposicionServicio> BdReposicionServicioIdBodegaOrigenNavigations { get; set; }
        public virtual ICollection<BdVentum> BdVentumIdBodegaDestinoNavigations { get; set; }
        public virtual ICollection<BdVentum> BdVentumIdBodegaOrigenNavigations { get; set; }
    }
}
