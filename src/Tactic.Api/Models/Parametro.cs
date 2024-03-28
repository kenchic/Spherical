using System;
using System.Collections.Generic;

namespace Tactic.Api.Models
{
    public partial class Parametro
    {
        public string Codigo { get; set; } = null!;
        public string IdSistema { get; set; } = null!;
        /// <summary>
        /// CATALOGO=&gt;EMPRESA
        /// </summary>
        public string Empresa { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public string Valor { get; set; } = null!;
        public bool Activo { get; set; }

        public virtual Sistema IdSistemaNavigation { get; set; } = null!;
    }
}
