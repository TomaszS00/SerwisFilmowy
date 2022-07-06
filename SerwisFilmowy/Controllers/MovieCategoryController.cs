using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SerwisFilmowy.Models;
using SerwisFilmowy.Services;

namespace SerwisFilmowy.Controllers;

[Route("api/movies/categories")]
[ApiController]
[Authorize]
public class MovieCategoryController : Controller
{
    private readonly IMovieCategoryService _movieCategoryService;

    public MovieCategoryController(IMovieCategoryService movieCategoryService)
    {
        _movieCategoryService = movieCategoryService;
    }


    [HttpGet]
    public async Task<ActionResult<IEnumerable<MovieCategoryDto>>> GetAll()
    {
        var movieCategories = await _movieCategoryService.GetAll();
        return Ok(movieCategories);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MovieCategoryDto>> GetById([FromRoute] int id)
    {
        var movieCategory = await _movieCategoryService.GetById(id);
        return Ok(movieCategory);
    }

    [HttpPost]
    public async Task<ActionResult<MovieCategoryDto>> Create([FromBody] MovieCategoryViewModel movieCategory)
    {
        await _movieCategoryService.Create(movieCategory);
        return Created($"/api/movies/categories", null);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update([FromRoute] int id, [FromBody] MovieCategoryViewModel movieCategory)
    {
        await _movieCategoryService.Update(id, movieCategory);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete([FromRoute] int id)
    {
        await _movieCategoryService.Remove(id);
        return NoContent();
    }
}
