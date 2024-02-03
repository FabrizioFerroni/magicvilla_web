using MagicVilla_MVC.Models.Dto;

namespace MagicVilla_MVC.Services.IServices
{
    public interface IUsuarioService
    {
        Task<T> Login<T>(LoginRequestDto dto);
        Task<T> Register<T>(RegistroRequestDto dto);
    }
}
