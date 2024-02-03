using AutoMapper;
using MagicVilla_MVC.Models.Dto;
using MagicVilla_MVC.Services.IServices;
using MagicVilla_MVC.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MagicVilla_MVC.Controllers
{
    public class UsuarioController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly IUsuarioService _usuarioService;
        private readonly IMapper _mapper;

        public UsuarioController(ILogger<HomeController> logger, IUsuarioService usuarioService, IMapper mapper)
        {
            _logger = logger;
            _usuarioService = usuarioService;
            _mapper = mapper;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginRequestDto dto)
        {
            var response = await _usuarioService.Login<ApiResponse>(dto);

            if(response != null && response.IsSuccess)
            {
                LoginResponseDto objeto = JsonConvert.DeserializeObject<LoginResponseDto>(Convert.ToString(response.Data));

                var handler = new JwtSecurityTokenHandler();
                var jwt = handler.ReadJwtToken(objeto.Token);

                //Claims 
                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

                identity.AddClaim(new Claim(ClaimTypes.Name, jwt.Claims.FirstOrDefault(c => c.Type == "unique_name").Value));
                identity.AddClaim(new Claim(ClaimTypes.Email, jwt.Claims.FirstOrDefault(c => c.Type == "email").Value));
                identity.AddClaim(new Claim(ClaimTypes.Role, jwt.Claims.FirstOrDefault(c => c.Type == "role").Value));

                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                HttpContext.Session.SetString(DS.SessionToken, objeto.Token);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("ErrorMensaje", response.ErrorMensaje.FirstOrDefault());
                return View(dto);
            }
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegistroRequestDto dto)
        {
            var response = await _usuarioService.Register<ApiResponse>(dto);

            if(response != null && response.IsSuccess)
            {
                return RedirectToAction("login");
            }
            return View(dto);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            HttpContext.Session.SetString(DS.SessionToken, "");
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccesoDenegado()
        {
            return View();
        }
    }
}
