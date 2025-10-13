using System;
using System.Collections.Generic;

namespace Spherical.Api.Models;

public partial class Factura
{
    public int Id { get; set; }

    public int IdCliente { get; set; }

    public string Empresa { get; set; } = null!;

    public string Estado { get; set; } = null!;
}
