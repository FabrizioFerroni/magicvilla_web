using MagicVilla_MVC.Models.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MagicVilla_MVC.Models.ViewModel
{
    public class NumeroVillaViewModel
    {
        public NumeroVillaViewModel()
        {
            NumeroVilla = new NumeroVillaCreateRequest();
        }
        public NumeroVillaCreateRequest NumeroVilla { get; set; }
        public IEnumerable<SelectListItem> VillaList { get; set; }
    }
}
