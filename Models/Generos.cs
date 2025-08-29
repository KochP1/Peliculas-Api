using System;
using System.Collections.Generic;

namespace TestApi.Models;

public partial class Generos
{
    public int Id { get; set; }

    public string Genero1 { get; set; } = null!;

    public virtual ICollection<GeneroPelicula> GeneroPeliculas { get; set; } = new List<GeneroPelicula>();
}
