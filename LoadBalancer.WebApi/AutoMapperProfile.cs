using AutoMapper;
using LoadBalancer.WebApi.Data.Entities.PuzzleEntity;
using LoadBalancer.WebApi.Data.Entities.StateEntity;
using LoadBalancer.WebApi.DTOs;

namespace LoadBalancer.WebApi
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Puzzle, PuzzleDto>().ReverseMap();
            CreateMap<Puzzle, PuzzleRequestDto>().ReverseMap();
            CreateMap<StateDto, State>().ReverseMap();
        }
    }
}
