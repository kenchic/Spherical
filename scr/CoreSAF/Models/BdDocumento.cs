using System;
using System.Collections.Generic;

namespace CoreSAF.Models
{
    public partial class BdDocumento
    {
        public BdDocumento()
        {
            BdDocumentoDetalles = new HashSet<BdDocumentoDetalle>();
        }

        public int Id { get; set; }
        public string IdDocumentoTipo { get; set; } = null!;
        public int IdBodegaOrigen { get; set; }
        public int IdBodegaDestino { get; set; }
        public int Numero { get; set; }
        public DateTime Fecha { get; set; }
        public string? Descripcion { get; set; }
        public string Estado { get; set; } = null!;

        public virtual BdBodega IdBodegaDestinoNavigation { get; set; } = null!;
        public virtual BdBodega IdBodegaOrigenNavigation { get; set; } = null!;
        public virtual BdDocumentoTipo IdDocumentoTipoNavigation { get; set; } = null!;
        public virtual ICollection<BdDocumentoDetalle> BdDocumentoDetalles { get; set; }
    }
}
