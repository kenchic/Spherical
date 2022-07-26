using System;
using System.ComponentModel.DataAnnotations;
using Creative.Recursos;

namespace Creative.Modelos.Lineup
{
    public class VDocumentoModelo
    {
        #region Propiedades

        [Key]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "MensajeRequerido", ErrorMessageResourceType = typeof(Idioma))]
        [Display(Name = "EtiquetaId", ResourceType = typeof(Idioma))]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "MensajeRequerido", ErrorMessageResourceType = typeof(Idioma))]
        [Display(Name = "EtiquetaDocumentoTipoNombre", ResourceType = typeof(Idioma))]
        public string idDocumentoTipo { get; set; }

        [Display(Name = "EtiquetaDocumentoTipoNombre", ResourceType = typeof(Idioma))]
        public virtual string DocumentoTipoNombre { get; set; }

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
        [Display(Name = "EtiquetaNumero", ResourceType = typeof(Idioma))]
        public int Numero { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "MensajeRequerido", ErrorMessageResourceType = typeof(Idioma))]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
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
        [StringLength(1000, ErrorMessageResourceName = "MensajeTamanoMaximo", ErrorMessageResourceType = typeof(Idioma))]
        [Display(Name = "EtiquetaDescripcion", ResourceType = typeof(Idioma))]
        public string Descripcion { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "MensajeRequerido", ErrorMessageResourceType = typeof(Idioma))]
        [Display(Name = "EtiquetaAnulado", ResourceType = typeof(Idioma))]
        public Boolean Anulado { get; set; }

    #endregion
}
}