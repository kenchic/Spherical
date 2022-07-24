using System;
using System.Collections.Generic;

namespace CoreSEG.Models
{
    public partial class Auditorium
    {
        public int Id { get; set; }
        public long IdSesion { get; set; }
        public string Tabla { get; set; } = null!;
        public DateTime Fecha { get; set; }
        public string Operacion { get; set; } = null!;
        public string? Observacion { get; set; }
        public string Detalle { get; set; } = null!;
    }
}
