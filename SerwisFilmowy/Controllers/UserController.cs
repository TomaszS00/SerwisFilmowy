using Microsoft.AspNetCore.Mvc;
using SerwisFilmowy.Models;
using SerwisFilmowy.Services;

namespace SerwisFilmowy.Controllers;


[Route("api/user")]
[ApiController]
public class UserController : Controller
{
    private readonly IUserService _userService;


    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<ActionResult> RegisterUser([FromBody] RegisterDto registerDto)
    {
        _userService.Register(registerDto);
        return Ok();
    }

    [HttpPost("login")]
    public ActionResult Login([FromBody] LoginDto dto)
    {
        string token = _userService.GenereteJWT(dto);
        return Ok(token);
    }
}
