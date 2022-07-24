using System;
using System.Collections.Generic;

namespace CoreSAF.Models
{
    public partial class VDocumentoDetalle
    {
        public int Id { get; set; }
        public short IdElemento { get; set; }
        public int IdDocumento { get; set; }
        public string? Descripcion { get; set; }
        public int IdBodegaDestino { get; set; }
        public int IdBodegaOrigen { get; set; }
        public int Cantidad { get; set; }
        public string ElementoNombre { get; set; } = null!;
        public string? BodegaOrigenNombre { get; set; }
        public string? BodegaDestinoNombre { get; set; }
    }
}
