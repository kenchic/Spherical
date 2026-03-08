using System;
using System.Collections.Generic;

namespace Spherical.Infrastructure.EF.Models;

public partial class DocumentoDetalle
{
    public int Id { get; set; }

    public short IdElemento { get; set; }

    public int IdDocumento { get; set; }

    public int Cantidad { get; set; }
}
