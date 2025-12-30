namespace Spherical.Client.DTO.Inventory
{
    public class DocumentoTipoDto
    {
        public string Id { get; set; } = string.Empty;
        public string Empresa { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public int Consecutivo { get; set; }
        public string Operacion { get; set; } = "+"; // '+' o '-'
        public short CantidadFilas { get; set; }
        public bool EsSistema { get; set; }
        public bool Activo { get; set; }
    }
}