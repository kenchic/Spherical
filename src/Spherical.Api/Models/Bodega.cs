using System;
using System.Collections.Generic;

namespace Spherical.Api.Models;

public partial class Bodega
{
    public int Id { get; set; }

    public int? IdProyecto { get; set; }

    public short? IdProveedor { get; set; }

    public string Empresa { get; set; } = null!;

    public string Codigo { get; set; } = null!;

    public string? Nombre { get; set; }

    public bool EsSistema { get; set; }

    public bool Activo { get; set; }
}
