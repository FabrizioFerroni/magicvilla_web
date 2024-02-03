using System.Net;

namespace MagicVilla_MVC.Models.Dto
{
    public class ApiResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; } = true;
        public List<String>? ErrorMensaje { get; set; }
        public object? Data { get; set; }
        public int TotalPaginas { get; set; }
    }
}
