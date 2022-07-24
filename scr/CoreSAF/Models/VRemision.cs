using System;
using System.Collections.Generic;

namespace CoreSAF.Models
{
    public partial class VRemision
    {
        public int Id { get; set; }
        public string IdDocumentoTipo { get; set; } = null!;
        public int IdBodegaOrigen { get; set; }
        public int IdBodegaDestino { get; set; }
        public int Numero { get; set; }
        public DateTime FechaEntrega { get; set; }
        public int IdProyecto { get; set; }
        public string? IdConductor { get; set; }
        public DateTime FechaPedido { get; set; }
        public bool? Transporte { get; set; }
        public decimal? ValorTransporte { get; set; }
        public bool? Despachado { get; set; }
        public bool? EquipoAdecuado { get; set; }
        public decimal? PesoEquipo { get; set; }
        public decimal? ValorEquipo { get; set; }
        public string? Estado { get; set; }
        public string DocumentoTipoNombre { get; set; } = null!;
        public string? BodegaOrigenNombre { get; set; }
        public string? BodegaDestinoNombre { get; set; }
    }
}
