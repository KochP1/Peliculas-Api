using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using TestApi.Data;
using TestApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var origenesPermitidos = builder.Configuration.GetSection("OrígenesPermitidos").Get<string[]>();
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("PermitirTodo", opcionesCors =>
    {
        // withOrigins(origenesPermitidos)
        opcionesCors.WithOrigins(origenesPermitidos!)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .WithExposedHeaders("cantidad-total-registros");
    });
});


builder.Services.AddControllers().AddJsonOptions(opciones => opciones.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddScoped<IActorService, ActorService>();
builder.Services.AddScoped<IDirectorService, DirectorService>();
builder.Services.AddScoped<IGeneroService, GeneroService>();
builder.Services.AddScoped<IPeliculaService, PeliculaService>();

builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();
app.Run();