using System;
using System.Collections.Generic;

namespace Spherical.Api.Models;

public partial class VusuarioPermiso
{
    public string Usuario { get; set; } = null!;

    public string Opcion { get; set; } = null!;

    public bool Consultar { get; set; }

    public bool Crear { get; set; }

    public bool Editar { get; set; }

    public bool Eliminar { get; set; }

    public bool Anular { get; set; }

    public bool Activar { get; set; }
}
