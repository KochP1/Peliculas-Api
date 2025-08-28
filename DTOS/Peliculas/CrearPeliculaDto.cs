namespace TestApi.DTOS
{
    public class CrearPeliculaDto
    {
        public string Nombre { get; set; } = null!;

        public DateOnly FechaSalida { get; set; }

        public string Estudio { get; set; } = null!;

        public decimal BoxOffice { get; set; }

        public decimal Presupuesto { get; set; }
        public List<int> Actores { get; set; } = [];
        public List<int> Directores { get; set; } = [];
        public List<int> Generos { get; set; } = [];
    }
}