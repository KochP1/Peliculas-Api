using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TestApi.Data;
using TestApi.DTOS;
using TestApi.Models;

namespace TestApi.Services
{
    public interface IDirectorService
    {
        Task<bool> ActualizarDirector(int id, CrearDirectorDto crearDirectorDto);
        Task<bool> BorrarDirector(int id);
        Task<DirectorDto> CrearDirector(CrearDirectorDto crearDirectorDto);
        Task<IEnumerable<DirectorDto>> ObtenerDirectores();
        Task<DirectorConPeliculaDto> ObtenerDirectorPorId(int id);
        Task<DirectorPeliculaDto> AgregarDirectorPelicula(DirectorPeliculaDto directorPeliculaDto);
        Task<bool> BorrarDirectorPelicula(int idDirector, int idPelicula);

    }

    public class DirectorService : IDirectorService
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public DirectorService(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }


        public async Task<IEnumerable<DirectorDto>> ObtenerDirectores()
        {
            var directores = await context.Directores.OrderBy(x => x.Nombres).ToListAsync();
            var directoresDto = mapper.Map<IEnumerable<DirectorDto>>(directores);
            return directoresDto;
        }

        public async Task<DirectorConPeliculaDto> ObtenerDirectorPorId(int id)
        {
            var director = await context.Directores
                .Include(x => x.DirectorPeliculas)
                    .ThenInclude(x => x.IdPeliculaNavigation)
                .FirstOrDefaultAsync(x => x.Id == id);

            var directorDto = mapper.Map<DirectorConPeliculaDto>(director);
            return directorDto;
        }

        public async Task<DirectorDto> CrearDirector(CrearDirectorDto crearDirectorDto)
        {
            var director = mapper.Map<Directores>(crearDirectorDto);

            context.Directores.Add(director);
            await context.SaveChangesAsync();

            var directorDto = mapper.Map<DirectorDto>(director);
            return directorDto;
        }

        public async Task<bool> ActualizarDirector(int id, CrearDirectorDto crearDirectorDto)
        {
            var director = await context.Directores.FirstOrDefaultAsync(x => x.Id == id);

            if (director is null)
            {
                return false;
            }

            director.Nombres = crearDirectorDto.Nombres;
            director.Apellidos = crearDirectorDto.Apellidos;
            director.Edad = crearDirectorDto.Edad;
            director.FechaNacimiento = crearDirectorDto.FechaNacimiento;

            context.Directores.Update(director);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<DirectorPeliculaDto> AgregarDirectorPelicula(DirectorPeliculaDto directorPeliculaDto)
        {
            var directorPelicula = mapper.Map<DirectorPelicula>(directorPeliculaDto);

            context.Add(directorPelicula);
            await context.SaveChangesAsync();
            return directorPeliculaDto;
        }

        public async Task<bool> BorrarDirectorPelicula(int idDirector, int idPelicula)
        {
            var directorPelicula = await context.DirectorPeliculas.Where(x => x.IdDirector == idDirector).FirstOrDefaultAsync(x => x.IdPelicula == idPelicula);

            if (directorPelicula is null)
            {
                return false;
            }

            context.DirectorPeliculas.Remove(directorPelicula);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> BorrarDirector(int id)
        {
            var director = await context.Directores.FirstOrDefaultAsync(x => x.Id == id);

            if (director is null)
            {
                return false;
            }

            context.Directores.Remove(director);
            await context.SaveChangesAsync();
            return true;
        }
    }
}