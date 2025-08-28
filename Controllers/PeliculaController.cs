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
            var pelicula = await peliculaService.ObtenerPeliculasPorId(id);
            return Ok(pelicula);
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

/*
{
  "nombre": "Avengers Infinity War",
  "fechaSalida": "2025-08-28",
  "estudio": "Marvel",
  "boxOffice": 1000000000,
  "presupuesto": 500000000,
  "actores": [
    1, 4
  ],
  "directores": [
    1
  ],
  "generos": [
    1, 2
  ]
}
*/