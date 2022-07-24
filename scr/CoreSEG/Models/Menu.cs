using System;
using System.Collections.Generic;

namespace CoreSEG.Models
{
    public partial class Menu
    {
        public Menu()
        {
            InverseIdMenuNavigation = new HashSet<Menu>();
            IdPermisos = new HashSet<Permiso>();
        }

        public string Id { get; set; } = null!;
        public string? IdMenu { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Vista { get; set; }
        public short Orden { get; set; }
        public string? Imagen { get; set; }
        public bool Activo { get; set; }

        public virtual Menu? IdMenuNavigation { get; set; }
        public virtual ICollection<Menu> InverseIdMenuNavigation { get; set; }

        public virtual ICollection<Permiso> IdPermisos { get; set; }
    }
}
