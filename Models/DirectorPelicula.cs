using System;
using System.Collections.Generic;

namespace TestApi.Models;

public partial class DirectorPelicula
{
    public int Id { get; set; }

    public int IdDirector { get; set; }

    public int IdPelicula { get; set; }

    public virtual Directores IdDirectorNavigation { get; set; } = null!;

    public virtual Peliculas IdPeliculaNavigation { get; set; } = null!;
}
