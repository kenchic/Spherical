using System;
using System.Collections.Generic;

namespace CoreSAF.Models
{
    public partial class VDocumento
    {
        public int Id { get; set; }
        public string IdDocumentoTipo { get; set; } = null!;
        public int IdBodegaOrigen { get; set; }
        public int IdBodegaDestino { get; set; }
        public int Numero { get; set; }
        public DateTime Fecha { get; set; }
        public string? Descripcion { get; set; }
        public string Estado { get; set; } = null!;
        public string DocumentoTipoNombre { get; set; } = null!;
        public string? BodegaOrigenNombre { get; set; }
        public string? BodegaDestinoNombre { get; set; }
    }
}
