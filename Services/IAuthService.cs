using Forum.DTOs.Auth;
using Forum.DTOs.Jwt;

namespace Forum.Services;

public interface IAuthService
{
    void Register(RegisterUserDto dto);

    JwtResponseDto Login(LoginUserDto dto);
}