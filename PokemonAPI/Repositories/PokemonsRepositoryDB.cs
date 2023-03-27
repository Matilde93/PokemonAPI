using Microsoft.EntityFrameworkCore;
using PokemonAPI.Contexts;
using PokemonLibrary;
using System.Data.SqlTypes;

namespace PokemonAPI.Repositories
{
    public class PokemonsRepositoryDB : IPokemonsRepository
    {

        private PokemonContext _context;
        
        public PokemonsRepositoryDB(PokemonContext context)
        {
            _context = context;
        } 
        public Pokemon Add(Pokemon newPokemon)
        {
            newPokemon.Id = 0;
            _context.pokemons.Add(newPokemon);
            _context.SaveChanges();
            return newPokemon;
        }

        public Pokemon Delete(int id)
        {
            Pokemon? pokemontoBeDeleted = GetById(id);
            if (pokemontoBeDeleted == null)
            {
                return null;
            }
            _context.pokemons.Remove(pokemontoBeDeleted);
            _context.SaveChanges();
            return pokemontoBeDeleted;
        }

    public List<Pokemon> GetAll(string? namefilter, int? pokedexfilter, int? maxlevelfilter, int? minlevelfilter)
        {
            List<Pokemon> result = new List<Pokemon>(_context.pokemons);

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

        public Pokemon? GetById(int id)
        {
            return _context.pokemons.Find(id);
            
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
            pokemonToBeUpdated.Level= pokemonUpdates.Level;
            pokemonToBeUpdated.Pokedex= pokemonUpdates.Pokedex;

            _context.pokemons.Update(pokemonToBeUpdated);
            _context.SaveChanges();
            return pokemonToBeUpdated;
        }
    }
}
