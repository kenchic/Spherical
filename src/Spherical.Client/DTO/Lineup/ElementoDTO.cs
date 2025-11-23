using System;
using System.ComponentModel.DataAnnotations;
using Spherical.Core.Creative;

namespace Spherical.Client.DTO.Lineup
{
    public class ElementoDto
    {
        #region Propiedades

        [Key]
        [Required(AllowEmptyStrings = false, ErrorMessage = Constants.RequiredFieldMsg)]
        [Display(Name = "EtiquetaId")]
        public short Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = Constants.RequiredFieldMsg)]
        [Display(Name = "EtiquetaGrupoElementoNombre")]
        public string IdGrupoElemento { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = Constants.RequiredFieldMsg)]
        [Display(Name = "EtiquetaUnidadMedidaNombre")]
        public string IdUnidadMedida { get; set; }

        [StringLength(20, ErrorMessage = Constants.MaxSizeMsg)]
        [Required(AllowEmptyStrings = false, ErrorMessage = Constants.RequiredFieldMsg)]
        public string Empresa { get; set; }

        [StringLength(50, ErrorMessage = Constants.MaxSizeMsg)]
        [Required(AllowEmptyStrings = false, ErrorMessage = Constants.RequiredFieldMsg)]
        [Display(Name = "EtiquetaReferencia")]
        public string Referencia { get; set; }

        [StringLength(100, ErrorMessage = Constants.MaxSizeMsg)]
        [Required(AllowEmptyStrings = false, ErrorMessage = Constants.RequiredFieldMsg)]
        [Display(Name = "EtiquetaNombre")]
        public string Nombre { get; set; }

        [Display(Name = "EtiquetaMt2")]
        public double Mt2 { get; set; }

        [Display(Name = "EtiquetaPeso")]
        public double Peso { get; set; }

        [Display(Name = "EtiquetaRotacion")]
        public bool Rotacion { get; set; }

        [Display(Name = "EtiquetaActivo")]
        public bool Activo { get; set; }

        #endregion
    }
}