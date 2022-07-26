using System;
using System.ComponentModel.DataAnnotations;
using Creative.Recursos;

namespace Creative.Modelos.Lineup
{
    public class ClienteModelo
    {
        #region Propiedades

        [Key]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "MensajeRequerido", ErrorMessageResourceType = typeof(Idioma))]
        [Display(Name = "EtiquetaId", ResourceType = typeof(Idioma))]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "MensajeRequerido", ErrorMessageResourceType = typeof(Idioma))]
        [Display(Name = "EtiquetaCiudadNombre", ResourceType = typeof(Idioma))]
        public string idCiudad { get; set; }

        [StringLength(20, ErrorMessageResourceName = "MensajeTamanoMaximo", ErrorMessageResourceType = typeof(Idioma))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "MensajeRequerido", ErrorMessageResourceType = typeof(Idioma))]
        [Display(Name = "EtiquetaIdentificacion", ResourceType = typeof(Idioma))]
        public string Identificacion { get; set; }

        [StringLength(25, ErrorMessageResourceName = "MensajeTamanoMaximo", ErrorMessageResourceType = typeof(Idioma))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "MensajeRequerido", ErrorMessageResourceType = typeof(Idioma))]
        [Display(Name = "EtiquetaNombre1", ResourceType = typeof(Idioma))]
        public string Nombre1 { get; set; }

        [StringLength(25, ErrorMessageResourceName = "MensajeTamanoMaximo", ErrorMessageResourceType = typeof(Idioma))]
        [Display(Name = "EtiquetaNombre2", ResourceType = typeof(Idioma))]
        public string Nombre2 { get; set; }

        [StringLength(25, ErrorMessageResourceName = "MensajeTamanoMaximo", ErrorMessageResourceType = typeof(Idioma))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "MensajeRequerido", ErrorMessageResourceType = typeof(Idioma))]
        [Display(Name = "EtiquetaApellido1", ResourceType = typeof(Idioma))]
        public string Apellido1 { get; set; }

        [StringLength(25, ErrorMessageResourceName = "MensajeTamanoMaximo", ErrorMessageResourceType = typeof(Idioma))]
        [Display(Name = "EtiquetaApellido2", ResourceType = typeof(Idioma))]
        public string Apellido2 { get; set; }
                
        [Display(Name = "EtiquetaNombre", ResourceType = typeof(Idioma))]
        public string Nombre { get; set; }

        [StringLength(200, ErrorMessageResourceName = "MensajeTamanoMaximo", ErrorMessageResourceType = typeof(Idioma))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "MensajeRequerido", ErrorMessageResourceType = typeof(Idioma))]
        [Display(Name = "EtiquetaDireccion", ResourceType = typeof(Idioma))]
        public string Direccion { get; set; }

        [StringLength(50, ErrorMessageResourceName = "MensajeTamanoMaximo", ErrorMessageResourceType = typeof(Idioma))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "MensajeRequerido", ErrorMessageResourceType = typeof(Idioma))]
        [Display(Name = "EtiquetaTelefono", ResourceType = typeof(Idioma))]
        public string Telefono { get; set; }

        [StringLength(50, ErrorMessageResourceName = "MensajeTamanoMaximo", ErrorMessageResourceType = typeof(Idioma))]
        [Display(Name = "EtiquetaCelular", ResourceType = typeof(Idioma))]
        public string Celular { get; set; }

        [StringLength(100, ErrorMessageResourceName = "MensajeTamanoMaximo", ErrorMessageResourceType = typeof(Idioma))]
        [Display(Name = "EtiquetaCorreo", ResourceType = typeof(Idioma))]
        public string Correo { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "MensajeRequerido", ErrorMessageResourceType = typeof(Idioma))]
        [Display(Name = "EtiquetaActivo", ResourceType = typeof(Idioma))]
        public Boolean Activo { get; set; }

        #endregion
    }
}