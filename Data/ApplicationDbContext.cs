using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TestApi.Models;

namespace TestApi.Data;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ActorPelicula> ActorPeliculas { get; set; }

    public virtual DbSet<Actores> Actores { get; set; }

    public virtual DbSet<DirectorPelicula> DirectorPeliculas { get; set; }

    public virtual DbSet<Directores> Directores { get; set; }

    public virtual DbSet<Generos> Generos { get; set; }

    public virtual DbSet<GeneroPelicula> GeneroPeliculas { get; set; }

    public virtual DbSet<Peliculas> Peliculas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-S5Q2S88; Database=Peliculas; Trusted_Connection=True; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ActorPelicula>(entity =>
        {
            entity.HasKey(e => e.IdActor);

            entity.ToTable("ActorPelicula");

            entity.Property(e => e.IdActor).ValueGeneratedNever();

            entity.HasOne(d => d.IdActorNavigation).WithOne(p => p.ActorPelicula)
                .HasForeignKey<ActorPelicula>(d => d.IdActor)
                .HasConstraintName("FK_ActorPelicula_Actores");

            entity.HasOne(d => d.IdPeliculaNavigation).WithMany(p => p.ActorPeliculas)
                .HasForeignKey(d => d.IdPelicula)
                .HasConstraintName("FK_ActorPelicula_Peliculas");
        });

        modelBuilder.Entity<Actores>(entity =>
        {
            entity.Property(e => e.Apellidos).HasMaxLength(50);
            entity.Property(e => e.Nombres).HasMaxLength(50);
        });

        modelBuilder.Entity<DirectorPelicula>(entity =>
        {
            entity.HasKey(e => e.IdDirector);

            entity.ToTable("DirectorPelicula");

            entity.Property(e => e.IdDirector).ValueGeneratedNever();

            entity.HasOne(d => d.IdDirectorNavigation).WithOne(p => p.DirectorPelicula)
                .HasForeignKey<DirectorPelicula>(d => d.IdDirector)
                .HasConstraintName("FK_DirectorPelicula_Directores");

            entity.HasOne(d => d.IdPeliculaNavigation).WithMany(p => p.DirectorPeliculas)
                .HasForeignKey(d => d.IdPelicula)
                .HasConstraintName("FK_DirectorPelicula_Peliculas");
        });

        modelBuilder.Entity<Directores>(entity =>
        {
            entity.Property(e => e.Apellidos).HasMaxLength(50);
            entity.Property(e => e.Nombres).HasMaxLength(50);
        });

        modelBuilder.Entity<Generos>(entity =>
        {
            entity.Property(e => e.Genero1)
                .HasMaxLength(30)
                .HasColumnName("Genero");
        });

        modelBuilder.Entity<GeneroPelicula>(entity =>
        {
            entity.HasKey(e => e.IdGenero);

            entity.ToTable("GeneroPelicula");

            entity.Property(e => e.IdGenero).ValueGeneratedNever();

            entity.HasOne(d => d.IdGeneroNavigation).WithOne(p => p.GeneroPelicula)
                .HasForeignKey<GeneroPelicula>(d => d.IdGenero)
                .HasConstraintName("FK_GeneroPelicula_Generos");

            entity.HasOne(d => d.IdPeliculaNavigation).WithMany(p => p.GeneroPeliculas)
                .HasForeignKey(d => d.IdPelicula)
                .HasConstraintName("FK_GeneroPelicula_Peliculas");
        });

        modelBuilder.Entity<Peliculas>(entity =>
        {
            entity.Property(e => e.BoxOffice).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Estudio).HasMaxLength(30);
            entity.Property(e => e.Nombre).HasMaxLength(30);
            entity.Property(e => e.Presupuesto).HasColumnType("decimal(18, 0)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
