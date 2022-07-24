using System;
using System.Collections.Generic;

namespace CoreSAF.Models
{
    public partial class VProyectoPlantilla
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Tipo { get; set; } = null!;
        public int Numero { get; set; }
        public string? Cantidad { get; set; }
        public short? IdElemento { get; set; }
        public string? Elemento { get; set; }
        public int IdProyecto { get; set; }
        public string? Documento { get; set; }
    }
}
