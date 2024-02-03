using AutoMapper;
using MagicVilla_MVC.Models;
using MagicVilla_MVC.Models.Dto;
using MagicVilla_MVC.Models.ViewModel;
using MagicVilla_MVC.Services.IServices;
using MagicVilla_MVC.Utils;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace MagicVilla_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IVillaService _villaService;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, IVillaService villaService, IMapper mapper)
        {
            _logger = logger;
            _villaService = villaService;
            _mapper = mapper;
        }

        public async  Task<IActionResult> Index(int page = 1)
        {
            List<VillaDto> villaList = new();
            VillaPaginadoViewModel villaVM = new VillaPaginadoViewModel();

            if (page < 1) page = 1;

            string token = HttpContext.Session.GetString(DS.SessionToken);

            var response = await _villaService.ObtenerTodosPaginados<ApiResponse>(token, page, 4);

            if (response != null && response.IsSuccess)
            {
                villaList = JsonConvert.DeserializeObject<List<VillaDto>>(Convert.ToString(response.Data)!)!;
                villaVM = new VillaPaginadoViewModel()
                {
                    VillaList = villaList,
                    PageNumber = page,
                    TotalPaginas = JsonConvert.DeserializeObject<int>(Convert.ToString(response.TotalPaginas))
                };

                if (page > 1) villaVM.Previo = "";
                if (villaVM.TotalPaginas <= page) villaVM.Siguiente = "disabled";
            }

            return View(villaVM);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}