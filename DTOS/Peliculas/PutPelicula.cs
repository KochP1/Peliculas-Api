namespace TestApi.DTOS
{
    public class PutPeliculaDto
    {
        public string Nombre { get; set; } = null!;

        public DateOnly FechaSalida { get; set; }

        public string Estudio { get; set; } = null!;

        public decimal BoxOffice { get; set; }

        public decimal Presupuesto { get; set; }
    }
}