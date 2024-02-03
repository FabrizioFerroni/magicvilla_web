using MagicVilla_MVC.Models.Dto;

namespace MagicVilla_MVC.Models.ViewModel
{
    public class VillaPaginadoViewModel
    {
        public int PageNumber { get; set; } = 1;
        public int TotalPaginas { get; set; } = 0;
        public string Previo { get; set; } = "disabled";
        public string Siguiente { get; set; } = "";
        public IEnumerable<VillaDto> VillaList { get; set; }
    }
}
