using Microsoft.AspNetCore.Mvc;
using PokemonAPI.Repositories;
using PokemonLibrary;
using Microsoft.AspNetCore.Cors;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PokemonAPI.Controllers
{
    [Route("api/[controller]")]//URI - Systemet sætter Pokemons ind i stedet for controller. Derfor navngivning af controlleren er vigtig. 
    [ApiController]
    public class PokemonsController : ControllerBase
    {
        private IPokemonsRepository _repository;

        public PokemonsController(IPokemonsRepository repository)
        {
            _repository = repository;
        }


        // GET: api/<PokemonsController>  Skrives således i url'en api/pokemons?namefilter=har
        [EnableCors("AllowAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet]
        public ActionResult<IEnumerable<Pokemon>> Get([FromQuery] string? namefilter, [FromQuery] int? pokedexfilter,
            [FromQuery] int? maxlevelfilter, [FromQuery] int? minlevelfilter)
        {
            List<Pokemon> pokemons = _repository.GetAll(namefilter, pokedexfilter, maxlevelfilter, minlevelfilter);
            if (pokemons.Count < 1)
            {
                return NoContent(); //Not Found er også ok
            }
            Response.Headers.Add("TotalAmount", "" + pokemons.Count());
            return Ok(pokemons);
        }

        // GET api/<PokemonsController>/5
        [EnableCors("AllowAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public ActionResult<Pokemon> Get(int id)
        {
            Pokemon? foundPokemon = _repository.GetById(id);
            if (foundPokemon == null)
            {
                return NotFound();
            }
            return Ok(foundPokemon);
        }

        // POST api/<PokemonsController>
        [EnableCors("AllowAll")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public ActionResult<Pokemon> Post([FromBody] Pokemon newPokemon)
        {
            try
            {
                Pokemon createdPokemon = _repository.Add(newPokemon);
                return Created($"api/pokemons/{createdPokemon.Id}", newPokemon);//Man behøver egentlig kun at skrive /{createdPokemon.Id} - dvs man kan droppe det første af URI'en
            }
            catch(ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<PokemonsController>/5
        [EnableCors("AllowAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]
        public ActionResult <Pokemon> Put(int id, [FromBody] Pokemon value)
        {
            try
            {
                Pokemon? updatedPokemon = _repository.Update(id, value);
                if (updatedPokemon == null)
                {
                    return NotFound();  
                }
                return Ok(updatedPokemon);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // DELETE api/<PokemonsController>/5
        [EnableCors("AllowAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public ActionResult<Pokemon> Delete(int id)
        {
            Pokemon? deletedPokemon = _repository.Delete(id);
            if (deletedPokemon == null)
            {
                return NotFound();
            }
            return Ok(deletedPokemon);
        }

    }
}
