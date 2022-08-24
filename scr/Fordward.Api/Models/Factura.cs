using System;
using System.Collections.Generic;

namespace Fordward.Api.Models
{
    public partial class Factura
    {
        public int Id { get; set; }
        public int IdCliente { get; set; }
        public string Estado { get; set; } = null!;
    }
}
