namespace Tactic.Helper
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

        internal static string GetSistema()
        {
            var valor = configuracion["Sistema"];
            return string.IsNullOrEmpty(valor) ? string.Empty : valor;
        }

        internal static string GetUrlApiDefender()
        {
            var valor = configuracion["UrlApiDefender"];
            return string.IsNullOrEmpty(valor) ? string.Empty : valor;
        }

        internal static string GetEmpresaId()
        {
            var valor = configuracion["EmpresaId"];
            return string.IsNullOrEmpty(valor) ? string.Empty : valor;
        }
    }
}
