using System;
using System.ComponentModel.DataAnnotations;
using CoreGeneral.Recursos;

namespace CoreGeneral.Modelos.SAF
{
    public class RemisionDetalleModelo
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
        public Int32 idRemision { get; set; }
        
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "MensajeRequerido", ErrorMessageResourceType = typeof(Idioma))]
        [Display(Name = "EtiquetaCantidad", ResourceType = typeof(Idioma))]
        public Int32 Cantidad { get; set; }      
        #endregion
    }
}