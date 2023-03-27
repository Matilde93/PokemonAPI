using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokemonLibrary;
using PokemonAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonAPI.Repositories.Tests
{
    [TestClass()]
    public class PokemonsRepositoryTests
    {
        PokemonsRepository repository = new PokemonsRepository();
        Pokemon? testPokemon;
        Pokemon? illegalPokemon;

        [TestInitialize] 
        public void TestInitialize()
        {
            testPokemon = repository.Add(new Pokemon { Id = 5, Name = "Test", Level = 34, Pokedex = 2 });
            illegalPokemon = repository.Add(new Pokemon { Id = 6, Name = "T", Level = 100 , Pokedex = 0 });
        }

        [TestMethod()]
        public void PokemonsRepositoryTest()
        {
            var pokemons = repository.GetAll();
            Assert.AreEqual(6, pokemons.Count());
        }

        [TestMethod()]
        public void GetAllTest()
        {
            List<Pokemon> getAllPokemon = repository.GetAll();
            Assert.IsInstanceOfType(getAllPokemon, typeof(List<Pokemon>));
        }

        [TestMethod()]
        public void AddTest()
        {
            //Pokemon testPokemon = repository.Add(new Pokemon { Id = 5, Name= "Test", Level= 34, Pokedex = 2} );
            string str = testPokemon.ToString();
            Assert.AreEqual("5 Test 34 2", str);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            //Pokemon testPokemon = repository.Add(new Pokemon { Id = 5, Name = "Test", Level = 34, Pokedex = 2 });
            int testId = testPokemon.Id;
            repository.Delete(testId);
            Assert.IsNull(repository.GetById(testId));
        }

        [TestMethod()]
        public void UpdateTest()
        {
           // Pokemon testPokemon = repository.Add(new Pokemon { Id = 5, Name = "Test", Level = 34, Pokedex = 2 });
            Pokemon updatedPokemon = new Pokemon { Id = 465, Name = "nyt navn", Level = 34, Pokedex = 2 };
            Pokemon result = repository.Update(testPokemon.Id, updatedPokemon);
            Assert.AreEqual("5 nyt navn 34 2", result.ToString());
        }

        [TestMethod()]
        public void GetByIdTest()
        {
            Pokemon getPokemon = repository.GetById(1);
            Assert.AreEqual(1, getPokemon.Id);
        }

        [TestMethod()]
        public void ValidateTest()
        {
            Assert.ThrowsException<ArgumentException>(() => illegalPokemon.ValidateName());
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => illegalPokemon.ValidateLevel());
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => illegalPokemon.ValidatePokedex());
        }
    }
}