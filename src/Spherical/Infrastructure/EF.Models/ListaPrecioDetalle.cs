using System;
using System.Collections.Generic;

namespace Spherical.Infrastructure.EF.Models;

public partial class ListaPrecioDetalle
{
    public short IdElemento { get; set; }

    public byte IdListaPrecio { get; set; }

    public int PrecioAlquiler { get; set; }

    public int PrecioVenta { get; set; }

    public int PrecioPerdida { get; set; }

    public virtual Elemento IdElementoNavigation { get; set; } = null!;

    public virtual ListaPrecio IdListaPrecioNavigation { get; set; } = null!;
}
