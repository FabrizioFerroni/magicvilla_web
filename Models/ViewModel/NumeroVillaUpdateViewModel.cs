using MagicVilla_MVC.Models.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MagicVilla_MVC.Models.ViewModel
{
    public class NumeroVillaUpdateViewModel
    {
        public NumeroVillaUpdateViewModel()
        {
            NumeroVilla = new NumeroVillaUpdateRequest();
        }
        public NumeroVillaUpdateRequest NumeroVilla { get; set; }
        public IEnumerable<SelectListItem> VillaList { get; set; }
    }
}
