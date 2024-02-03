using MagicVilla_MVC.Models.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MagicVilla_MVC.Models.ViewModel
{
    public class NumeroVillaDeleteViewModel
    {

        public NumeroVillaDeleteViewModel()
        {
            NumeroVilla = new NumeroVillaResponse();
        }

        public NumeroVillaResponse NumeroVilla { get; set; }

        public IEnumerable<SelectListItem> VillaList { get; set; }
    }
}
