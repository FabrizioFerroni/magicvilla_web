using AutoMapper;
using MagicVilla_MVC.Models.Dto;
using MagicVilla_MVC.Models.ViewModel;
using MagicVilla_MVC.Services;
using MagicVilla_MVC.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using MagicVilla_MVC.Utils;

namespace MagicVilla_MVC.Controllers
{
    public class NumeroVillaController : Controller
    {

        private readonly INumeroVillaService _numeroVillaService;
        private readonly IVillaService _villaService;
        private readonly IMapper _mapper;

        public NumeroVillaController(INumeroVillaService numeroVillaService, IVillaService villaService, IMapper mapper)
        {
            _numeroVillaService = numeroVillaService;
            _villaService = villaService;
            _mapper = mapper;
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Index()
        {
            List<NumeroVillaResponse> numeroVillaList = new();
            string token = HttpContext.Session.GetString(DS.SessionToken);

            var response = await _numeroVillaService.ObtenerTodos<ApiResponse>(token);

            if (response != null && response.IsSuccess)
            {
                numeroVillaList = JsonConvert.DeserializeObject<List<NumeroVillaResponse>>(Convert.ToString(response.Data)!)!;
            }

            return View(numeroVillaList);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Crear()
        {
            NumeroVillaViewModel numeroVillaVM = new();
            string token = HttpContext.Session.GetString(DS.SessionToken);

            var response = await _villaService.ObtenerTodos<ApiResponse>(token);

            if (response != null && response.IsSuccess)
            {
                numeroVillaVM.VillaList = JsonConvert.DeserializeObject<List<VillaDto>>(Convert.ToString(response.Data)!)!
                                            .Select(v => new SelectListItem
                                            {
                                                Text = v.Nombre,
                                                Value = v.Id.ToString()
                                            });
            }
            return View(numeroVillaVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(NumeroVillaViewModel model)
        {
            string token = HttpContext.Session.GetString(DS.SessionToken);
            if (ModelState.IsValid)
            {
                var response = await _numeroVillaService.Crear<ApiResponse>(model.NumeroVilla, token);

                if (response != null && response.IsSuccess)
                {
                    TempData["exitoso"] = response.Data;
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    if (response.ErrorMensaje.Count > 0)
                    {
                        ModelState.AddModelError("ErrorMensaje", response.ErrorMensaje.FirstOrDefault());
                    }
                }
            }

            var res = await _villaService.ObtenerTodos<ApiResponse>(token);

            if (res != null && res.IsSuccess)
            {
                model.VillaList = JsonConvert.DeserializeObject<List<VillaDto>>(Convert.ToString(res.Data)!)!.Select(v => new SelectListItem
                {
                    Text = v.Nombre,
                    Value = v.Id.ToString()
                });
            }

            TempData["error"] = "Un Error Ocurrio al Crear el numero de villa";
            return View(model);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Actualizar(string id)
        {
            NumeroVillaUpdateViewModel numeroVillaVM = new();
            string token = HttpContext.Session.GetString(DS.SessionToken);
            var response = await _numeroVillaService.Obtener<ApiResponse>(id, token);
            if (response != null && response.IsSuccess)
            {
                NumeroVillaResponse model = JsonConvert.DeserializeObject<NumeroVillaResponse>(Convert.ToString(response.Data)!)!;
                numeroVillaVM.NumeroVilla = _mapper.Map<NumeroVillaUpdateRequest>(model);
            }
            response = await _villaService.ObtenerTodos<ApiResponse>(token);

            if (response != null && response.IsSuccess)
            {
                numeroVillaVM.VillaList = JsonConvert.DeserializeObject<List<VillaDto>>(Convert.ToString(response.Data)!)!
                                            .Select(v => new SelectListItem
                                            {
                                                Text = v.Nombre,
                                                Value = v.Id.ToString()
                                            });

                return View(numeroVillaVM);
            }

            return RedirectToAction(nameof(Index));

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Actualizar(NumeroVillaUpdateViewModel model)
        {
                string token = HttpContext.Session.GetString(DS.SessionToken);
            if (ModelState.IsValid)
            {
                var response = await _numeroVillaService.Actualizar<ApiResponse>(model.NumeroVilla, token);

                if (response != null && response.IsSuccess)
                {
                    TempData["exitoso"] = response.Data;
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    if (response.ErrorMensaje.Count > 0)
                    {
                        ModelState.AddModelError("ErrorMensaje", response.ErrorMensaje.FirstOrDefault());
                    }
                }
            }


            var res = await _villaService.ObtenerTodos<ApiResponse>(token);

            if (res != null && res.IsSuccess)
            {
                model.VillaList = JsonConvert.DeserializeObject<List<VillaDto>>(Convert.ToString(res.Data)!)!.Select(v => new SelectListItem
                {
                    Text = v.Nombre,
                    Value = v.Id.ToString()
                });
            }
            TempData["error"] = "Un Error Ocurrio al Actualizar el numero de villa";
            return View(model);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Remover(string id)
        {
            NumeroVillaDeleteViewModel numeroVillaVM = new();
            string token = HttpContext.Session.GetString(DS.SessionToken);
            var response = await _numeroVillaService.Obtener<ApiResponse>(id, token);
            if (response != null && response.IsSuccess)
            {
                NumeroVillaResponse model = JsonConvert.DeserializeObject<NumeroVillaResponse>(Convert.ToString(response.Data)!)!;
                numeroVillaVM.NumeroVilla = model;
            }
            response = await _villaService.ObtenerTodos<ApiResponse>(token);

            if (response != null && response.IsSuccess)
            {
                numeroVillaVM.VillaList = JsonConvert.DeserializeObject<List<VillaDto>>(Convert.ToString(response.Data)!)!
                                            .Select(v => new SelectListItem
                                            {
                                                Text = v.Nombre,
                                                Value = v.Id.ToString()
                                            });

                return View(numeroVillaVM);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoverNumeroVilla(NumeroVillaDeleteViewModel modelo)
        {
            string token = HttpContext.Session.GetString(DS.SessionToken);
            var response = await _numeroVillaService.Remover<ApiResponse>(modelo.NumeroVilla.VillaNro, token);
            if (response != null && response.IsSuccess)
            {
                TempData["exitoso"] = response.Data;
                return RedirectToAction(nameof(Index));
            }
            TempData["error"] = "Un Error Ocurrio al Remover el numero de villa";
            return View(nameof(Remover));
        }
    }
}
