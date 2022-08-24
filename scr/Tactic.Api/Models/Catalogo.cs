using System;
using System.Collections.Generic;

namespace Tactic.Api.Models
{
    public partial class Catalogo
    {
        public Catalogo()
        {
            CatalogoDetalles = new HashSet<CatalogoDetalle>();
        }

        public string Id { get; set; } = null!;
        public string IdSistema { get; set; } = null!;
        /// <summary>
        /// CATALOGO=&gt;EMPRESA
        /// </summary>
        public string? Empresa { get; set; }
        public string Descripcion { get; set; } = null!;

        public virtual Sistema IdSistemaNavigation { get; set; } = null!;
        public virtual ICollection<CatalogoDetalle> CatalogoDetalles { get; set; }
    }
}
