namespace Defender.Api.Models
{
    public class MenuDTO
    {
        public string Id { get; set; } = null!;

        public string Empresa { get; set; } = null!;

        public string? IdMenu { get; set; }

        public string Nombre { get; set; } = null!;

        public string? Url { get; set; }

        public short Orden { get; set; }

        public string? Icono { get; set; }

        public bool Activo { get; set; }
    }
}
