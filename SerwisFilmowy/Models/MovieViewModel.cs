using System.ComponentModel.DataAnnotations;
using SerwisFilmowy.Entities;

namespace SerwisFilmowy.Models;

public class MovieViewModel
{
    [Display(Name = "Title")]
    [Required(ErrorMessage = "Title is required")]
    public string Title { get; set; }
    [Display(Name = "Producer")]
    [Required(ErrorMessage = "Producer is required")]
    public string? Producer { get; set; }
    [Display(Name = "Actors")]
    [Required(ErrorMessage = "Actors is required")]
    public string? Actors { get; set; }
    [Display(Name = "Description")]
    [Required(ErrorMessage = "Description is required")]
    public string? Description { get; set; }
    [Display(Name = "Image")]
    [Required(ErrorMessage = "Image is required")]
    public string? ImageURL { get; set; }
    [Display(Name = "Movie Category")]
    [Range(1, int.MaxValue, ErrorMessage = "Movie Category is required")]
    public int? MovieCategoryId { get; set; }
}