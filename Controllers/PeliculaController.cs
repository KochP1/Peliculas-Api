using Microsoft.AspNetCore.Mvc;
using TestApi.DTOS;
using TestApi.Services;

namespace TestApi.Controllers
{
    [ApiController]
    [Route("api/peliculas")]
    public class PeliculaController : ControllerBase
    {
        private readonly IPeliculaService peliculaService;

        public PeliculaController(IPeliculaService peliculaService)
        {
            this.peliculaService = peliculaService;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var peliculas = await peliculaService.ObtenerPeliculas();
            return Ok(peliculas);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetById(int id)
        {
            try
            {
                var pelicula = await peliculaService.ObtenerPeliculasPorId(id);

                if (pelicula is null)
                {
                    return NotFound();
                }
                return Ok(pelicula);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post(CrearPeliculaDto crearPeliculaDto)
        {
            try
            {
                var pelicula = await peliculaService.CrearPelicula(crearPeliculaDto);
                return StatusCode(201, pelicula);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Delete(int id, PutPeliculaDto putPeliculaDto)
        {
            try
            {
                var result = await peliculaService.ActualizarPelicula(id, putPeliculaDto);

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
                var result = await peliculaService.BorrarPelicula(id);

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