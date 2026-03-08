using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Creative.DTO.Control
{
    public class RemisionDTO
    {
        #region Propiedades
        [Key]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "MensajeRequerido")]
        [Display(Name = "EtiquetaId")]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "MensajeRequerido")]
        [Display(Name = "EtiquetaBodegaOrigenNombre")]
        public int idBodegaOrigen { get; set; }

        [Display(Name = "EtiquetaBodegaOrigenNombre")]
        public virtual string BodegaOrigenNombre { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "MensajeRequerido")]
        [Display(Name = "EtiquetaBodegaDestinoNombre")]
        public int idBodegaDestino { get; set; }

        [Display(Name = "EtiquetaBodegaDestinoNombre")]
        public virtual string BodegaDestinoNombre { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "MensajeRequerido")]
        [Display(Name = "EtiquetaProyectoNombre")]
        public int idProyecto { get; set; }

        [Display(Name = "EtiquetaClienteNombre")]
        public virtual string ClienteNombre { get; set; }

        [Display(Name = "EtiquetaProyectoNombre")]
        public virtual string ProyectoNombre { get; set; }

        [Display(Name = "EtiquetaDocumentoTipoNombre")]
        public string idDocumentoTipo { get; set; }

        [Display(Name = "EtiquetaDocumentoTipoNombre")]
        public virtual string DocumentoTipoNombre { get; set; }

        [Display(Name = "EtiquetaConductor")]
        public int idConductor { get; set; }

        [Display(Name = "EtiquetaConductor")]
        public virtual string ConductorNombre { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "MensajeRequerido")]
        [Display(Name = "EtiquetaNumero")]
        public int Numero { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "MensajeRequerido")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "EtiquetaFechaEntrega")]
        public DateTime? FechaEntrega { get; set; }

        [Display(Name = "EtiquetaFechaEntrega")]
        public string DFechaEntrega
        {
            get
            {
                string strFormato = FechaEntrega == null ? string.Empty : FechaEntrega.Value.ToString("dd/MM/yyyy");
                return strFormato;
            }
        }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "MensajeRequerido")]
        [Display(Name = "EtiquetaFecha")]
        public DateTime? Fecha { get; set; }

        [Display(Name = "EtiquetaFecha")]
        public string DFecha
        {
            get
            {
                string strFormato = Fecha == null ? string.Empty : Fecha.Value.ToString("dd/MM/yyyy");
                return strFormato;
            }
        }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "MensajeRequerido")]
        [Display(Name = "EtiquetaFecha")]
        public DateTime? FechaSistema { get; set; }

        [Display(Name = "EtiquetaFecha")]
        public string DFechasistema
        {
            get
            {
                string strFormato = FechaSistema == null ? string.Empty : FechaSistema.Value.ToString("dd/MM/yyyy");
                return strFormato;
            }
        }

        [Display(Name = "EtiquetaFechaPedido")]
        public DateTime FechaPedido { get; set; }

        [Display(Name = "EtiquetaTransporte")]
        public bool Transporte { get; set; }

        [Display(Name = "EtiquetaValorTransporte")]
        public int ValorTransporte { get; set; }

        [Display(Name = "EtiquetaDespacho")]
        public bool Despachado { get; set; }

        [Display(Name = "EtiquetaEquipoAdecuado")]
        public bool EquipoAdecuado { get; set; }

        [Display(Name = "EtiquetaPesoEquipo")]
        public decimal PesoEquipo { get; set; }

        [Display(Name = "EtiquetaValorEquipo")]
        public long ValorEquipo { get; set; }

        [Display(Name = "EtiquetaEstado")]
        public string Estado { get; set; }

        public List<RemisionDetalleDTO> Detalle { get; set; }
        #endregion
    }
}
