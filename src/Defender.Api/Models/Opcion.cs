using System;
using System.Collections.Generic;

namespace Defender.Api.Models
{
    public partial class Opcion
    {
        public Opcion()
        {
            PermisoOpcions = new HashSet<PermisoOpcion>();
        }

        public string Id { get; set; } = null!;
        public string Empresa { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public bool Consultar { get; set; }
        public bool Crear { get; set; }
        public bool Editar { get; set; }
        public bool Eliminar { get; set; }
        public bool Anular { get; set; }
        public bool Activar { get; set; }

        public virtual ICollection<PermisoOpcion> PermisoOpcions { get; set; }
    }
}
