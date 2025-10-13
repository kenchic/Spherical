using System;
using System.Collections.Generic;

namespace Spherical.Api.Models;

public partial class Cliente
{
    public int Id { get; set; }

    public string IdCiudad { get; set; } = null!;

    public string Empresa { get; set; } = null!;

    public string Identificacion { get; set; } = null!;

    public string Nombre1 { get; set; } = null!;

    public string? Nombre2 { get; set; }

    public string Apellido1 { get; set; } = null!;

    public string? Apellido2 { get; set; }

    public string Direccion { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string? Celular { get; set; }

    public string? Correo { get; set; }

    public bool Activo { get; set; }

    public virtual ICollection<Proyecto> Proyectos { get; set; } = new List<Proyecto>();
}
