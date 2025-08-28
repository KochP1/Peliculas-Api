using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TestApi.Data;
using TestApi.DTOS;
using TestApi.Models;

namespace TestApi.Services
{
    public interface IGeneroService
    {
        Task<bool> ActualizarGenero(int id, CrearGeneroDto crearGeneroDto);
        Task<bool> BorrarGenero(int id);
        Task<GeneroDto> CrearGenero(CrearGeneroDto crearGeneroDto);
        Task<IEnumerable<GeneroDto>> ObtenerGeneros();
    }

    public class GeneroService : IGeneroService
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public GeneroService(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<GeneroDto>> ObtenerGeneros()
        {
            var generos = await context.Generos.OrderBy(x => x.Genero1).ToListAsync();

            var generosDto = mapper.Map<IEnumerable<GeneroDto>>(generos);
            return generosDto;
        }

        public async Task<GeneroDto> CrearGenero(CrearGeneroDto crearGeneroDto)
        {
            var genero = mapper.Map<Generos>(crearGeneroDto);

            context.Generos.Add(genero);
            await context.SaveChangesAsync();

            var generoDto = mapper.Map<GeneroDto>(genero);
            return generoDto;
        }

        public async Task<bool> ActualizarGenero(int id, CrearGeneroDto crearGeneroDto)
        {
            var genero = await context.Generos.FirstOrDefaultAsync(x => x.Id == id);

            if (genero is null)
            {
                return false;
            }

            genero.Genero1 = crearGeneroDto.Genero1;

            context.Generos.Update(genero);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> BorrarGenero(int id)
        {
            var genero = await context.Generos.FirstOrDefaultAsync(x => x.Id == id);

            if (genero is null)
            {
                return false;
            }

            context.Generos.Remove(genero);
            await context.SaveChangesAsync();
            return true;
        }
    }
}