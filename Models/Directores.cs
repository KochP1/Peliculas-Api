using System;
using System.Collections.Generic;

namespace TestApi.Models;

public partial class Directores
{
    public int Id { get; set; }

    public string Nombres { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public int Edad { get; set; }

    public DateOnly FechaNacimiento { get; set; }

    public virtual ICollection<DirectorPelicula> DirectorPeliculas { get; set; } = new List<DirectorPelicula>();
}
