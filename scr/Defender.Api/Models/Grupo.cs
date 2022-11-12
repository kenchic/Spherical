using System;
using System.Collections.Generic;

namespace Defender.Api.Models
{
    public partial class Grupo
    {
        public Grupo()
        {
            IdRols = new HashSet<Rol>();
            IdUsuarios = new HashSet<Usuario>();
        }

        public string Id { get; set; } = null!;
        public string Empresa { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public bool Activar { get; set; }

        public virtual ICollection<Rol> IdRols { get; set; }
        public virtual ICollection<Usuario> IdUsuarios { get; set; }
    }
}
