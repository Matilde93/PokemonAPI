using PokemonLibrary;
namespace PokemonAPI.Repositories
{
    public interface IPokemonsRepository
    {
        Pokemon Add(Pokemon newPokemon);
        Pokemon Delete(int id);
        List<Pokemon> GetAll(string? namefilter, int? pokedexfilter, int? maxlevelfilter, int? minlevelfilter);
        Pokemon? GetById(int id);
        Pokemon? Update(int id, Pokemon pokemonUpdates);
    }
}