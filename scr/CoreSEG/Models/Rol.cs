using System;
using System.Collections.Generic;

namespace CoreSEG.Models
{
    public partial class Rol
    {
        public Rol()
        {
            IdGrupos = new HashSet<Grupo>();
            IdPermisos = new HashSet<Permiso>();
            IdUsuarios = new HashSet<Usuario>();
        }

        public string Id { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public bool Activo { get; set; }

        public virtual ICollection<Grupo> IdGrupos { get; set; }
        public virtual ICollection<Permiso> IdPermisos { get; set; }
        public virtual ICollection<Usuario> IdUsuarios { get; set; }
    }
}
