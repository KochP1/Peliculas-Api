using System;
using System.Collections.Generic;

namespace TestApi.Models;

public partial class Peliculas
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public DateOnly FechaSalida { get; set; }

    public string Estudio { get; set; } = null!;

    public decimal BoxOffice { get; set; }

    public decimal Presupuesto { get; set; }

    public virtual ICollection<ActorPelicula> ActorPeliculas { get; set; } = new List<ActorPelicula>();

    public virtual ICollection<DirectorPelicula> DirectorPeliculas { get; set; } = new List<DirectorPelicula>();

    public virtual ICollection<GeneroPelicula> GeneroPeliculas { get; set; } = new List<GeneroPelicula>();
}
