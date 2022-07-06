using System.ComponentModel.DataAnnotations;

namespace SerwisFilmowy.Entities;

public class MovieRate
{
    [Key]
    public int Id { get; set; }
    public int? MovieId { get; set; }
    public int? UserId { get; set; }
    public int? Rate { get; set; }
}

