namespace Spherical.Client.DTO.Inventory
{
    public class DocumentoDto
    {
        public int Id { get; set; }
        public string IdDocumentoTipo { get; set; } = string.Empty;
        public int IdBodegaOrigen { get; set; }
        public int IdBodegaDestino { get; set; }
        public string Empresa { get; set; } = string.Empty;
        public int Numero { get; set; }
        public DateTime Fecha { get; set; } = DateTime.UtcNow;
        public string? Descripcion { get; set; }
        public string Estado { get; set; } = "ABIERTO";
        public List<DocumentoDetalleDto> Detalles { get; set; } = new();
    }
}