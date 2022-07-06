using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SerwisFilmowy.Database;
using SerwisFilmowy.Entities;
using SerwisFilmowy.Models;
using SerwisFilmowy.Exceptions;

namespace SerwisFilmowy.Services;

public interface IMovieCategoryService
{
    Task<MovieCategoryDto> GetById(int id);
    Task<IEnumerable<MovieCategoryDto>> GetAll();
    Task Create(MovieCategoryViewModel movieCategoryViewModel);
    Task Update(int id, MovieCategoryViewModel movieCategoryViewModel);
    Task Remove(int id);
}

public class MovieCategoryService : IMovieCategoryService
{
    private readonly MovieDbContext _context;
    private readonly IMapper _mapper;

    public MovieCategoryService(MovieDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }


    public async Task<MovieCategoryDto> GetById(int id)
    {
        var movieCategory = await _context
            .MovieCategories
            .FirstOrDefaultAsync(m => m.Id == id);

        if (movieCategory is null) throw new NotFoundException("Movie Category not found");

        return _mapper.Map<MovieCategoryDto>(movieCategory);
    }

    public async Task<IEnumerable<MovieCategoryDto>> GetAll()
    {
        var movieCategory = await _context
            .MovieCategories
            .ToListAsync();

        var movieCategoryDto = _mapper.Map<List<MovieCategoryDto>>(movieCategory);
        return movieCategoryDto;
    }


    public async Task Create(MovieCategoryViewModel movieCategoryViewModel)
    {
        var category = new MovieCategory()
        {
            Name = movieCategoryViewModel.Name,
        };
        await _context.MovieCategories.AddAsync(category);
        await _context.SaveChangesAsync();
    }
    public async Task Update(int id, MovieCategoryViewModel movieCategoryViewModel)
    {
        var dbCategory = await _context.MovieCategories.FirstOrDefaultAsync(c => c.Id == id);
        if (dbCategory is null) throw new NotFoundException("Movie Category not found");
        dbCategory.Name = movieCategoryViewModel.Name;
        await _context.SaveChangesAsync();
    }

    public async Task Remove(int id)
    {
        var category = await _context.MovieCategories.FirstOrDefaultAsync(c => c.Id == id);
        if (category == null) throw new NotFoundException("Movie Category not found");
        _context.MovieCategories.Remove(category);
        await _context.SaveChangesAsync();
    }
}
