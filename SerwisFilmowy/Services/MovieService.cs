using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SerwisFilmowy.Database;
using SerwisFilmowy.Entities;
using SerwisFilmowy.Models;

namespace SerwisFilmowy.Services;

public class MovieService
{
    private readonly MovieDbContext _context;
    private readonly IMapper _mapper;

    public MovieService(MovieDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }


    public async Task<IEnumerable<MovieDto>> GetAll(string name)
    {
        IQueryable<Movie> movieQuery = _context.Movies
            .Include(m => m.MovieCategory)
            .Include(m => m.Rates);
        if (!string.IsNullOrEmpty(name))
        {
            movieQuery = movieQuery.Where(m => m.Title.Contains(name));
        }
        var movies = await movieQuery.ToListAsync();
        var movieDto = _mapper.Map<IEnumerable<MovieDto>>(movies);
        return movieDto;
    }

    public async Task<MovieDto> GetById(int id)
    {
        var movie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == id);
        // ToDo exception
        if (movie == null) throw new Exception("Movie not found");
        return _mapper.Map<MovieDto>(movie);
    }


    public async Task Create(MovieViewModel movieViewModel)
    {
        var movie = new Movie()
        {
            Title = movieViewModel.Title,
            Producer = movieViewModel.Producer,
            Actors = movieViewModel.Actors,
            Description = movieViewModel.Description,
            ImageURL = movieViewModel.ImageURL,
            MovieCategoryId = movieViewModel.MovieCategoryId
        };
        await _context.Movies.AddAsync(movie);
        await _context.SaveChangesAsync();
    }

    public async Task Remove(int id)
    {
        var movie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == id);
        // ToDo exception
        if (movie == null) throw new Exception("Movie not found");
        _context.Movies.Remove(movie);
        await _context.SaveChangesAsync();
    }

    public async Task Update(int id, MovieViewModel movieViewModel)
    {
        var newMovie = await _context.Movies.FirstOrDefaultAsync(t => t.Id == id);
        // ToDo exception
        if (newMovie == null) throw new Exception("Movie not found");

        newMovie.Title = movieViewModel.Title;
        newMovie.Producer = movieViewModel.Producer;
        newMovie.Actors = movieViewModel.Actors;
        newMovie.Description = movieViewModel.Description;
        newMovie.ImageURL = movieViewModel.ImageURL;
        newMovie.MovieCategoryId = movieViewModel.MovieCategoryId;

        await _context.SaveChangesAsync();
    }

    public async Task Rate(int id, MovieRate rate)
    {
        var movie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == id);
        // ToDo exception
        if (movie == null) throw new Exception("Movie not found");
        movie.Rates.Add(rate);
        await _context.SaveChangesAsync();
    }
}
