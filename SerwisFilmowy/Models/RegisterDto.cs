using SerwisFilmowy.Entities;

namespace SerwisFilmowy.Models;

public class RegisterDto
{
    public string Email { get; set; }
    public string Password { get; set; }
    public int RoleId { get; set; }
}
