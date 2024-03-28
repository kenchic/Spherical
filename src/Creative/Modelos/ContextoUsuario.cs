using Creative.DTO.Defender;

namespace Creative.Modelos
{
    public class ContextoUsuario
    {
        public UsuarioDTO Usuario { get; set; }

        public string Empresa { get; set; }

        public string Sistema { get; set; }

        public ContextoUsuario(string sistema)
        {
            Sistema = sistema;
        }
    }
}
