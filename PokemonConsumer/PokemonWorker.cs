using PokemonLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PokemonConsumer
{
    public class PokemonWorker
    {
        public void DoWork()
        {

            IEnumerable<Pokemon>? pokemons = GetAll().Result;
            foreach (var pokemon in pokemons)
            {
                Console.WriteLine(pokemon);
            }
            Console.WriteLine();

            //Pokemon addPokemon = new Pokemon() { Id = 5, Name = "Test", Level = 80, Pokedex = 22 };
            //addPokemon = Post(addPokemon).Result;
            //Console.WriteLine(addPokemon); 


            Pokemon getByIdPokemon = new Pokemon() { Id = 5, Name = "Test", Level = 80, Pokedex = 22 };
            getByIdPokemon = GetById(getByIdPokemon.Id).Result;
            Console.WriteLine(getByIdPokemon);


        }

        public async Task<IEnumerable<Pokemon>?> GetAll()
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync($"https://pokemonapi20230221123533.azurewebsites.net/api/pokemons");
                IEnumerable<Pokemon>? list = await response.Content.ReadFromJsonAsync<IEnumerable<Pokemon>>();
                return list;
            }
        }

        public async Task<Pokemon>? GetById(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync($"https://pokemonapi20230221123533.azurewebsites.net/api/pokemons/{id}");
                Pokemon? pokemon = await response.Content.ReadFromJsonAsync<Pokemon>();
                return pokemon;
            }
        }

        public async Task<Pokemon>? Delete(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.DeleteAsync($"https://pokemonapi20230221123533.azurewebsites.net/api/pokemons/{id}");
                Pokemon? pokemon = await response.Content.ReadFromJsonAsync<Pokemon>();
                return pokemon;
            }
        }



        public async Task<Pokemon>? Post(Pokemon newPokemon)
        {
            
            using (HttpClient client = new HttpClient())
            {
                JsonContent content = JsonContent.Create(newPokemon);
                HttpResponseMessage response = await client.PostAsync($"https://pokemonapi20230221123533.azurewebsites.net/api/pokemons", content);
                if (response.IsSuccessStatusCode)
                {
                    Pokemon? pokemon = await response.Content.ReadFromJsonAsync<Pokemon>();
                    return pokemon;
                }
                else
                {
                    throw new Exception();
                }
            }
        }
    }
}
