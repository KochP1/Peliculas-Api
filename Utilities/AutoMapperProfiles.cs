using AutoMapper;
using TestApi.DTOS;
using TestApi.Models;

namespace TestApi.Utilities
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            // ACTORES
            CreateMap<Actores, ActorDto>().ReverseMap();
            CreateMap<CrearActorDto, Actores>().ReverseMap();

            // DIRECTORES
            CreateMap<Directores, DirectorDto>().ReverseMap();
            CreateMap<CrearDirectorDto, Directores>().ReverseMap();
        }
    }
}