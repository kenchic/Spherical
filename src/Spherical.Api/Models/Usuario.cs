using System;
using System.Collections.Generic;

namespace Spherical.Api.Models;

public partial class Usuario
{
    public string Id { get; set; } = null!;

    public string Empresa { get; set; } = null!;

    public string Identificacion { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Usuario1 { get; set; } = null!;

    public string Clave { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public bool Admin { get; set; }

    public bool Activo { get; set; }

    public virtual ICollection<Grupo> IdGrupos { get; set; } = new List<Grupo>();

    public virtual ICollection<Rol> IdRols { get; set; } = new List<Rol>();
}
