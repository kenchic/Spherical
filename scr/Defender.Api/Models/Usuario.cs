using System;
using System.Collections.Generic;

namespace Defender.Api.Models
{
    public partial class Usuario
    {
        public string Id { get; set; } = null!;
        public string Identificacion { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string Usuario1 { get; set; } = null!;
        public string Clave { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public bool Admin { get; set; }
        public bool Activo { get; set; }
    }
}
