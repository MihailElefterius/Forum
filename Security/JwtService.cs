using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Forum.Models;
using Microsoft.IdentityModel.Tokens;

namespace Forum.Security;

public class JwtService : IJwtService
{
    private readonly IConfiguration _configuration;

    public JwtService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(User user)
    {
        string secretKey = _configuration["Jwt:Key"]
                           ?? throw new InvalidOperationException("JWT key is missing.");

        byte[] keyBytes = Encoding.UTF8.GetBytes(secretKey);

        SymmetricSecurityKey key = new(keyBytes);

        SigningCredentials signingCredentials =
            new(key, SecurityAlgorithms.HmacSha256);

        List<Claim> claims = new()
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };

        JwtSecurityToken token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: signingCredentials);

        JwtSecurityTokenHandler handler = new();

        return handler.WriteToken(token);
    }
}