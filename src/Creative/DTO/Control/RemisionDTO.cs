using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Creative.Recursos;

namespace Creative.DTO.Control
{
    public class RemisionDTO
    {
        #region Propiedades
        [Key]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "MensajeRequerido", ErrorMessageResourceType = typeof(Idioma))]
        [Display(Name = "EtiquetaId", ResourceType = typeof(Idioma))]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "MensajeRequerido", ErrorMessageResourceType = typeof(Idioma))]
        [Display(Name = "EtiquetaBodegaOrigenNombre", ResourceType = typeof(Idioma))]
        public int idBodegaOrigen { get; set; }

        [Display(Name = "EtiquetaBodegaOrigenNombre", ResourceType = typeof(Idioma))]
        public virtual string BodegaOrigenNombre { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "MensajeRequerido", ErrorMessageResourceType = typeof(Idioma))]
        [Display(Name = "EtiquetaBodegaDestinoNombre", ResourceType = typeof(Idioma))]
        public int idBodegaDestino { get; set; }

        [Display(Name = "EtiquetaBodegaDestinoNombre", ResourceType = typeof(Idioma))]
        public virtual string BodegaDestinoNombre { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "MensajeRequerido", ErrorMessageResourceType = typeof(Idioma))]
        [Display(Name = "EtiquetaProyectoNombre", ResourceType = typeof(Idioma))]
        public int idProyecto { get; set; }

        [Display(Name = "EtiquetaClienteNombre", ResourceType = typeof(Idioma))]
        public virtual string ClienteNombre { get; set; }

        [Display(Name = "EtiquetaProyectoNombre", ResourceType = typeof(Idioma))]
        public virtual string ProyectoNombre { get; set; }

        [Display(Name = "EtiquetaDocumentoTipoNombre", ResourceType = typeof(Idioma))]
        public string idDocumentoTipo { get; set; }

        [Display(Name = "EtiquetaDocumentoTipoNombre", ResourceType = typeof(Idioma))]
        public virtual string DocumentoTipoNombre { get; set; }

        [Display(Name = "EtiquetaConductor", ResourceType = typeof(Idioma))]
        public int idConductor { get; set; }

        [Display(Name = "EtiquetaConductor", ResourceType = typeof(Idioma))]
        public virtual string ConductorNombre { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "MensajeRequerido", ErrorMessageResourceType = typeof(Idioma))]
        [Display(Name = "EtiquetaNumero", ResourceType = typeof(Idioma))]
        public int Numero { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "MensajeRequerido", ErrorMessageResourceType = typeof(Idioma))]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "EtiquetaFechaEntrega", ResourceType = typeof(Idioma))]
        public DateTime? FechaEntrega { get; set; }

        [Display(Name = "EtiquetaFechaEntrega", ResourceType = typeof(Idioma))]
        public string DFechaEntrega
        {
            get
            {
                string strFormato = FechaEntrega == null ? string.Empty : FechaEntrega.Value.ToString("dd/MM/yyyy");
                return strFormato;
            }
        }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "MensajeRequerido", ErrorMessageResourceType = typeof(Idioma))]
        [Display(Name = "EtiquetaFecha", ResourceType = typeof(Idioma))]
        public DateTime? Fecha { get; set; }

        [Display(Name = "EtiquetaFecha", ResourceType = typeof(Idioma))]
        public string DFecha
        {
            get
            {
                string strFormato = Fecha == null ? string.Empty : Fecha.Value.ToString("dd/MM/yyyy");
                return strFormato;
            }
        }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "MensajeRequerido", ErrorMessageResourceType = typeof(Idioma))]
        [Display(Name = "EtiquetaFecha", ResourceType = typeof(Idioma))]
        public DateTime? FechaSistema { get; set; }

        [Display(Name = "EtiquetaFecha", ResourceType = typeof(Idioma))]
        public string DFechasistema
        {
            get
            {
                string strFormato = FechaSistema == null ? string.Empty : FechaSistema.Value.ToString("dd/MM/yyyy");
                return strFormato;
            }
        }

        [Display(Name = "EtiquetaFechaPedido", ResourceType = typeof(Idioma))]
        public DateTime FechaPedido { get; set; }

        [Display(Name = "EtiquetaTransporte", ResourceType = typeof(Idioma))]
        public bool Transporte { get; set; }

        [Display(Name = "EtiquetaValorTransporte", ResourceType = typeof(Idioma))]
        public int ValorTransporte { get; set; }

        [Display(Name = "EtiquetaDespacho", ResourceType = typeof(Idioma))]
        public bool Despachado { get; set; }

        [Display(Name = "EtiquetaEquipoAdecuado", ResourceType = typeof(Idioma))]
        public bool EquipoAdecuado { get; set; }

        [Display(Name = "EtiquetaPesoEquipo", ResourceType = typeof(Idioma))]
        public decimal PesoEquipo { get; set; }

        [Display(Name = "EtiquetaValorEquipo", ResourceType = typeof(Idioma))]
        public long ValorEquipo { get; set; }

        [Display(Name = "EtiquetaEstado", ResourceType = typeof(Idioma))]
        public string Estado { get; set; }

        public List<RemisionDetalleDTO> Detalle { get; set; }
        #endregion
    }
}
