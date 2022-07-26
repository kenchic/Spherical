using System;
using System.ComponentModel.DataAnnotations;
using Creative.Recursos;

namespace Creative.Modelos.Lineup
{
    public class VClienteModelo
    {
        #region Propiedades

        [Key]
        [Display(Name = "EtiquetaId", ResourceType = typeof(Idioma))]
        public int Id { get; set; }

        [Key]
        [Display(Name = "EtiquetaCiudadNombre", ResourceType = typeof(Idioma))]
        public string idCiudad { get; set; }

        [Display(Name = "EtiquetaCiudadNombre", ResourceType = typeof(Idioma))]
        public string CiudadNombre { get; set; }

        [Display(Name = "EtiquetaIdentificacion", ResourceType = typeof(Idioma))]
        public string Identificacion { get; set; }

        [Display(Name = "EtiquetaNombre1", ResourceType = typeof(Idioma))]
        public string Nombre1 { get; set; }

        [Display(Name = "EtiquetaNombre2", ResourceType = typeof(Idioma))]
        public string Nombre2 { get; set; }

        [Display(Name = "EtiquetaApellido1", ResourceType = typeof(Idioma))]
        public string Apellido1 { get; set; }

        [Display(Name = "EtiquetaApellido2", ResourceType = typeof(Idioma))]
        public string Apellido2 { get; set; }
                
        [Display(Name = "EtiquetaNombre", ResourceType = typeof(Idioma))]
        public string Nombre { get; set; }

        [Display(Name = "EtiquetaDireccion", ResourceType = typeof(Idioma))]
        public string Direccion { get; set; }

        [Display(Name = "EtiquetaTelefono", ResourceType = typeof(Idioma))]
        public string Telefono { get; set; }

        [Display(Name = "EtiquetaCelular", ResourceType = typeof(Idioma))]
        public string Celular { get; set; }

        [Display(Name = "EtiquetaCorreo", ResourceType = typeof(Idioma))]
        public string Correo { get; set; }

        [Display(Name = "EtiquetaActivo", ResourceType = typeof(Idioma))]
        public bool Activo { get; set; }

        #endregion
    }
}