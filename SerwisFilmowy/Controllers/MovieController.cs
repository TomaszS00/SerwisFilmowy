using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SerwisFilmowy.Models;
using SerwisFilmowy.Services;

namespace SerwisFilmowy.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/movies")]
    public class MovieController : Controller
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromBody] string name)
        {
            var movies = await _movieService.GetAll(name);
            return Ok(movies);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskById(int id)
        {
            var movie = await _movieService.GetById(id);
            return Ok(movie);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MovieViewModel movie)
        {
            await _movieService.Create(movie);
            return Created($"/api/movies", null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] MovieViewModel movie)
        {
            await _movieService.Update(id, movie);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Remove([FromRoute] int id)
        {
            await _movieService.Remove(id);
            return NoContent();
        }

        [HttpPost("/{id}/rate")]
        public async Task<IActionResult> Rate([FromRoute] int id, [FromBody] MovieRateViewModel rate)
        {
            await _movieService.Rate(id, rate);
            return Ok();
        }
    }
}
