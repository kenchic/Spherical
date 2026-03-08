using System;
using System.Collections.Generic;

namespace Spherical.Infrastructure.EF.Models;

public partial class Rol
{
    public string Id { get; set; } = null!;

    public string Empresa { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public bool Activo { get; set; }

    public virtual ICollection<Grupo> IdGrupos { get; set; } = new List<Grupo>();

    public virtual ICollection<Permiso> IdPermisos { get; set; } = new List<Permiso>();

    public virtual ICollection<Usuario> IdUsuarios { get; set; } = new List<Usuario>();
}
