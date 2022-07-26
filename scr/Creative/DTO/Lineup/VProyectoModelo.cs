using System;
using System.ComponentModel.DataAnnotations;
using Creative.Recursos;

namespace Creative.Modelos.Lineup
{
    public class VProyectoModelo
    {
        #region Propiedades

        [Key]
        [Display(Name = "EtiquetaId", ResourceType = typeof(Idioma))]
        public int Id { get; set; }

        [Display(Name = "EtiquetaCiudadNombre", ResourceType = typeof(Idioma))]
        public string idCiudad { get; set; }

        [Display(Name = "EtiquetaCiudadNombre", ResourceType = typeof(Idioma))]
        public string CiudadNombre { get; set; }

        [Display(Name = "EtiquetaClienteNombre", ResourceType = typeof(Idioma))]
        public int idCliente { get; set; }

        [Display(Name = "EtiquetaClienteNombre", ResourceType = typeof(Idioma))]
        public string ClienteNombre { get; set; }

        public int idContrato{ get; set; }

        [Display(Name = "EtiquetaNombre", ResourceType = typeof(Idioma))]
        public string Nombre { get; set; }

        [Display(Name = "EtiquetaTipo", ResourceType = typeof(Idioma))]
        public string Tipo { get; set; }

        [Display(Name = "EtiquetaDireccion", ResourceType = typeof(Idioma))]
        public string Direccion { get; set; }

        [Display(Name = "EtiquetaTelefono", ResourceType = typeof(Idioma))]
        public string Telefono { get; set; }

        [Display(Name = "EtiquetaObservacion", ResourceType = typeof(Idioma))]
        public string Observacion { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [Display(Name = "EtiquetaFecha", ResourceType = typeof(Idioma))]
        public DateTime Fecha { get; set; }

        [Display(Name = "EtiquetaFormaContacto", ResourceType = typeof(Idioma))]
        public string FormaContacto { get; set; }

        [Display(Name = "EtiquetaSistemaMedida", ResourceType = typeof(Idioma))]
        public string SistemaMedida { get; set; }

        [Display(Name = "EtiquetaIdentificacionResponsable", ResourceType = typeof(Idioma))]
        public string IdentificacionResponsable { get; set; }

        [Display(Name = "EtiquetaNombreResponsable", ResourceType = typeof(Idioma))]
        public string NombreResponsable { get; set; }

        [Display(Name = "EtiquetaTelResponsable", ResourceType = typeof(Idioma))]
        public string TelResponsable { get; set; }

        [Display(Name = "EtiquetaActivo", ResourceType = typeof(Idioma))]
        public bool Activo { get; set; }

        [Display(Name = "EtiquetaEstado", ResourceType = typeof(Idioma))]
        public byte Estado { get; set; }

        #endregion
    }
}