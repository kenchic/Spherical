using System;
using System.Collections.Generic;

namespace GES.Api.Models
{
    public partial class Sistema
    {
        public Sistema()
        {
            Catalogos = new HashSet<Catalogo>();
            Parametros = new HashSet<Parametro>();
        }

        public string Id { get; set; } = null!;
        public string Version { get; set; } = null!;

        public virtual ICollection<Catalogo> Catalogos { get; set; }
        public virtual ICollection<Parametro> Parametros { get; set; }
    }
}
