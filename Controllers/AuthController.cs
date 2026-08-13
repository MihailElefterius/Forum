using Forum.DTOs.Auth;
using Forum.DTOs.Jwt;
using Forum.Services;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPatch("register")]
    public IActionResult Register(RegisterUserDto dto)
    {
        _authService.Register(dto);

        return Ok();
    }

    [HttpPatch("login")]
    public IActionResult Login(LoginUserDto dto)
    {
        JwtResponseDto response = _authService.Login(dto);

        return Ok(response);
    }
}