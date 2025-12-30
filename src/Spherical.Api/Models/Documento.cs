using System;
using System.Collections.Generic;

namespace Spherical.Api.Models;

public partial class Documento
{
    public int Id { get; set; }

    public string IdDocumentoTipo { get; set; } = null!;

    public int IdBodegaOrigen { get; set; }

    public int IdBodegaDestino { get; set; }

    public string Empresa { get; set; } = null!;

    public int Numero { get; set; }

    public DateTime Fecha { get; set; }

    public string? Descripcion { get; set; }

    public string Estado { get; set; } = null!;
}
