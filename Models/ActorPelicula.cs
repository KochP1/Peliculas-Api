using System;
using System.Collections.Generic;

namespace TestApi.Models;

public partial class ActorPelicula
{
    public int Id { get; set; }

    public int IdActor { get; set; }

    public int IdPelicula { get; set; }

    public virtual Actores IdActorNavigation { get; set; } = null!;

    public virtual Peliculas IdPeliculaNavigation { get; set; } = null!;
}
