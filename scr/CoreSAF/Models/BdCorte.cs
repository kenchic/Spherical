using System;
using System.Collections.Generic;

namespace CoreSAF.Models
{
    public partial class BdCorte
    {
        public int Id { get; set; }
        public int IdBodegaOrigen { get; set; }
        public int IdBodegaDestino { get; set; }
        public int IdProyecto { get; set; }
        public byte IdDocumentoTipo { get; set; }
        public int Numero { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime FechaSistema { get; set; }
        public string Estado { get; set; } = null!;

        public virtual BdBodega IdBodegaDestinoNavigation { get; set; } = null!;
        public virtual BdBodega IdBodegaOrigenNavigation { get; set; } = null!;
        public virtual BdProyecto IdProyectoNavigation { get; set; } = null!;
    }
}
