using System;
using System.Collections.Generic;

namespace CoreSAF.Models
{
    public partial class BdProyecto
    {
        public BdProyecto()
        {
            BdContratos = new HashSet<BdContrato>();
            BdCortes = new HashSet<BdCorte>();
            BdDevolucions = new HashSet<BdDevolucion>();
            BdRemisions = new HashSet<BdRemision>();
        }

        public int Id { get; set; }
        public int IdCliente { get; set; }
        /// <summary>
        /// Catalogo [CIUDADES]
        /// </summary>
        public string IdCiudad { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string Tipo { get; set; } = null!;
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public string? Observacion { get; set; }
        public DateTime Fecha { get; set; }
        public string? FormaContacto { get; set; }
        public string? SistemaMedida { get; set; }
        public string? IdentificacionResponsable { get; set; }
        public string? NombreResponsable { get; set; }
        public string? TelResponsable { get; set; }
        public bool? Activo { get; set; }
        public byte Estado { get; set; }

        public virtual BdCliente IdClienteNavigation { get; set; } = null!;
        public virtual ICollection<BdContrato> BdContratos { get; set; }
        public virtual ICollection<BdCorte> BdCortes { get; set; }
        public virtual ICollection<BdDevolucion> BdDevolucions { get; set; }
        public virtual ICollection<BdRemision> BdRemisions { get; set; }
    }
}
