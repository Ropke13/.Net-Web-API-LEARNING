using AutoMapper;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.DTO;

namespace NZWalksAPI.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() 
        {
            //Region
            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<Region, AddRegionRequestDto>().ReverseMap();
            CreateMap<Region, UpdateRegionRequestDto>().ReverseMap();

            //Walk
            CreateMap<Walk, AddWalkData>().ReverseMap();
            CreateMap<Walk, WalkData>().ReverseMap();
            CreateMap<Walk, UpdateWalkData>().ReverseMap();

            CreateMap<Difficulty, DifficultyData>().ReverseMap();
        }
    }
}
