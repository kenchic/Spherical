using System;
using System.Collections.Generic;

namespace Spherical.Infrastructure.EF.Models;

public partial class ListaPrecio
{
    public byte Id { get; set; }

    public string Empresa { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public bool Activo { get; set; }

    public virtual ICollection<ListaPrecioDetalle> ListaPrecioDetalles { get; set; } = new List<ListaPrecioDetalle>();
}
