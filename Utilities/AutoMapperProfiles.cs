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
            CreateMap<ActorDto, ActorPelicula>().ReverseMap();
            CreateMap<ActorPeliculaDto, ActorPelicula>().ReverseMap();

            // DIRECTORES
            CreateMap<Directores, DirectorDto>().ReverseMap();
            CreateMap<CrearDirectorDto, Directores>().ReverseMap();
            CreateMap<DirectorDto, DirectorPelicula>().ReverseMap();
            CreateMap<DirectorPeliculaDto, DirectorPelicula>().ReverseMap();

            // GENEROS
            CreateMap<Generos, GeneroDto>().ReverseMap();
            CreateMap<CrearGeneroDto, Generos>().ReverseMap();
            CreateMap<GeneroDto, GeneroPelicula>().ReverseMap();
            CreateMap<GeneroPeliculaDto, GeneroPelicula>().ReverseMap();

            // PELICULAS
            CreateMap<Peliculas, PeliculaDto>().ReverseMap();
            CreateMap<Peliculas, PeliculaCollectionDto>().ForMember(dest => dest.Actores, config => config.MapFrom(src => src.ActorPeliculas.Select(x => x.IdActorNavigation))).
            ForMember(dest => dest.Directores, config => config.MapFrom(src => src.DirectorPeliculas.Select(x => x.IdDirectorNavigation))).ForMember(dest => dest.Generos, config => config.MapFrom(src => src.GeneroPeliculas.Select(x => x.IdGeneroNavigation)));
            CreateMap<CrearPeliculaDto, Peliculas>().
            ForMember(dest => dest.ActorPeliculas, opt => opt.Ignore()).
            ForMember(dest => dest.DirectorPeliculas, opt => opt.Ignore()).
            ForMember(dest => dest.GeneroPeliculas, opt => opt.Ignore());
        }
    }
}