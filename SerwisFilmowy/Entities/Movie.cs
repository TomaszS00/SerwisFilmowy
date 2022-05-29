using System.ComponentModel.DataAnnotations;

namespace SerwisFilmowy.Entities;

public class Movie
{
    [Key]
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Producer { get; set; }
    public string? Actors { get; set; }
    public string? Description { get; set; }
    public string? ImageURL { get; set; }
    public int? MovieCategoryId { get; set; }
    public MovieCategory? MovieCategory { get; set; }
    public List<MovieRate>? Rates { get; set; }
}