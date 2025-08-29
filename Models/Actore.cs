using System;
using System.Collections.Generic;

namespace TestApi.Models;

public partial class Actores
{
    public int Id { get; set; }

    public string Nombres { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public int Edad { get; set; }

    public DateOnly FechaNacimiento { get; set; }

    public virtual ICollection<ActorPelicula> ActorPeliculas { get; set; } = new List<ActorPelicula>();
}
