using System;
using System.ComponentModel.DataAnnotations;
using Creative.Recursos;

namespace Creative.Modelos.Lineup
{
    public class VListaPrecioDetalleModelo
    {
        #region Propiedades

        [Display(Name = "EtiquetaId", ResourceType = typeof(Idioma))]
        public Int32 Id { get; set; }

        [Display(Name = "EtiquetaListaPrecioNombre", ResourceType = typeof(Idioma))]
        public virtual String ListaPrecioNombre { get; set; }

        [Display(Name = "EtiquetaListaPrecioNombre", ResourceType = typeof(Idioma))]
        public Byte idListaPrecio { get; set; }

        [Display(Name = "EtiquetaElementoNombre", ResourceType = typeof(Idioma))]
        public Int16 idElemento { get; set; }

        [Display(Name = "EtiquetaElementoNombre", ResourceType = typeof(Idioma))]
        public virtual String ElementoNombre { get; set; }

        [Display(Name = "EtiquetaPrecioAlquiler", ResourceType = typeof(Idioma))]
        public Int32 PrecioAlquiler { get; set; }

        [Display(Name = "EtiquetaPrecioVenta", ResourceType = typeof(Idioma))]
        public Int32 PrecioVenta { get; set; }

        [Display(Name = "EtiquetaPrecioPerdida", ResourceType = typeof(Idioma))]
        public Int32 PrecioPerdida { get; set; }
        
        #endregion
    }
}