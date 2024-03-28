namespace Lineup.Api.Helper
{
    internal static class Configuracion
    {
        private static readonly IConfigurationRoot configuracion = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", true, true)
            .Build();

        internal static string DirectorioLlave()
        {
            var valor = configuracion["DirectorioLlave"];
            return string.IsNullOrEmpty(valor) ? string.Empty : valor;
        }
    }
}
