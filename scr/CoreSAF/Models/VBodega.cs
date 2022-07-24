using System;
using System.Collections.Generic;

namespace CoreSAF.Models
{
    public partial class VBodega
    {
        public int Id { get; set; }
        public int? IdProyecto { get; set; }
        public short? IdProveedor { get; set; }
        public string? Nombre { get; set; }
        public bool Activo { get; set; }
        public bool EsSistema { get; set; }
        public string? ProveedorNombre { get; set; }
        public string? ProyectoNombre { get; set; }
        public string Codigo { get; set; } = null!;
    }
}
