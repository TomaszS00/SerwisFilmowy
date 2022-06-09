using SerwisFilmowy.Entities;

namespace SerwisFilmowy.Models;

public class MovieDto
{
    public string Title { get; set; }
    public string? Producer { get; set; }
    public string? Actors { get; set; }
    public string? Description { get; set; }
    public string? ImageURL { get; set; }
    public MovieCategory? MovieCategory { get; set; }
    public List<MovieRate>? Rates { get; set; }
}
