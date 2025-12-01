using System.ComponentModel.DataAnnotations;
using Spherical.Core.Creative;

namespace Spherical.Client.DTO.Inventory
{
    public class DocumentoDto
    {
        [Key]
        [Required(AllowEmptyStrings = false, ErrorMessage = Constants.RequiredFieldMsg)]
        public int Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = Constants.RequiredFieldMsg)]
        public string IdDocumentoTipo { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = Constants.RequiredFieldMsg)]
        public int IdBodegaOrigen { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = Constants.RequiredFieldMsg)]
        public int IdBodegaDestino { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = Constants.RequiredFieldMsg)]
        public string Empresa { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = Constants.RequiredFieldMsg)]
        public int Numero { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = Constants.RequiredFieldMsg)]
        public DateTime Fecha { get; set; } = DateTime.UtcNow;
        [Required(AllowEmptyStrings = false, ErrorMessage = Constants.RequiredFieldMsg)]
        public string? Descripcion { get; set; }
        public string Estado { get; set; } = "ABIERTO";
        public List<DocumentoDetalleDto> Detalles { get; set; } = new();
    }
}