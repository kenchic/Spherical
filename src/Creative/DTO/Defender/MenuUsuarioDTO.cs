namespace Creative.DTO.Defender
{
    public class MenuUsuarioDTO
    {
        public string Id { get; set; } = null!;

        public string Nombre { get; set; } = null!;

        public string? Url { get; set; }

        public int? Orden { get; set; }

        public string? Padre { get; set; }

        public string? Icono { get; set; }
    }
}
