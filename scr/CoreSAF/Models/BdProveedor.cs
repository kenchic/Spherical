using System;
using System.Collections.Generic;

namespace CoreSAF.Models
{
    public partial class BdProveedor
    {
        public BdProveedor()
        {
            BdBodegas = new HashSet<BdBodega>();
            BdDevolucionServicios = new HashSet<BdDevolucionServicio>();
            BdOrdenServicios = new HashSet<BdOrdenServicio>();
        }

        public short Id { get; set; }
        public string? Identificacion { get; set; }
        public string Nombre { get; set; } = null!;
        public string Iniciales { get; set; } = null!;
        public string? Telefono { get; set; }
        public string? Direccion { get; set; }
        public bool Activo { get; set; }

        public virtual ICollection<BdBodega> BdBodegas { get; set; }
        public virtual ICollection<BdDevolucionServicio> BdDevolucionServicios { get; set; }
        public virtual ICollection<BdOrdenServicio> BdOrdenServicios { get; set; }
    }
}
