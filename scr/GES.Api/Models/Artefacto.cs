using System;
using System.Collections.Generic;

namespace GES.Api.Models
{
    public partial class Artefacto
    {
        public Artefacto()
        {
            ArtefactoHistorials = new HashSet<ArtefactoHistorial>();
        }

        public int Id { get; set; }
        public byte IdSistema { get; set; }
        public string Tipo { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public string Estado { get; set; } = null!;

        public virtual ICollection<ArtefactoHistorial> ArtefactoHistorials { get; set; }
    }
}
