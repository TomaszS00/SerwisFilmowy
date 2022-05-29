using System.ComponentModel.DataAnnotations;

namespace SerwisFilmowy.Entities;

public class Role
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
}
