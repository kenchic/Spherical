namespace Spherical.Client.DTO.Inventory
{
    public class DocumentoDetalleDto
    {
        public int Id { get; set; }
        public short IdElemento { get; set; }
        public int IdDocumento { get; set; }
        public int Cantidad { get; set; }
        // Datos auxiliares para UI
        public string? ElementoNombre { get; set; }
    }
}