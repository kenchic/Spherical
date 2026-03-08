namespace Spherical.Client.DTO.Lineup
{
    public class BodegaDto
    {
        public int Id { get; set; }
        public int? IdProyecto { get; set; }
        public short? IdProveedor { get; set; }
        public string Empresa { get; set; } = string.Empty;
        public string Codigo { get; set; } = string.Empty;
        public string? Nombre { get; set; }
        public bool EsSistema { get; set; }
        public bool Activo { get; set; }
    }
}