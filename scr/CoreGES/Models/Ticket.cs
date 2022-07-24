using System;
using System.Collections.Generic;

namespace CoreGES.Models
{
    public partial class Ticket
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public string Tipo { get; set; } = null!;
        public string Prioridad { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public string Estado { get; set; } = null!;
    }
}
