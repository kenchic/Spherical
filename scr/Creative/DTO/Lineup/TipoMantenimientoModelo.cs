using System;
using System.ComponentModel.DataAnnotations;
using Creative.Recursos;

namespace Creative.Modelos.Lineup
{
    public class TipoMantenimientoModelo
    {
        #region Propiedades

        [Key]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "MensajeRequerido", ErrorMessageResourceType = typeof(Idioma))]
        [Display(Name = "EtiquetaId", ResourceType = typeof(Idioma))]
        public Int16 Id { get; set; }

        [StringLength(100, ErrorMessageResourceName = "MensajeTamanoMaximo", ErrorMessageResourceType = typeof(Idioma))]
        [Display(Name = "EtiquetaNombre", ResourceType = typeof(Idioma))]
        public String Nombre { get; set; }

        [Display(Name = "EtiquetaValor", ResourceType = typeof(Idioma))]
        public Decimal Valor { get; set; }

        [Display(Name = "EtiquetaActivo", ResourceType = typeof(Idioma))]
        public Boolean Activo { get; set; }

        #endregion
    }
}