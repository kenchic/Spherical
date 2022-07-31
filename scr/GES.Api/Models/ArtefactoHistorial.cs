using System;
using System.Collections.Generic;

namespace GES.Api.Models
{
    public partial class ArtefactoHistorial
    {
        public long Id { get; set; }
        public int IdArtefacto { get; set; }
        public int IdRequerimiento { get; set; }
        public string Objetivo { get; set; } = null!;
        public string? Version { get; set; }

        public virtual Artefacto IdArtefactoNavigation { get; set; } = null!;
    }
}
