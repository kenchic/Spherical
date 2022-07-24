using System;
using System.Collections.Generic;

namespace CoreGES.Models
{
    public partial class Catalogo
    {
        public Catalogo()
        {
            CatalogoDetalles = new HashSet<CatalogoDetalle>();
        }

        public string Id { get; set; } = null!;
        public string IdSistema { get; set; } = null!;
        public string Descripcion { get; set; } = null!;

        public virtual Sistema IdSistemaNavigation { get; set; } = null!;
        public virtual ICollection<CatalogoDetalle> CatalogoDetalles { get; set; }
    }
}
