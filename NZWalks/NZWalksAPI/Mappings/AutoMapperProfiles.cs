using AutoMapper;
using NZWalksAPI.Models.Data.Difficulty;
using NZWalksAPI.Models.Data.Regiones;
using NZWalksAPI.Models.Data.Walks;
using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() 
        {
            //Region
            CreateMap<Region, RegionData>().ReverseMap();
            CreateMap<Region, AddRegionData>().ReverseMap();
            CreateMap<Region, UpdateRegionData>().ReverseMap();

            //Walk
            CreateMap<Walk, AddWalkData>().ReverseMap();
            CreateMap<Walk, WalkData>().ReverseMap();
            CreateMap<Walk, UpdateWalkData>().ReverseMap();

            CreateMap<Difficulty, DifficultyData>().ReverseMap();
        }
    }
}
