using System;
using System.ComponentModel.DataAnnotations;
using Creative.Recursos;

namespace Creative.DTO.Lineup
{
    public class ElementoDTO
    {
        #region Propiedades

        [Key]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "MensajeRequerido", ErrorMessageResourceType = typeof(Idioma))]
        [Display(Name = "EtiquetaId", ResourceType = typeof(Idioma))]
        public Int16 Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "MensajeRequerido", ErrorMessageResourceType = typeof(Idioma))]
        [Display(Name = "EtiquetaGrupoElementoNombre", ResourceType = typeof(Idioma))]
        public Byte idGrupoElemento { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "MensajeRequerido", ErrorMessageResourceType = typeof(Idioma))]
        [Display(Name = "EtiquetaUnidadMedidaNombre", ResourceType = typeof(Idioma))]
        public Byte idUnidadMedida { get; set; }

        [StringLength(50, ErrorMessageResourceName = "MensajeTamanoMaximo", ErrorMessageResourceType = typeof(Idioma))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "MensajeRequerido", ErrorMessageResourceType = typeof(Idioma))]
        [Display(Name = "EtiquetaReferencia", ResourceType = typeof(Idioma))]
        public String Referencia { get; set; }

        [StringLength(100, ErrorMessageResourceName = "MensajeTamanoMaximo", ErrorMessageResourceType = typeof(Idioma))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "MensajeRequerido", ErrorMessageResourceType = typeof(Idioma))]
        [Display(Name = "EtiquetaNombre", ResourceType = typeof(Idioma))]
        public String Nombre { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "MensajeRequerido", ErrorMessageResourceType = typeof(Idioma))]
        [Display(Name = "EtiquetaMt2", ResourceType = typeof(Idioma))]
        public Double Mt2 { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "MensajeRequerido", ErrorMessageResourceType = typeof(Idioma))]
        [Display(Name = "EtiquetaPeso", ResourceType = typeof(Idioma))]
        public Double Peso { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "MensajeRequerido", ErrorMessageResourceType = typeof(Idioma))]
        [Display(Name = "EtiquetaRotacion", ResourceType = typeof(Idioma))]
        public Boolean Rotacion { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "MensajeRequerido", ErrorMessageResourceType = typeof(Idioma))]
        [Display(Name = "EtiquetaActivo", ResourceType = typeof(Idioma))]
        public Boolean Activo { get; set; }

        #endregion
    }
}