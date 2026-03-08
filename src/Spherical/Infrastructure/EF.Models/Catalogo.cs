using System;
using System.Collections.Generic;

namespace Spherical.Infrastructure.EF.Models;

public partial class Catalogo
{
    public string Id { get; set; } = null!;

    public string IdSistema { get; set; } = null!;

    public string? Empresa { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<CatalogoDetalle> CatalogoDetalles { get; set; } = new List<CatalogoDetalle>();

    public virtual Sistema IdSistemaNavigation { get; set; } = null!;
}
