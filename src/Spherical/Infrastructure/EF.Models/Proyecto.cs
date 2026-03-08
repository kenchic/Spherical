using System;
using System.Collections.Generic;

namespace Spherical.Infrastructure.EF.Models;

public partial class Proyecto
{
    public int Id { get; set; }

    public int IdCliente { get; set; }

    public string IdCiudad { get; set; } = null!;

    public string Empresa { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string Tipo { get; set; } = null!;

    public string? Direccion { get; set; }

    public string? Telefono { get; set; }

    public string? Observacion { get; set; }

    public DateOnly Fecha { get; set; }

    public string? FormaContacto { get; set; }

    public string? IdSistemaMedida { get; set; }

    public string? IdentificacionResponsable { get; set; }

    public string? NombreResponsable { get; set; }

    public string? TelResponsable { get; set; }

    public string IdEstado { get; set; } = null!;

    public bool Activo { get; set; }

    public virtual Cliente IdClienteNavigation { get; set; } = null!;
}
