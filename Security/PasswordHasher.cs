using Microsoft.AspNetCore.Identity;
using Forum.Models;

namespace Forum.Security;

public class PasswordHasher : IPasswordHasher
{
    private readonly PasswordHasher<User> _hasher = new();

    public string Hash(string password)
    {
        User user = new();

        return _hasher.HashPassword(user, password);
    }

    public bool Verify(string password, string passwordHash)
    {
        User user = new();

        PasswordVerificationResult result =
            _hasher.VerifyHashedPassword(user, passwordHash, password);

        return result == PasswordVerificationResult.Success;
    }
}