using API.Models.Domain;
using API.Models.DTO;
using AutoMapper;

namespace API.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Map Between Region Domain Model & Region DTOs
            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<Region, NewRegionDto>().ReverseMap();
            CreateMap<Region, UpdateRegionDto>().ReverseMap();

            // Map Between Walk Domain Model & Walk DTOs
            CreateMap<Walk, WalkDTO>().ReverseMap();
            CreateMap<Walk, NewWalkDto>().ReverseMap();
            CreateMap<Walk, UpdateWalkDto>().ReverseMap();

            // Map Between Difficulty Domain Model & Difficulty DTO
            CreateMap<Difficulty, DifficultyDto>().ReverseMap();
        }
    }
}
