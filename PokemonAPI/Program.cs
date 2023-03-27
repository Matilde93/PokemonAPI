using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PokemonAPI;
using PokemonAPI.Contexts;
using PokemonAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowAll",
    policy =>
    {
        policy
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
    options.AddPolicy(name: "OnlyGET",
    policy =>
    {
        policy
        .AllowAnyOrigin()
        .WithMethods("GET")
        .AllowAnyHeader();
    });
});




builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

bool useSQL = false;
if (useSQL)
{
    var optionsBuilder = new DbContextOptionsBuilder<PokemonContext>();
    optionsBuilder.UseSqlServer(Secrets.ConnectionString);
    PokemonContext context = new PokemonContext(optionsBuilder.Options);
    builder.Services.AddSingleton<IPokemonsRepository>(new PokemonsRepositoryDB(context));

}
else
{
    builder.Services.AddSingleton<IPokemonsRepository>(new PokemonsRepository());
}

var app = builder.Build();

//app.UseSwagger();
//app.UseSwaggerUI();

// Configure the HTTP request pipeline.

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
