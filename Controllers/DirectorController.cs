using Microsoft.AspNetCore.Mvc;
using TestApi.DTOS;
using TestApi.Services;

namespace TestApi.Controllers
{
    [ApiController]
    [Route("api/directores")]
    public class DirectorController : ControllerBase
    {
        private readonly IDirectorService directorService;

        public DirectorController(IDirectorService directorService)
        {
            this.directorService = directorService;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
                var directores = await directorService.ObtenerDirectores();
                return Ok(directores);
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
                var director = await directorService.ObtenerDirectorPorId(id);
                return Ok(director);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post(CrearDirectorDto crearDirectorDto)
        {
            try
            {
                var director = await directorService.CrearDirector(crearDirectorDto);
                return StatusCode(201, director);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpPost("director-pelicula")]
        public async Task<ActionResult> Put(DirectorPeliculaDto directorPeliculaDto)
        {
            try
            {
                var result = await directorService.AgregarDirectorPelicula(directorPeliculaDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpDelete("actor-pelicula/{idDirector:int}/{idPelicula:int}")]
        public async Task<ActionResult> Delete(int idDirector, int idPelicula)
        {
            try
            {
                var result = await directorService.BorrarDirectorPelicula(idDirector, idPelicula);

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

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, CrearDirectorDto crearDirectorDto)
        {
            try
            {
                var result = await directorService.ActualizarDirector(id, crearDirectorDto);

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
                var result = await directorService.BorrarDirector(id);

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