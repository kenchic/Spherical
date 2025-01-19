using System;
using System.Collections.Generic;

namespace Spherical.Api.Models
{
    public partial class PermisoOpcion
    {
        public string IdPermiso { get; set; } = null!;
        public string IdOpcion { get; set; } = null!;
        public bool Consultar { get; set; }
        public bool Crear { get; set; }
        public bool Editar { get; set; }
        public bool Eliminar { get; set; }
        public bool Anular { get; set; }
        public bool Activar { get; set; }

        public virtual Opcion IdOpcionNavigation { get; set; } = null!;
        public virtual Permiso IdPermisoNavigation { get; set; } = null!;
    }
}
