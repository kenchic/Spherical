using System;
using System.Collections.Generic;

namespace Tactic.Api.Models
{
    public partial class ListaPrecio
    {
        public byte Id { get; set; }
        /// <summary>
        /// CATALOGO=&gt;EMPRESA
        /// </summary>
        public string Empresa { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public bool Activo { get; set; }
    }
}
