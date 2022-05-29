﻿using System.ComponentModel.DataAnnotations;

namespace SerwisFilmowy.Entities;

public class MovieRate
{
    [Key]
    public int Id { get; set; }
    public int? MovieId { get; set; }
    public Movie? Movie { get; set; }
    public int? UserId { get; set; }
    public User? User { get; set; }
    public int? Rate { get; set; }
}