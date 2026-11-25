using Forum.DTOs.Auth;
using Forum.DTOs.Jwt;
using Forum.Exceptions;
using Forum.Models;
using Forum.Repositories;
using Forum.Security;
using Forum.Services;
using Moq;

namespace Forum.Tests;

public class Tests
{
    private Mock<IUserRepository> _userRepo;
    private Mock<IPasswordHasher> _passHasher;
    private Mock<IJwtService> _jwtService;
    private AuthService _authService;

    [SetUp]
    public void Setup()
    {
        _userRepo = new Mock<IUserRepository>();
        _passHasher = new Mock<IPasswordHasher>();
        _jwtService = new Mock<IJwtService>();
        _authService = new AuthService(_userRepo.Object, _passHasher.Object, _jwtService.Object);
    }

    [Test]
    public void Test_ShouldAddUserToRepository()
    {
        //Arrange
        RegisterUserDto registerUserDto = new RegisterUserDto()
        {
            Email = "email@abc.com",
            Password = "pass1234!",
            Username = "user1"
        };
        _userRepo.Setup(u => u.ExistsByUsername("user1")).Returns(false);
        _userRepo.Setup(u => u.ExistsByEmail("email@abc.com")).Returns(false);

        _passHasher.Setup(p => p.Hash("pass1234!")).Returns("Hashed-pass");
        //Act
        _authService.Register(registerUserDto);
        //Assert
        _userRepo.Verify(u => u.Add(
            It.Is<User>(user => user.Username == "user1"
                                && user.Email == "email@abc.com"
                                && user.PasswordHash == "Hashed-pass"
                                && user.Role == UserRole.User)), Times.Once);
    }

    [Test]
    public void Test_ShouldThrow_InvalidOperationException_WhenUsernameExists()
    {
        //Arrange
        RegisterUserDto registerUserDto = new RegisterUserDto()
        {
            Email = "email@abc.com",
            Password = "pass1234!",
            Username = "user1"
        };
        _userRepo.Setup(u => u.ExistsByUsername("user1")).Returns(true);
        //Act and Assert
        InvalidOperationException exception =
            Assert.Throws<InvalidOperationException>(() => _authService.Register(registerUserDto));
        Assert.That(exception.Message, Is.EqualTo("Username already exists."));
    }

    [Test]
    public void Test_ShouldThrow_InvalidOperationException_WhenEmailExists()
    {
        //Arrange
        RegisterUserDto registerUserDto = new RegisterUserDto()
        {
            Email = "email@abc.com",
            Password = "pass1234!",
            Username = "user1"
        };
        _userRepo.Setup(u => u.ExistsByEmail("email@abc.com")).Returns(true);
        //Act and Assert
        InvalidOperationException exception =
            Assert.Throws<InvalidOperationException>(() => _authService.Register(registerUserDto));
        Assert.That(exception.Message, Is.EqualTo("Email already exists."));
    }

    [Test]
    public void Test_ShouldThrow_AuthenticationException_WhenUsernameOrPasswordAreInvalidOrEmpty()
    {
        //Arrange
        LoginUserDto? loginUserDto = new LoginUserDto()
        {
            Password = "pass1234!",
            Username = "user1"
        };
        User? user = null;
        _userRepo.Setup(u => u.GetByUsername("user1")).Returns(user);
        //Act & Assert
        AuthenticationException authenticationException =
            Assert.Throws<AuthenticationException>(() => _authService.Login(loginUserDto));
        Assert.That(authenticationException.Message, Is.EqualTo("Invalid username or password."));
    }

    [Test]
    public void Test_ShouldThrow_AuthenticationException_WhenPasswordIsInvalid()
    {
        //Arrange
        LoginUserDto? loginUserDto = new LoginUserDto()
        {
            Password = "pass1234!",
            Username = "user1"
        };
        User user = new User()
        {
            Comments = new List<Comment>() { new Comment(), new Comment() },
            Email = "email@abc.com",
            Id = 1,
            PasswordHash = "Hashed",
            Posts = new List<Post>() { new Post(), new Post() },
            Role = UserRole.User,
            Username = "user1"
        };
        _userRepo.Setup(u => u.GetByUsername("user1")).Returns(user);
        bool validPassword = false;
        _passHasher.Setup(p => p.Verify(loginUserDto.Password, user.PasswordHash)).Returns(validPassword);
        //Act & Assert
        AuthenticationException authenticationException =
            Assert.Throws<AuthenticationException>(() => _authService.Login(loginUserDto));
        Assert.That(authenticationException.Message, Is.EqualTo("Invalid username or password."));
    }

    [Test]
    public void Test_ShouldReturnJwtToken()
    {
        //Arrange
        LoginUserDto loginUserDto = new LoginUserDto()
        {
            Password = "pass1234!",
            Username = "user1"
        };
        User user = new User()
        {
            Comments = new List<Comment>() { new Comment(), new Comment() },
            Email = "email@abc.com",
            Id = 1,
            PasswordHash = "pass1234!",
            Posts = new List<Post>() { new Post(), new Post() },
            Role = UserRole.User,
            Username = "user1"
        };
        string token = "token";
        bool validPass = true;
        _userRepo.Setup(u => u.GetByUsername("user1")).Returns(user);
        _jwtService.Setup(j => j.GenerateToken(user)).Returns(token);
        _passHasher.Setup(p => p.Verify(loginUserDto.Password, user.PasswordHash)).Returns(validPass);
        //Act 
        JwtResponseDto result = _authService.Login(loginUserDto);
        //Assert
        Assert.That(result.Token, Is.EqualTo(token));
    }
}