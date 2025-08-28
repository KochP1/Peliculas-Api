using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TestApi.Data;
using TestApi.DTOS;
using TestApi.Models;

namespace TestApi.Services
{
    public interface IPeliculaService
    {
        Task<IEnumerable<PeliculaDto>> ObtenerPeliculas();
        Task<PeliculaCollectionDto> ObtenerPeliculasPorId(int id);
        Task<PeliculaDto> CrearPelicula(CrearPeliculaDto crearPeliculaDto);
        Task<bool> BorrarPelicula(int id);
    }

    public class PeliculaService : IPeliculaService
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public PeliculaService(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<PeliculaDto>> ObtenerPeliculas()
        {
            var peliculas = await context.Peliculas.OrderBy(x => x.Nombre).ToListAsync();
            var peliculasDto = mapper.Map<IEnumerable<PeliculaDto>>(peliculas);

            return peliculasDto;
        }

        public async Task<PeliculaCollectionDto> ObtenerPeliculasPorId(int id)
        {
            var pelicula = await context.Peliculas
            .OrderBy(x => x.Nombre)
            .Include(x => x.ActorPeliculas)
                .ThenInclude(x => x.IdActorNavigation)
            .Include(x => x.DirectorPeliculas)
                .ThenInclude(x => x.IdDirectorNavigation)
            .Include(x => x.GeneroPeliculas)
                .ThenInclude(x => x.IdGeneroNavigation)
            .FirstOrDefaultAsync(x => x.Id == id);

            var peliculaDto = mapper.Map<PeliculaCollectionDto>(pelicula);
            return peliculaDto;
        }

        public async Task<PeliculaDto> CrearPelicula(CrearPeliculaDto crearPeliculaDto)
        {
            var pelicula = mapper.Map<Peliculas>(crearPeliculaDto);
            context.Peliculas.Add(pelicula);
            await context.SaveChangesAsync();

            foreach (var item in crearPeliculaDto.Actores)
            {
                context.ActorPeliculas.Add(new ActorPelicula { IdActor = item, IdPelicula = pelicula.Id });
            }

            foreach (var item in crearPeliculaDto.Directores)
            {
                context.DirectorPeliculas.Add(new DirectorPelicula { IdDirector = item, IdPelicula = pelicula.Id });
            }

            foreach (var item in crearPeliculaDto.Generos)
            {
                context.GeneroPeliculas.Add(new GeneroPelicula { IdGenero = item, IdPelicula = pelicula.Id });
            }

            await context.SaveChangesAsync();
            var peliculaDto = mapper.Map<PeliculaDto>(pelicula);
            return peliculaDto;
        }

        public async Task<bool> BorrarPelicula(int id)
        {
            var pelicula = await context.Peliculas.FirstOrDefaultAsync(x => x.Id == id);

            if (pelicula is null)
            {
                return false;
            }

            context.Peliculas.Remove(pelicula);
            await context.SaveChangesAsync();
            return true;
        }
    }
}