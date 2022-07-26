using System;
using System.ComponentModel.DataAnnotations;
using Creative.Recursos;

namespace Creative.Modelos.Lineup
{
    public class VDocumentoDetalleModelo
    {
        #region Propiedades

        [Key]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "MensajeRequerido", ErrorMessageResourceType = typeof(Idioma))]
        [Display(Name = "EtiquetaId", ResourceType = typeof(Idioma))]
        public Int32 Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "MensajeRequerido", ErrorMessageResourceType = typeof(Idioma))]
        [Display(Name = "EtiquetaElementoNombre", ResourceType = typeof(Idioma))]
        public Int16 idElemento { get; set; }

        [Display(Name = "EtiquetaElementoNombre", ResourceType = typeof(Idioma))]
        public virtual String ElementoNombre { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "MensajeRequerido", ErrorMessageResourceType = typeof(Idioma))]
        [Display(Name = "EtiquetaId", ResourceType = typeof(Idioma))]
        public Int32 idDocumento { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "MensajeRequerido", ErrorMessageResourceType = typeof(Idioma))]
        [Display(Name = "EtiquetaBodegaOrigenNombre", ResourceType = typeof(Idioma))]
        public Int32 idBodegaOrigen { get; set; }

        [Display(Name = "EtiquetaBodegaOrigenNombre", ResourceType = typeof(Idioma))]
        public virtual String BodegaOrigenNombre { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "MensajeRequerido", ErrorMessageResourceType = typeof(Idioma))]
        [Display(Name = "EtiquetaBodegaDestinoNombre", ResourceType = typeof(Idioma))]
        public Int32 idBodegaDestino { get; set; }

        [Display(Name = "EtiquetaBodegaDestinoNombre", ResourceType = typeof(Idioma))]
        public virtual String BodegaDestinoNombre { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "MensajeRequerido", ErrorMessageResourceType = typeof(Idioma))]
        [Display(Name = "EtiquetaCantidad", ResourceType = typeof(Idioma))]
        public Int32 Cantidad { get; set; }        
        
        
        #endregion
    }
}