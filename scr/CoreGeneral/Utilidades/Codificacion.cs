using System;
using System.Text;

namespace CoreGeneral.Utilidades
{
    public static class Codificacion
    {
        public static string CodificarBase64String(string cadena)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(cadena));
        }

        public static string DecoficarBase64String(string cadena)
        {
            UTF8Encoding codificador = new UTF8Encoding();
            Decoder decodificadorUtf8 = codificador.GetDecoder();
            byte[] bytesCadena = Convert.FromBase64String(cadena);
            int contador = decodificadorUtf8.GetCharCount(bytesCadena, 0, bytesCadena.Length);
            char[] decodificado = new char[contador];
            decodificadorUtf8.GetChars(bytesCadena, 0, bytesCadena.Length, decodificado, 0);
            return new string(decodificado);
        }
    }
}
