using System;
using System.Collections.Generic;

namespace Fordward.Api.Models
{
    public partial class Factura
    {
        public int Id { get; set; }
        public int IdCliente { get; set; }
        /// <summary>
        /// CATALOGO=&gt;EMPRESA
        /// </summary>
        public string Empresa { get; set; } = null!;
        /// <summary>
        /// CATALOGO=&gt;ESTADOS_FACTURA
        /// </summary>
        public string Estado { get; set; } = null!;
    }
}
