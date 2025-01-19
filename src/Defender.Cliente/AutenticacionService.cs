using Creative.DTO.Defender;
using Creative.Utilidades;
using Creative.Helper;
using Creative.Negocios;
using Creative.Recursos;

namespace Defender.Cliente
{
    public class AutenticacionService
    {
        public string Autenticar(AutenticacionDTO autenticacionDTO)
        {
            try
            {
                var resultado = ClienteApi.PostRecurso(Configuracion.UrlApiDefender(), "api/Autenticar", autenticacionDTO);
                return resultado;
            }
            catch (Exception ex)
            {
                MensajeNegocio.EscribirLog(Constantes.MensajeError, ex.Message, "AutenticacionService - Autenticar");
                throw;
            }
        }
    }
}
