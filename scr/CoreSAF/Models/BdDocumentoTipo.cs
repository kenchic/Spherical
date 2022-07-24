using System;
using System.Collections.Generic;

namespace CoreSAF.Models
{
    public partial class BdDocumentoTipo
    {
        public BdDocumentoTipo()
        {
            BdDevolucionServicios = new HashSet<BdDevolucionServicio>();
            BdDocumentos = new HashSet<BdDocumento>();
            BdMantenimientos = new HashSet<BdMantenimiento>();
            BdOrdenServicios = new HashSet<BdOrdenServicio>();
            BdRemisions = new HashSet<BdRemision>();
            BdReposicionServicios = new HashSet<BdReposicionServicio>();
            BdReposicions = new HashSet<BdReposicion>();
        }

        public string Id { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public long Consecutivo { get; set; }
        public string Operacion { get; set; } = null!;
        public short CantidadFilas { get; set; }
        public bool EsSistema { get; set; }
        public bool Activo { get; set; }

        public virtual ICollection<BdDevolucionServicio> BdDevolucionServicios { get; set; }
        public virtual ICollection<BdDocumento> BdDocumentos { get; set; }
        public virtual ICollection<BdMantenimiento> BdMantenimientos { get; set; }
        public virtual ICollection<BdOrdenServicio> BdOrdenServicios { get; set; }
        public virtual ICollection<BdRemision> BdRemisions { get; set; }
        public virtual ICollection<BdReposicionServicio> BdReposicionServicios { get; set; }
        public virtual ICollection<BdReposicion> BdReposicions { get; set; }
    }
}
