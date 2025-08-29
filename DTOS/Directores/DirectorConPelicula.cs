namespace TestApi.DTOS
{
    public class DirectorConPeliculaDto : DirectorDto
    {
        public List<PeliculaDto> Peliculas { get; set; } = [];

    }
}