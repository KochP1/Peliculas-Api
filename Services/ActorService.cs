using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TestApi.Data;
using TestApi.DTOS;
using TestApi.Models;

namespace TestApi.Services
{
    public interface IActorService
    {
        Task<bool> ActualizarActor(int id, CrearActorDto crearActorDto);
        Task<bool> BorrarActor(int id);
        Task<ActorDto> CrearActor(CrearActorDto crearActorDto);
        Task<IEnumerable<ActorDto>> ObtenerAutores();
    }

    public class ActorService : IActorService
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public ActorService(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<ActorDto>> ObtenerAutores()
        {
            var actores = await context.Actores.OrderBy(x => x.Nombres).ToListAsync();

            var actoresDto = mapper.Map<IEnumerable<ActorDto>>(actores);
            return actoresDto;
        }

        public async Task<ActorDto> CrearActor(CrearActorDto crearActorDto)
        {
            var actor = mapper.Map<Actores>(crearActorDto);

            context.Actores.Add(actor);
            await context.SaveChangesAsync();

            var autorDto = mapper.Map<ActorDto>(actor);
            return autorDto;
        }

        public async Task<bool> ActualizarActor(int id, CrearActorDto crearActorDto)
        {
            var actor = await context.Actores.FirstOrDefaultAsync(x => x.Id == id);

            if (actor is null)
            {
                return false;
            }

            actor.Nombres = crearActorDto.Nombres;
            actor.Apellidos = crearActorDto.Apellidos;
            actor.Edad = crearActorDto.Edad;
            actor.FechaNacimiento = crearActorDto.FechaNacimiento;

            context.Actores.Update(actor);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> BorrarActor(int id)
        {
            var actor = await context.Actores.FirstOrDefaultAsync(x => x.Id == id);

            if (actor is null)
            {
                return false;
            }

            context.Actores.Remove(actor);
            await context.SaveChangesAsync();
            return true;
        }
    }
}