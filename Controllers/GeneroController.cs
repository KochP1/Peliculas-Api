using Microsoft.AspNetCore.Mvc;
using TestApi.DTOS;
using TestApi.Services;

namespace TestApi.Controllers
{
    [ApiController]
    [Route("api/generos")]
    public class GeneroController : ControllerBase
    {
        private readonly IGeneroService generoService;

        public GeneroController(IGeneroService generoService)
        {
            this.generoService = generoService;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
                var generos = await generoService.ObtenerGeneros();
                return Ok(generos);
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
                var genero = await generoService.ObtenerGeneroPorId(id);
                return Ok(genero);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post(CrearGeneroDto crearGeneroDto)
        {
            try
            {
                var genero = await generoService.CrearGenero(crearGeneroDto);
                return StatusCode(201, genero);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpPost("genero-pelicula")]
        public async Task<ActionResult> Put(GeneroPeliculaDto generoPeliculaDto)
        {
            try
            {
                var result = await generoService.AgregarGeneroPelicula(generoPeliculaDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpDelete("genero-pelicula/{idGenero:int}/{idPelicula:int}")]
        public async Task<ActionResult> Delete(int idGenero, int idPelicula)
        {
            try
            {
                var result = await generoService.BorrarGeneroPelicula(idGenero, idPelicula);

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
        public async Task<ActionResult> Put(int id, CrearGeneroDto crearGeneroDto)
        {
            try
            {
                var result = await generoService.ActualizarGenero(id, crearGeneroDto);

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
                var result = await generoService.BorrarGenero(id);

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