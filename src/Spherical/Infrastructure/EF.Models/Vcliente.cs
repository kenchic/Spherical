using System;
using System.Collections.Generic;

namespace Spherical.Infrastructure.EF.Models;

public partial class Vcliente
{
    public int Id { get; set; }

    public string IdCiudad { get; set; } = null!;

    public string NombreCiudad { get; set; } = null!;

    public string Empresa { get; set; } = null!;

    public string Identificacion { get; set; } = null!;

    public string NombreCliente { get; set; } = null!;

    public string Nombre1 { get; set; } = null!;

    public string? Nombre2 { get; set; }

    public string Apellido1 { get; set; } = null!;

    public string? Apellido2 { get; set; }

    public string Direccion { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string? Celular { get; set; }

    public string? Correo { get; set; }

    public bool Activo { get; set; }
}
