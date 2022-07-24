using System;
using System.Collections.Generic;

namespace CoreSAF.Models
{
    public partial class BdContrato
    {
        public short Id { get; set; }
        public int IdProyecto { get; set; }
        public byte IdListaPrecio { get; set; }
        public short? IdAgente { get; set; }
        public bool InformacionBd { get; set; }
        public bool ContratoAlquiler { get; set; }
        public bool CartaPagare { get; set; }
        public bool Pagare { get; set; }
        public bool LetraCambio { get; set; }
        public bool GarantiasCondiciones { get; set; }
        public bool Deposito { get; set; }
        public bool Anticipo { get; set; }
        public bool PersonaJuridica { get; set; }
        public bool PersonaNatural { get; set; }
        public bool FotoCopiaCedula { get; set; }
        public bool FotoCopiaNit { get; set; }
        public bool CamaraComercio { get; set; }
        public byte DescuentoAlquiler { get; set; }
        public byte DescuentoVenta { get; set; }
        public byte DescuentoReposicion { get; set; }
        public byte DescuentoMantenimiento { get; set; }
        public byte DescuentoTransporte { get; set; }
        public byte? PorcentajeAgente { get; set; }

        public virtual Agente? IdAgenteNavigation { get; set; }
        public virtual BdListaPrecio IdListaPrecioNavigation { get; set; } = null!;
        public virtual BdProyecto IdProyectoNavigation { get; set; } = null!;
    }
}
