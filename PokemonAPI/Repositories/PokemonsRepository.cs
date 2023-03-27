using PokemonLibrary;
namespace PokemonAPI.Repositories
{
    public class PokemonsRepository : IPokemonsRepository
    {
        private int _nextID;
        private List<Pokemon> _pokemons;

        public PokemonsRepository()
        {
            _nextID = 1;
            _pokemons = new List<Pokemon>()
            {
                new Pokemon() { Id = _nextID++, Name = "Pikachu", Level = 100, Pokedex = 2},
                new Pokemon() { Id = _nextID++, Name = "Charmander", Level = 80, Pokedex = 12},
                new Pokemon() { Id = _nextID++, Name = "Bulbasaur", Level = 67, Pokedex = 2},
                new Pokemon() { Id = _nextID++, Name = "Eevee", Level = 45, Pokedex = 18},
            };
        }


        //Gør dette til eksamen, lav kopi for at beskytte listen. Der laves en lokal kopi.
        public List<Pokemon> GetAll(string? namefilter, int? pokedexfilter, int? maxlevelfilter, int? minlevelfilter)
        {
            List<Pokemon> result = new List<Pokemon>(_pokemons);

            if (namefilter != null)
            {
                result = result.FindAll(pokemon => pokemon.Name.Contains(namefilter, StringComparison.InvariantCultureIgnoreCase));
            }
            if (pokedexfilter != null)
            {
                result = result.FindAll(pokemon => pokemon.Pokedex == pokedexfilter);
            }
            if (minlevelfilter != null)
            {
                result = result.FindAll(pokemon => pokemon.Level >= minlevelfilter);
            }
            if (maxlevelfilter != null)
            {
                result = result.FindAll(pokemon => pokemon.Level <= maxlevelfilter);
            }
            if (maxlevelfilter != null && minlevelfilter != null)
            {
                result = result.FindAll(pokemon => (pokemon.Level <= maxlevelfilter && pokemon.Level >= minlevelfilter));
            }

            return result;
        }

        public Pokemon Add(Pokemon newPokemon)
        {
            newPokemon.Validate();
            newPokemon.Id = _nextID++;
            _pokemons.Add(newPokemon);
            return newPokemon;
        }

        public Pokemon Delete(int id)
        {
            Pokemon? pokemontoBeDeleted = GetById(id);
            if (pokemontoBeDeleted == null)
            {
                return null;
            }
            _pokemons.Remove(pokemontoBeDeleted);
            return pokemontoBeDeleted;
        }

        public Pokemon? Update(int id, Pokemon pokemonUpdates)
        {
            pokemonUpdates.Validate();
            Pokemon? pokemonToBeUpdated = GetById(id);
            if (pokemonToBeUpdated == null)
            {
                return null;
            }
            pokemonToBeUpdated.Name = pokemonUpdates.Name;
            pokemonToBeUpdated.Level = pokemonUpdates.Level;
            pokemonToBeUpdated.Pokedex = pokemonUpdates.Pokedex;
            return pokemonToBeUpdated;
        }

        public Pokemon? GetById(int id)
        {
            return _pokemons.Find(pokemon => pokemon.Id == id);
        }
    }
}
