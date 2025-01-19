using System;
using System.Collections.Generic;

namespace Spherical.Api.Models
{
    public partial class Menu
    {
        public Menu()
        {
            InverseIdMenuNavigation = new HashSet<Menu>();
            IdPermisos = new HashSet<Permiso>();
        }

        public string Id { get; set; } = null!;
        public string Empresa { get; set; } = null!;
        public string? IdMenu { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Url { get; set; }
        public short Orden { get; set; }
        public string? Icono { get; set; }
        public bool Activo { get; set; }

        public virtual Menu? IdMenuNavigation { get; set; }
        public virtual ICollection<Menu> InverseIdMenuNavigation { get; set; }

        public virtual ICollection<Permiso> IdPermisos { get; set; }
    }
}
