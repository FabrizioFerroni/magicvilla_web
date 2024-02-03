using MagicVilla_MVC.Models.Dto;

namespace MagicVilla_MVC.Services.IServices
{
    public interface IBaseService
    {
        public ApiResponse responseModel { get; set; }
        Task<T> SendAsync<T>(ApiRequest apiRequest);
    }
}
