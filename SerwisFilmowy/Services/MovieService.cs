﻿using System.Security.Claims;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SerwisFilmowy.Database;
using SerwisFilmowy.Entities;
using SerwisFilmowy.Models;
using SerwisFilmowy.Exceptions;

namespace SerwisFilmowy.Services;

public interface IMovieService
{
    Task<IEnumerable<MovieDto>> GetAll(string name);
    Task<MovieDto> GetById(int id);
    Task Create(MovieViewModel movieViewModel);
    Task Remove(int id);
    Task Update(int id, MovieViewModel movieViewModel);
    Task Rate(int id, MovieRate rate);
    Task Rate(int id, MovieRateViewModel rate);
}

public class MovieService : IMovieService
{
    private readonly MovieDbContext _context;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _contextAccessor;

    public MovieService(MovieDbContext context, IMapper mapper, IHttpContextAccessor contextAccessor)
    {
        _context = context;
        _mapper = mapper;
        _contextAccessor = contextAccessor;
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
        if (movie == null) throw new NotFoundException("Movie not found");
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
        if (movie == null) throw new NotFoundException("Movie not found");
        _context.Movies.Remove(movie);
        await _context.SaveChangesAsync();
    }

    public async Task Update(int id, MovieViewModel movieViewModel)
    {
        var newMovie = await _context.Movies.FirstOrDefaultAsync(t => t.Id == id);
        if (newMovie == null) throw new NotFoundException("Movie not found");

        newMovie.Title = movieViewModel.Title;
        newMovie.Producer = movieViewModel.Producer;
        newMovie.Actors = movieViewModel.Actors;
        newMovie.Description = movieViewModel.Description;
        newMovie.ImageURL = movieViewModel.ImageURL;
        newMovie.MovieCategoryId = movieViewModel.MovieCategoryId;

        await _context.SaveChangesAsync();
    }

    public async Task Rate(int id, MovieRateViewModel rate)
    {
        var userId = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
        var movie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == id);
        if (movie == null) throw new NotFoundException("Movie not found");
        MovieRate movieRate = new MovieRate()
        {
            MovieId = id,
            UserId = userId,
            Rate = rate.Rate

        };
        _context.MovieRates.Add(movieRate);
        await _context.SaveChangesAsync();
    }

    public Task Rate(int id, MovieRate rate)
    {
        throw new NotImplementedException();
    }

    public ClaimsPrincipal User => _contextAccessor.HttpContext?.User;
}
