namespace TestApi.DTOS
{
    public class CrearDirectorDto
    {
        public string Nombres { get; set; } = null!;

        public string Apellidos { get; set; } = null!;

        public int Edad { get; set; }

        public DateOnly FechaNacimiento { get; set; }

    }
}