using System.ComponentModel.DataAnnotations;

namespace SerwisFilmowy.Models;

public class MovieCategoryViewModel
{
    [Required]
    [Display(Name = "Movie Category")]
    public string Name { get; set; }
}
