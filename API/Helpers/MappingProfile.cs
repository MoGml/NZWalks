using API.Models.Domain;
using API.Models.DTO;
using AutoMapper;

namespace API.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<Region, NewRegionDto>().ReverseMap();
            CreateMap<Region, UpdateRegionDto>().ReverseMap();
        }
    }
}
