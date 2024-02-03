using MagicVilla_MVC.Models.Dto;

namespace MagicVilla_MVC.Services.IServices
{
    public interface INumeroVillaService
    {
        Task<T> ObtenerTodos<T>(string token);
        Task<T> Obtener<T>(string id, string token);
        Task<T> Crear<T>(NumeroVillaCreateRequest dto, string token);
        Task<T> Actualizar<T>(NumeroVillaUpdateRequest dto, string token);
        Task<T> Remover<T>(int VillaNro, string token);
    }
}
