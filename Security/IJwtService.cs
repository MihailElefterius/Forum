using Forum.Models;

namespace Forum.Security;

public interface IJwtService
{
    string GenerateToken(User user);
}