using System;
using System.Collections.Generic;

namespace CoreSEG.Models
{
    public partial class Permiso
    {
        public Permiso()
        {
            PermisoOpcions = new HashSet<PermisoOpcion>();
            IdMenus = new HashSet<Menu>();
            IdRols = new HashSet<Rol>();
        }

        public string Id { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public bool Activo { get; set; }

        public virtual ICollection<PermisoOpcion> PermisoOpcions { get; set; }

        public virtual ICollection<Menu> IdMenus { get; set; }
        public virtual ICollection<Rol> IdRols { get; set; }
    }
}
