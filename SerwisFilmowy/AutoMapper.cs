using AutoMapper;
using SerwisFilmowy.Entities;
using SerwisFilmowy.Models;

namespace SerwisFilmowy;

public class AutoMapper : Profile
{
    public AutoMapper()
    {
        CreateMap<Movie, MovieViewModel>();
        CreateMap<Movie, MovieDto>();
        CreateMap<MovieCategory, MovieCategoryDto>();
        CreateMap<MovieCategory, MovieCategoryViewModel>();

    }
}
