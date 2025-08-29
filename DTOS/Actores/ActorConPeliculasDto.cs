namespace TestApi.DTOS
{
    public class ActorConPeliculasDto
    {
        public int Id { get; set; }

        public string Nombres { get; set; } = null!;

        public string Apellidos { get; set; } = null!;

        public int Edad { get; set; }

        public DateOnly FechaNacimiento { get; set; }
        
        public List<PeliculaDto> Peliculas { get; set; } = [];
    }
}