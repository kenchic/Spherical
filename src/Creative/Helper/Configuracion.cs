
using Microsoft.Extensions.Configuration;

namespace Creative.Helper
{
    public static class Configuracion
    {
        private static readonly IConfiguration configuracion = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", true, true)
            .Build();

        public static string UrlApiDefender()
        {
            var valor = configuracion["UrlApiDefender"];
            return string.IsNullOrEmpty(valor) ? string.Empty : valor;
        }

        //internal static string DirectorioLlave()
        //{
        //    var valor = configuracion["DirectorioLlave"];
        //    return string.IsNullOrEmpty(valor) ? string.Empty : valor;
        //}
    }
}
