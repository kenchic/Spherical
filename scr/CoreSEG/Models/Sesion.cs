using System;
using System.Collections.Generic;

namespace CoreSEG.Models
{
    public partial class Sesion
    {
        public int Id { get; set; }
        public string IdUsuario { get; set; } = null!;
        public string Token { get; set; } = null!;
        public string? Terminal { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public int? Tiempo { get; set; }
        public int? IdSesionBd { get; set; }
    }
}
