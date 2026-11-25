using Forum.Controllers;
using Forum.Models;
using Forum.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Forum.Tests.ControllersTests;

public class UserControllerTests
{
    private Mock<IUserService> _userService;
    private UserController _userController;

    [SetUp]
    public void SetUp()
    {
        _userService = new Mock<IUserService>();
        _userController = new UserController(_userService.Object);
    }

    [Test]
    public void Test_ShouldReturnOkResultWhenUserFoundById()
    {
        //Arrange
        User user = new User()
        {
            Username = "userOne",
            Comments = new List<Comment>(),
            Email = "email@email.com",
            Id = 1, PasswordHash = "hash", Posts = new List<Post>(),
            Role = UserRole.User
        };
        _userService.Setup(u => u.GetById(1)).Returns(user);
        //Act
        var result = _userController.GetById(1);
        //Assert
        Assert.That(result, Is.TypeOf<OkObjectResult>());
        OkObjectResult objectResult = (OkObjectResult)result;
        Assert.That(objectResult.Value, Is.EqualTo(user));
        _userService.Verify(u => u.GetById(1), Times.Once);
    }

    [Test]
    public void Test_ShouldReturnNotFoundResultWhenUserNotFoundById()
    {
        //Arrange
        User? user = null;
        _userService.Setup(u => u.GetById(1)).Returns(user);
        //Act
        var result = _userController.GetById(1);
        //Assert
        Assert.That(result, Is.TypeOf<NotFoundResult>());
    }

    [Test]
    public void Test_ShouldReturnOkResultWhenUserFoundByUsername()
    {
        //Arrange
        User user = new User()
        {
            Username = "userOne",
            Comments = new List<Comment>(),
            Email = "email@email.com",
            Id = 1, PasswordHash = "hash", Posts = new List<Post>(),
            Role = UserRole.User
        };
        _userService.Setup(u => u.GetByUsername("userOne")).Returns(user);
        //Act
        var result = _userController.GetByUsername("userOne");
        //Assert
        Assert.That(result, Is.TypeOf<OkObjectResult>());
        OkObjectResult objectResult = (OkObjectResult)result;
        Assert.That(objectResult.Value, Is.EqualTo(user));
        _userService.Verify(u => u.GetByUsername("userOne"), Times.Once);
    }

    [Test]
    public void Test_ShouldReturnNotFoundResultWhenUserNotFoundByUsername()
    {
        //Arrange
        User? user = null;
        _userService.Setup(u => u.GetByUsername("userOne")).Returns(user);
        //Act
        var result = _userController.GetByUsername("userOne");
        //Assert
        Assert.That(result, Is.TypeOf<NotFoundResult>());
    }

    [Test]
    public void Test_ShouldReturnOkResultWhenAllUserFound()
    {
        //Arrange
        List<User> users = new List<User>()
        {
            new User(),
            new User()
        };
        _userService.Setup(u => u.GetAll()).Returns(users);
        //Act
        var result = _userController.GetAll();
        //Assert
        Assert.That(result, Is.TypeOf<OkObjectResult>());
        OkObjectResult objectResult = (OkObjectResult)result;
        Assert.That(objectResult.Value, Is.EqualTo(users));
        _userService.Verify(u => u.GetAll(), Times.Once);
    }

    [Test]
    public void Test_CreateShouldReturnOkResult()
    {
        //Arrange
        User user = new User()
        {
            Username = "userOne",
            Comments = new List<Comment>(),
            Email = "email@email.com",
            Id = 1, PasswordHash = "hash", Posts = new List<Post>(),
            Role = UserRole.User
        };
        //Act
        var result = _userController.Create(user);
        //Assert
        Assert.That(result, Is.TypeOf<OkResult>());
        _userService.Verify(u => u.Create(user), Times.Once);
    }

    [Test]
    public void Test_UpdateShouldReturnOkResult()
    {
        //Arrange
        User user = new User()
        {
            Username = "userOne",
            Comments = new List<Comment>(),
            Email = "email@email.com",
            Id = 1, PasswordHash = "hash", Posts = new List<Post>(),
            Role = UserRole.User
        };
        //Act
        var result = _userController.Update(user);
        //Assert
        Assert.That(result, Is.TypeOf<OkResult>());
        _userService.Verify(u => u.Update(user), Times.Once);
    }

    [Test]
    public void Test_DeleteShouldReturnOkResult()
    {
        //Arrange
        //Act
        var result = _userController.Delete(1);
        //Assert
        Assert.That(result, Is.TypeOf<OkResult>());
        _userService.Verify(u => u.Delete(1), Times.Once);
    }
}