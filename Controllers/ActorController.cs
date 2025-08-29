using Microsoft.AspNetCore.Mvc;
using TestApi.DTOS;
using TestApi.Services;

namespace TestApi.Controllers
{
    [ApiController]
    [Route("api/actores")]

    public class ActorController : ControllerBase
    {
        private readonly IActorService actorService;

        public ActorController(IActorService actorService)
        {
            this.actorService = actorService;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
                var actores = await actorService.ObtenerActores();
                return Ok(actores);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetById(int id)
        {
            try
            {
                var actor = await actorService.ObtenerActorPorId(id);

                if (actor is null)
                {
                    return NotFound();
                }
                
                return Ok(actor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post(CrearActorDto crearActorDto)
        {
            try
            {
                var actor = await actorService.CrearActor(crearActorDto);
                return StatusCode(201, actor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, CrearActorDto crearActorDto)
        {
            try
            {
                var result = await actorService.ActualizarActor(id, crearActorDto);

                if (!result)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpPost("actor-pelicula")]
        public async Task<ActionResult> Put(ActorPeliculaDto actorPeliculaDto)
        {
            try
            {
                var result = await actorService.AgregarActorPelicula(actorPeliculaDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpDelete("actor-pelicula/{idActor:int}/{idPelicula:int}")]
        public async Task<ActionResult> Delete(int idActor, int idPelicula)
        {
            try
            {
                var result = await actorService.BorrarActorPelicula(idActor, idPelicula);

                if (!result)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var result = await actorService.BorrarActor(id);

                if (!result)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }
    }
}