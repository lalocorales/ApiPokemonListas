using ApiPokemonListas.Models;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiPokemonListas.Controllers
{
    [ApiController]
    [Authorize(Roles = "Admin")]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/pokemons")]
    public class PokemonV2Controller : ControllerBase
    {
        private static List<Pokemon> pokemons = new()
        {
            new Pokemon { Id = 1, Name = "Pikachu", Type = "Electric" },
            new Pokemon { Id = 2, Name = "Charmander", Type = "Fire" },
            new Pokemon { Id = 3, Name = "Squirtle", Type = "Water" }
        };

        [HttpGet]
        public ActionResult<IEnumerable<object>> GetAll()
        {
            // Nombre en mayúsculas (para distinguir v2)
            var result = pokemons.Select(p => new
            {
                p.Id,
                Name = p.Name.ToUpper(),
                p.Type
            });

            return Ok(result);
        }
    }
}

