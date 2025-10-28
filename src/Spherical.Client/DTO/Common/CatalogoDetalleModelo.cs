namespace Spherical.Client.DTO.Common
{
    public class CatalogoDetalleModelo
    {
        public string Id { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string? ValorCadena { get; set; }
        public int? ValorNumero { get; set; }
        public decimal? ValorDecimal { get; set; }
        public bool? Activo { get; set; }
    }
}