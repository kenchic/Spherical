using System;
using System.Collections.Generic;

namespace Spherical.Infrastructure.EF.Models;

public partial class Grupo
{
    public string Id { get; set; } = null!;

    public string Empresa { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public bool Activar { get; set; }

    public virtual ICollection<Rol> IdRols { get; set; } = new List<Rol>();

    public virtual ICollection<Usuario> IdUsuarios { get; set; } = new List<Usuario>();
}
