using Forum.Controllers;
using Forum.DTOs.Auth;
using Forum.DTOs.Jwt;
using Forum.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Forum.Tests.ControllersTests;

public class AuthControllerTests
{
    private Mock<IAuthService> _authService;
    private AuthController _authController;

    [SetUp]
    public void SetUp()
    {
        _authService = new Mock<IAuthService>();
        _authController = new AuthController(_authService.Object);
    }

    [Test]
    public void Test_Register_ShouldReturnStatusOk()
    {
        //Arrange
        RegisterUserDto dto = new RegisterUserDto()
        {
            Username = "userOne",
            Email = "email@email.com",
            Password = "pass1234"
        };
        //Act
        var result = _authController.Register(dto);
        //Assert
        _authService.Verify(a => a.Register(dto), Times.Once);
        Assert.That(result, Is.TypeOf<OkResult>());
    }

    [Test]
    public void Test_Login_ShouldReturnStatusOk()
    {
        //Arrange
        LoginUserDto dto = new LoginUserDto()
        {
            Username = "userOne",
            Password = "pass1234"
        };
        JwtResponseDto responseDto = new JwtResponseDto()
        {
            Token = "token"
        };
        _authService.Setup(a => a.Login(dto)).Returns(responseDto);
        //Act
        var result = _authController.Login(dto);
        //Assert
        _authService.Verify(a => a.Login(dto),Times.Once());
        Assert.That(result, Is.TypeOf<OkObjectResult>());
        OkObjectResult objectResult = (OkObjectResult)result;
        Assert.That(objectResult.Value,Is.EqualTo(responseDto));
    }
}