using System;
using System.ComponentModel.DataAnnotations;
using Creative.Recursos;

namespace Creative.Modelos.Lineup
{
    public class VBodegaModelo
    {
        #region Propiedades

        [Key]
        [Display(Name = "EtiquetaId", ResourceType = typeof(Idioma))]
        public int Id { get; set; }

        [Display(Name = "EtiquetaProyectoNombre", ResourceType = typeof(Idioma))]
        public int? idProyecto { get; set; }

        [Display(Name = "EtiquetaProyectoNombre", ResourceType = typeof(Idioma))]
        public virtual String ProyectoNombre { get; set; }

        [Display(Name = "EtiquetaProveedorNombre", ResourceType = typeof(Idioma))]
        public int? idProveedor { get; set; }

        [Display(Name = "EtiquetaProveedorNombre", ResourceType = typeof(Idioma))]
        public virtual string ProveedorNombre { get; set; }

        [Display(Name = "EtiquetaCodigo", ResourceType = typeof(Idioma))]
        public string Codigo { get; set; }

        [Display(Name = "EtiquetaNombre", ResourceType = typeof(Idioma))]
        public string Nombre { get; set; }

        [Display(Name = "EtiquetaEsSistema", ResourceType = typeof(Idioma))]
        public bool EsSistema { get; set; }

        [Display(Name = "EtiquetaActivo", ResourceType = typeof(Idioma))]
        public bool Activo { get; set; }

        #endregion
    }
}