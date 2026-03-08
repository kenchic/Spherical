using System;
using System.Collections.Generic;

namespace Spherical.Infrastructure.EF.Models;

public partial class Permiso
{
    public string Id { get; set; } = null!;

    public string Empresa { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public bool Activo { get; set; }

    public virtual ICollection<PermisoOpcion> PermisoOpcions { get; set; } = new List<PermisoOpcion>();

    public virtual ICollection<Menu> IdMenus { get; set; } = new List<Menu>();

    public virtual ICollection<Rol> IdRols { get; set; } = new List<Rol>();
}
