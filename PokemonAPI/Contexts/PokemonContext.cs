using Microsoft.EntityFrameworkCore;
using PokemonLibrary;

namespace PokemonAPI.Contexts
{
    public class PokemonContext : DbContext  
    {

        public PokemonContext(DbContextOptions<PokemonContext> options ) : base( options ) { }

        public DbSet<Pokemon> pokemons { get; set; }

             


    }
}
