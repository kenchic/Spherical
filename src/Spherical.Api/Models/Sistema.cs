using System;
using System.Collections.Generic;

namespace Spherical.Api.Models;

public partial class Sistema
{
    public string Id { get; set; } = null!;

    public string Version { get; set; } = null!;

    public virtual ICollection<Catalogo> Catalogos { get; set; } = new List<Catalogo>();

    public virtual ICollection<Parametro> Parametros { get; set; } = new List<Parametro>();
}
