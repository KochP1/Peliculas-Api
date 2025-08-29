using System;
using System.Collections.Generic;

namespace TestApi.Models;

public partial class GeneroPelicula
{
    public int Id { get; set; }

    public int IdGenero { get; set; }

    public int IdPelicula { get; set; }

    public virtual Generos IdGeneroNavigation { get; set; } = null!;

    public virtual Peliculas IdPeliculaNavigation { get; set; } = null!;
}
