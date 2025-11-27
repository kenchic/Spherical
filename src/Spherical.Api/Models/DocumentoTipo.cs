using System;
using System.Collections.Generic;

namespace Spherical.Api.Models;

public partial class DocumentoTipo
{
    public string Id { get; set; } = null!;

    public string Empresa { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public long Consecutivo { get; set; }

    public string Operacion { get; set; } = null!;

    public short CantidadFilas { get; set; }

    public bool EsSistema { get; set; }

    public bool Activo { get; set; }
}
