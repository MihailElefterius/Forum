using Forum.Models;
using Forum.Security;
using Microsoft.Extensions.Configuration;

namespace Forum.Tests.SecurityTests;

public class JwtServiceTests
{
    private JwtService _jwtService;
    private IConfiguration _configuration;

    [Test]
    public void GenerateToken_ShouldReturnToken()
    {
        //Arrange
        _configuration = new ConfigurationManager().AddInMemoryCollection(new Dictionary<string, string?>()
        {
            ["Jwt:Key"] = "test-token-test-token-test-token-1"
        }).Build();
        _jwtService = new JwtService(_configuration);
        User user = new User
        {
            Id = 1,
            Username = "userOne",
            Role = UserRole.User
        };
        //Act
        string result = _jwtService.GenerateToken(user);
        //Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.Not.Empty);
    }

    [Test]
    public void TestJwtShouldThrowInvalidOperationExceptionWhenNoJwtTokenFound()
    {
        //Arrange
        _configuration = new ConfigurationManager().AddInMemoryCollection(new Dictionary<string, string?>()
        {
            [""] = "test-token-test-token-test-token-1"
        }).Build();
        _jwtService = new JwtService(_configuration);
        User user = new User
        {
            Id = 1,
            Username = "userOne",
            Role = UserRole.User
        };
        //Act && Assert
        InvalidOperationException invalidOperationException =
            Assert.Throws<InvalidOperationException>(() => _jwtService.GenerateToken(user));
        Assert.That(invalidOperationException.Message, Is.EqualTo("JWT key is missing."));
    }
    
}