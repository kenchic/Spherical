using System;
using System.Collections.Generic;

namespace Spherical.Infrastructure.EF.Models;

public partial class Elemento
{
    public short Id { get; set; }

    public string IdGrupoElemento { get; set; } = null!;

    public string IdUnidadMedida { get; set; } = null!;

    public string Empresa { get; set; } = null!;

    public string Referencia { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public double Mt2 { get; set; }

    public double Peso { get; set; }

    public bool Rotacion { get; set; }

    public bool Activo { get; set; }

    public virtual ICollection<ListaPrecioDetalle> ListaPrecioDetalles { get; set; } = new List<ListaPrecioDetalle>();
}
