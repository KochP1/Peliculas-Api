using TestApi.Models;

namespace TestApi.DTOS
{
    public class PeliculaCollectionDto
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public DateOnly FechaSalida { get; set; }

        public string Estudio { get; set; } = null!;

        public decimal BoxOffice { get; set; }

        public decimal Presupuesto { get; set; }
        public List<ActorDto> Actores { get; set; } = [];
        public List<DirectorDto> Directores { get; set; } = [];
        public List<GeneroDto> Generos { get; set; } = [];
    }
}