using System;
using System.Collections.Generic;

namespace CoreSAF.Models
{
    public partial class VElemento
    {
        public short Id { get; set; }
        public byte IdGrupoElemento { get; set; }
        public byte IdUnidadMedida { get; set; }
        public string Referencia { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public double Mt2 { get; set; }
        public double Peso { get; set; }
        public bool Rotacion { get; set; }
        public bool Activo { get; set; }
        public string GrupoElementoNombre { get; set; } = null!;
        public string UnidadMedidaNombre { get; set; } = null!;
    }
}
