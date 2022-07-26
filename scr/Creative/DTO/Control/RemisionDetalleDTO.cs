using System.ComponentModel.DataAnnotations;
using Creative.Recursos;

namespace Creative.DTO.Control
{
    public class RemisionDetalleDTO
    {
        #region Propiedades

        [Key]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "MensajeRequerido", ErrorMessageResourceType = typeof(Idioma))]
        [Display(Name = "EtiquetaId", ResourceType = typeof(Idioma))]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "MensajeRequerido", ErrorMessageResourceType = typeof(Idioma))]
        [Display(Name = "EtiquetaElementoNombre", ResourceType = typeof(Idioma))]
        public short idElemento { get; set; }

        [Display(Name = "EtiquetaElementoNombre", ResourceType = typeof(Idioma))]
        public virtual string ElementoNombre { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "MensajeRequerido", ErrorMessageResourceType = typeof(Idioma))]
        [Display(Name = "EtiquetaId", ResourceType = typeof(Idioma))]
        public int idRemision { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "MensajeRequerido", ErrorMessageResourceType = typeof(Idioma))]
        [Display(Name = "EtiquetaCantidad", ResourceType = typeof(Idioma))]
        public int Cantidad { get; set; }
        #endregion
    }
}