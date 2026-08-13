using Forum.DTOs.Auth;
using Forum.DTOs.Jwt;
using Forum.Exceptions;
using Forum.Models;
using Forum.Repositories;
using Forum.Security;

namespace Forum.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtService _jwtService;

    public AuthService(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IJwtService jwtService)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtService = jwtService;
    }

    public void Register(RegisterUserDto dto)
    {
        if (_userRepository.ExistsByUsername(dto.Username))
        {
            throw new InvalidOperationException("Username already exists.");
        }

        if (_userRepository.ExistsByEmail(dto.Email))
        {
            throw new InvalidOperationException("Email already exists.");
        }

        User user = new()
        {
            Username = dto.Username,
            Email = dto.Email,
            PasswordHash = _passwordHasher.Hash(dto.Password),
            Role = UserRole.User
        };

        _userRepository.Add(user);
    }

    public JwtResponseDto Login(LoginUserDto dto)
    {
        User? user = _userRepository.GetByUsername(dto.Username);

        if (user == null)
        {
            throw new AuthenticationException("Invalid username or password.");
        }

        bool validPassword =
            _passwordHasher.Verify(dto.Password, user.PasswordHash);

        if (!validPassword)
        {
            throw new AuthenticationException("Invalid username or password.");
        }

        string token = _jwtService.GenerateToken(user);

        return new JwtResponseDto
        {
            Token = token
        };
    }
}