using System;
using System.Collections.Generic;

namespace CoreSAF.Models
{
    public partial class Agente
    {
        public Agente()
        {
            BdContratos = new HashSet<BdContrato>();
        }

        public short Id { get; set; }
        public string Nombre { get; set; } = null!;
        public bool Activo { get; set; }

        public virtual ICollection<BdContrato> BdContratos { get; set; }
    }
}
