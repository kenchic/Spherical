using System.Net;

namespace Spherical.Client.DTO.Spherical
{    
    public class ApiResponse<T>
    {
        public bool Success { get; set; } = true;

        public int StatusCode { get; set; }

        public string ErrorMessage { get; set; }

        public T Data { get; set; }

        public ApiResponse()
        {

        }

        public ApiResponse(HttpStatusCode statusCode, T result, string errorMessage = null)
        {
            StatusCode = (int)statusCode;
            Data = result;
            ErrorMessage = errorMessage ?? GetDefaultMessageStatusCode();
            Success = string.IsNullOrEmpty(errorMessage);
        }

        private string GetDefaultMessageStatusCode()
        {
            return StatusCode switch
            {
                400 => "Error en request",
                401 => "No autorizado",
                403 => "Oculto",
                404 => "Rercurso no encontrado",
                500 => "Error en ruta",
                _ => ""
            };
        }
    }
}
