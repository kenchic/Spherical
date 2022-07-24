using System;
using System.Collections.Generic;

namespace CoreSAF.Models
{
    public partial class BdCorteOrdenServicio
    {
        public int Id { get; set; }
        public int IdBodegaOrigen { get; set; }
        public int IdBodegaDestino { get; set; }
        public int BdCorte { get; set; }
        public byte IdDocumentoTipo { get; set; }
        public int Numero { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime FechaSistema { get; set; }
        public string Estado { get; set; } = null!;
    }
}
