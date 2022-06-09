using Microsoft.AspNetCore.Identity;
using SerwisFilmowy.Database;
using SerwisFilmowy.Entities;
using SerwisFilmowy.Models;

namespace SerwisFilmowy.Services;

public interface IUserService
{
    void Register(RegisterDto registerDto);
    void Login(LoginDto loginDto);
}

public class UserService : IUserService
{
    private readonly MovieDbContext _context;
    private readonly IPasswordHasher<User> _passwordHasher;


    public UserService(MovieDbContext context, IPasswordHasher<User> passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }


    public void Register(RegisterDto registerDto)
    {
        var newUser = new User()
        {
            Email = registerDto.Email,
            RoleId = registerDto.RoleId,
        };
        var hashedPassword = _passwordHasher.HashPassword(newUser, registerDto.Password);
        newUser.PasswordHash = hashedPassword;
        _context.Users.Add(newUser);
        _context.SaveChanges();
    }

    // To do after jwt tokens
    public void Login(LoginDto loginDto)
    {

    }
}