namespace Spherical.Helper
{
    internal static class Configuracion
    {
        private static readonly IConfigurationRoot configuracion = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", true, true)
            .Build();

        internal static string UrlApi
        {
            get
            {
                return configuracion["UrlApi"];
            }
        }

        internal static string DirectorioLlave
        {
            get
            {
                return configuracion["DirectorioLlave"];
            }
        }
    }
}
