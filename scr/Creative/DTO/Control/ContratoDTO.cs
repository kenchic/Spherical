using System;
using System.ComponentModel.DataAnnotations;
using Creative.Recursos;

namespace Creative.DTO.Control
{
    public class ContratoDTO
    {
        #region Propiedades

        [Key]
        [Display(Name = "EtiquetaId", ResourceType = typeof(Idioma))]
        public short Id { get; set; }

        public short idProyecto { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "MensajeRequerido", ErrorMessageResourceType = typeof(Idioma))]
        [Display(Name = "EtiquetaListaPrecioNombre", ResourceType = typeof(Idioma))]
        public byte idListaPrecio { get; set; }

        [Display(Name = "EtiquetaAgenteNombre", ResourceType = typeof(Idioma))]
        public short? idAgente { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "MensajeRequerido", ErrorMessageResourceType = typeof(Idioma))]
        [Display(Name = "EtiquetaInformacionBD", ResourceType = typeof(Idioma))]
        public bool InformacionBD { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "MensajeRequerido", ErrorMessageResourceType = typeof(Idioma))]
        [Display(Name = "EtiquetaContratoAlquiler", ResourceType = typeof(Idioma))]
        public bool ContratoAlquiler { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "MensajeRequerido", ErrorMessageResourceType = typeof(Idioma))]
        [Display(Name = "EtiquetaCartaPagare", ResourceType = typeof(Idioma))]
        public bool CartaPagare { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "MensajeRequerido", ErrorMessageResourceType = typeof(Idioma))]
        [Display(Name = "EtiquetaPagare", ResourceType = typeof(Idioma))]
        public bool Pagare { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "MensajeRequerido", ErrorMessageResourceType = typeof(Idioma))]
        [Display(Name = "EtiquetaLetraCambio", ResourceType = typeof(Idioma))]
        public bool LetraCambio { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "MensajeRequerido", ErrorMessageResourceType = typeof(Idioma))]
        [Display(Name = "EtiquetaGarantiasCondiciones", ResourceType = typeof(Idioma))]
        public bool GarantiasCondiciones { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "MensajeRequerido", ErrorMessageResourceType = typeof(Idioma))]
        [Display(Name = "EtiquetaDeposito", ResourceType = typeof(Idioma))]
        public bool Deposito { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "MensajeRequerido", ErrorMessageResourceType = typeof(Idioma))]
        [Display(Name = "EtiquetaAnticipo", ResourceType = typeof(Idioma))]
        public bool Anticipo { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "MensajeRequerido", ErrorMessageResourceType = typeof(Idioma))]
        [Display(Name = "EtiquetaPersonaJuridica", ResourceType = typeof(Idioma))]
        public bool PersonaJuridica { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "MensajeRequerido", ErrorMessageResourceType = typeof(Idioma))]
        [Display(Name = "EtiquetaPersonaNatural", ResourceType = typeof(Idioma))]
        public bool PersonaNatural { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "MensajeRequerido", ErrorMessageResourceType = typeof(Idioma))]
        [Display(Name = "EtiquetaFotoCopiaCedula", ResourceType = typeof(Idioma))]
        public bool FotoCopiaCedula { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "MensajeRequerido", ErrorMessageResourceType = typeof(Idioma))]
        [Display(Name = "EtiquetaFotoCopiaNit", ResourceType = typeof(Idioma))]
        public bool FotoCopiaNit { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "MensajeRequerido", ErrorMessageResourceType = typeof(Idioma))]
        [Display(Name = "EtiquetaCamaraComercio", ResourceType = typeof(Idioma))]
        public bool CamaraComercio { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "MensajeRequerido", ErrorMessageResourceType = typeof(Idioma))]
        [Display(Name = "EtiquetaDescuentoAlquiler", ResourceType = typeof(Idioma))]
        public byte DescuentoAlquiler { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "MensajeRequerido", ErrorMessageResourceType = typeof(Idioma))]
        [Display(Name = "EtiquetaDescuentoVenta", ResourceType = typeof(Idioma))]
        public byte DescuentoVenta { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "MensajeRequerido", ErrorMessageResourceType = typeof(Idioma))]
        [Display(Name = "EtiquetaDescuentoReposicion", ResourceType = typeof(Idioma))]
        public byte DescuentoReposicion { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "MensajeRequerido", ErrorMessageResourceType = typeof(Idioma))]
        [Display(Name = "EtiquetaDescuentoMantenimiento", ResourceType = typeof(Idioma))]
        public byte DescuentoMantenimiento { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "MensajeRequerido", ErrorMessageResourceType = typeof(Idioma))]
        [Display(Name = "EtiquetaDescuentoTransporte", ResourceType = typeof(Idioma))]
        public byte DescuentoTransporte { get; set; }

        [Display(Name = "EtiquetaPorcentajeAgente", ResourceType = typeof(Idioma))]
        public byte? PorcentajeAgente { get; set; }

        #endregion
    }
}