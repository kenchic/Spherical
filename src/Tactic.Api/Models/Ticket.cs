using System;
using System.Collections.Generic;

namespace Tactic.Api.Models
{
    public partial class Ticket
    {
        public int Id { get; set; }
        /// <summary>
        /// CATALOGO=&gt;EMPRESA
        /// </summary>
        public string Empresa { get; set; } = null!;
        public string Titulo { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public string Tipo { get; set; } = null!;
        public string Prioridad { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// CATALOGO=&gt;ESTADOS_TICKET
        /// </summary>
        public string Estado { get; set; } = null!;
    }
}
