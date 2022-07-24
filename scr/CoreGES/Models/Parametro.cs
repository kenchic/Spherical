using System;
using System.Collections.Generic;

namespace CoreGES.Models
{
    public partial class Parametro
    {
        public string Codigo { get; set; } = null!;
        public string IdSistema { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public string Valor { get; set; } = null!;
        public bool Activo { get; set; }

        public virtual Sistema IdSistemaNavigation { get; set; } = null!;
    }
}
