namespace Defender.Helper
{
    internal static class Configuracion
    {
        private static readonly IConfigurationRoot configuracion = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", true, true)
            .Build();

        internal static string GetDirectorioLlave()
        {
            var valor = configuracion["DirectorioLlave"];
            return string.IsNullOrEmpty(valor) ? string.Empty : valor;
        }

        internal static string GetUrlLogin()
        {
            var valor = configuracion["UrlLogin"];
            return string.IsNullOrEmpty(valor) ? string.Empty : valor;
        }
    }
}
