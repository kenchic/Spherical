using System;
using System.Collections.Generic;

namespace Control.Api.Models
{
    public partial class VProyecto
    {
        public int Id { get; set; }
        public int IdCliente { get; set; }
        public string Nombre1 { get; set; } = null!;
    }
}
