using AutoMapper;
using MagicVilla_MVC.Models.Dto;
using MagicVilla_MVC.Services.IServices;
using MagicVilla_MVC.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;

namespace MagicVilla_MVC.Controllers
{
    public class VillaController : Controller
    {
        private readonly IVillaService _villaService;
        private readonly IMapper _mapper;

        public VillaController(IVillaService villaService, IMapper mapper)
        {
            _villaService = villaService;
            _mapper = mapper;
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Index()
        {
            List<VillaDto> villaList = new();
            string token = HttpContext.Session.GetString(DS.SessionToken);
            var response = await _villaService.ObtenerTodos<ApiResponse>(token);

            if (response != null && response.IsSuccess)
            {
                villaList = JsonConvert.DeserializeObject<List<VillaDto>>(Convert.ToString(response.Data)!)!;
            }

            return View(villaList);
        }

        [Authorize(Roles = "admin")]
        public  IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(VillaCreateDto dto)
        {
            if (ModelState.IsValid)
            {
                string token = HttpContext.Session.GetString(DS.SessionToken);
                var response = await _villaService.Crear<ApiResponse>(dto, token);

                if (response != null && response.IsSuccess)
                {
                    TempData["exitoso"] = response.Data;
                    return RedirectToAction(nameof(Index));
                }
            }
            TempData["error"] = "Un Error Ocurrio al Crear la villa";
            return View(dto);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Actualizar(string id)
        {
            string token = HttpContext.Session.GetString(DS.SessionToken);
            var response = await _villaService.Obtener<ApiResponse>(id, token);
            if(response != null && response.IsSuccess)
            {
                VillaDto dto = JsonConvert.DeserializeObject<VillaDto>(Convert.ToString(response.Data)!)!;
                var dataMap = _mapper.Map<VillaUpdateDto>(dto);
                return View(dataMap);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Actualizar(VillaUpdateDto dto)
        {
            if (ModelState.IsValid)
            {
                string token = HttpContext.Session.GetString(DS.SessionToken);
                var response = await _villaService.Actualizar<ApiResponse>(dto, token);

                if (response != null && response.IsSuccess)
                {
                    TempData["exitoso"] = response.Data;
                    return RedirectToAction(nameof(Index));
                }
            }
            TempData["error"] = "Un Error Ocurrio al Actualizar la villa";
            return View(dto);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Remover(string id)
        {
            string token = HttpContext.Session.GetString(DS.SessionToken);
            var response = await _villaService.Obtener<ApiResponse>(id, token);
            if (response != null && response.IsSuccess)
            {
                VillaDto dto = JsonConvert.DeserializeObject<VillaDto>(Convert.ToString(response.Data)!)!;
                return View(dto);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoverVilla(string id)
        {
            string token = HttpContext.Session.GetString(DS.SessionToken);
            var response = await _villaService.Remover<ApiResponse>(id, token);

                if (response != null && response.IsSuccess)
                {
                TempData["exitoso"] = response.Data;
                return RedirectToAction(nameof(Index));
                }

            TempData["error"] = "Un Error Ocurrio al Remover la villa";

            return View(id);
        }

    }
}
