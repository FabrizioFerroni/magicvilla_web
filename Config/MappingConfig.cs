using AutoMapper;
using MagicVilla_MVC.Models.Dto;

namespace MagicVilla_MVC.Config
{
    public class MappingConfig: Profile
    {
        public MappingConfig() {
            CreateMap<VillaDto, VillaCreateDto>().ReverseMap();
            CreateMap<VillaDto, VillaUpdateDto>().ReverseMap();
            CreateMap<NumeroVillaResponse, NumeroVillaCreateRequest>().ReverseMap();
            CreateMap<NumeroVillaResponse, NumeroVillaUpdateRequest>().ReverseMap();
        }
    }
}
