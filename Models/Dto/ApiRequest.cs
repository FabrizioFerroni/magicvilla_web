using static MagicVilla_MVC.Utils.DS;

namespace MagicVilla_MVC.Models.Dto
{
    public class ApiRequest
    {
        public ApiTipo ApiTipo { get; set; } = ApiTipo.GET;
        public String Url { get; set; } = "";
        public object Datos { get; set; } = "";
        public string Token { get; set; } = "";
        public Parametros Parametros { get; set; }
    }

    public class Parametros
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
