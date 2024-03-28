using System;
using System.Collections.Generic;

namespace Tactic.Api.Models
{
    public partial class CatalogoDetalle
    {
        public string Id { get; set; } = null!;
        public string IdCatalogo { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string? ValorCadena { get; set; }
        public int? ValorNumero { get; set; }
        public decimal? ValorDecimal { get; set; }
        public bool? Activo { get; set; }

        public virtual Catalogo IdCatalogoNavigation { get; set; } = null!;
    }
}
