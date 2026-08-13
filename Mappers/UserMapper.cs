using Forum.DTOs.User;
using Forum.Models;

namespace Forum.Mappers;

public static class UserMapper
{
    public static UserResponseDto ToDto(User user)
    {
        return new UserResponseDto
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            Role = user.Role
        };
    }
}