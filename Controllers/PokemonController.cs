using ApiPokemonListas.Models;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.ConstrainedExecution;

namespace ApiPokemonListas.Controllers
{

    //dotnet dev-certs https --trust

    [ApiController]
    [Route("api/pokemons")]
    public class PokemonController : ControllerBase
    {
        private static List<Pokemon> pokemons = new List<Pokemon>
    {
        new Pokemon { Id = 1, Name = "Pikachu", Type = "Electric" },
        new Pokemon { Id = 2, Name = "Charmander", Type = "Fire" },
        new Pokemon { Id = 3, Name = "Squirtle", Type = "Water" }
    };

        [HttpGet]
        public ActionResult<IEnumerable<Pokemon>> GetAll() => Ok(pokemons);

        [HttpGet("{id}")]
        public ActionResult<Pokemon> GetById(int id)
        {
            var pokemon = pokemons.FirstOrDefault(p => p.Id == id);
            return pokemon != null ? Ok(pokemon) : NotFound();
        }

        [HttpPost]
        public ActionResult<Pokemon> Create(Pokemon pokemon)
        {
            pokemon.Id = pokemons.Any() ? pokemons.Max(p => p.Id) + 1 : 1;
            pokemons.Add(pokemon);
            return CreatedAtAction(nameof(GetById), new { id = pokemon.Id }, pokemon);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Pokemon updatedPokemon)
        {
            var pokemon = pokemons.FirstOrDefault(p => p.Id == id);
            if (pokemon == null) return NotFound();

            pokemon.Name = updatedPokemon.Name;
            pokemon.Type = updatedPokemon.Type;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var pokemon = pokemons.FirstOrDefault(p => p.Id == id);
            if (pokemon == null) return NotFound();

            pokemons.Remove(pokemon);
            return NoContent();
        }
    }
}
