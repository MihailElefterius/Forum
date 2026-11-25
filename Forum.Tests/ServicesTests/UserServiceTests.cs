using Forum.Models;
using Forum.Repositories;
using Forum.Services;
using Moq;

namespace Forum.Tests;

public class UserServiceTests
{
    private Mock<IUserRepository> _userRepo;
    private UserService _userService;

    [SetUp]
    public void SetUp()
    {
        _userRepo = new Mock<IUserRepository>();
        _userService = new UserService(_userRepo.Object);
    }

    [Test]
    public void Test_ShouldReturnUserById()
    {
        //Arrange
        User user = new User()
        {
            Comments = new List<Comment>(),
            Email = "email@email.com",
            Id = 1,
            PasswordHash = "pass",
            Posts = new List<Post>(),
            Role = UserRole.User,
            Username = "User1"
        };
        _userRepo.Setup(u => u.GetById(1)).Returns(user);
        //Act
        User? result = _userService.GetById(1);
        //Assert
        Assert.That(result, Is.EqualTo(user));
        _userRepo.Verify(u => u.GetById(1), Times.Once);
    }

    [Test]
    public void Test_ShouldReturnUserByUsername()
    {
        //Arrange
        User user = new User()
        {
            Comments = new List<Comment>(),
            Email = "email@email.com",
            Id = 1,
            PasswordHash = "pass",
            Posts = new List<Post>(),
            Role = UserRole.User,
            Username = "User1"
        };
        _userRepo.Setup(u => u.GetByUsername("User1")).Returns(user);
        //Act
        User? result = _userService.GetByUsername("User1");
        //Assert
        Assert.That(result, Is.EqualTo(user));
        _userRepo.Verify(u => u.GetByUsername("User1"), Times.Once);
    }

    [Test]
    public void Test_ShouldReturnAllUsers()
    {
        //Arrange
        List<User> users = new List<User>()
        {
            new User(),
            new User(),
            new User()
        };
        _userRepo.Setup(u => u.GetAll()).Returns(users);
        //Act
        var result = _userService.GetAll();
        //Assert
        Assert.That(result, Is.EqualTo(users));
        _userRepo.Verify(u => u.GetAll(), Times.Once);
    }

    [Test]
    public void Test_CreateShouldAddUserToRepo()
    {
        //Arrange
        User user = new User()
        {
            Comments = new List<Comment>(),
            Email = "email@email.com",
            Id = 1,
            PasswordHash = "pass",
            Posts = new List<Post>(),
            Role = UserRole.User,
            Username = "User1"
        };
        _userRepo.Setup(u => u.ExistsByUsername("User1")).Returns(false);
        _userRepo.Setup(u => u.ExistsByEmail("User1")).Returns(false);

        //Act
        _userService.Create(user);
        //Assert
        _userRepo.Verify(u => u.Add(user), Times.Once);
    }

    [Test]
    public void Test_CreateShouldThrowInvalidOperationExceptionIfUsernameExists()
    {
        //Arrange
        User user = new User()
        {
            Comments = new List<Comment>(),
            Email = "email@email.com",
            Id = 1,
            PasswordHash = "pass",
            Posts = new List<Post>(),
            Role = UserRole.User,
            Username = "User1"
        };
        _userRepo.Setup(u => u.ExistsByUsername("User1")).Returns(true);
        _userRepo.Setup(u => u.ExistsByEmail("email@email.com")).Returns(false);

        //Act && Assert
        InvalidOperationException invalidOperationException =
            Assert.Throws<InvalidOperationException>(() => _userService.Create(user));
        Assert.That(invalidOperationException.Message, Is.EqualTo("Username already exists."));
    }
    [Test]
    public void Test_CreateShouldThrowInvalidOperationExceptionIfEmailExists()
    {
        //Arrange
        User user = new User()
        {
            Comments = new List<Comment>(),
            Email = "email@email.com",
            Id = 1,
            PasswordHash = "pass",
            Posts = new List<Post>(),
            Role = UserRole.User,
            Username = "User1"
        };
        _userRepo.Setup(u => u.ExistsByUsername("User1")).Returns(false);
        _userRepo.Setup(u => u.ExistsByEmail("email@email.com")).Returns(true);

        //Act && Assert
        InvalidOperationException invalidOperationException =
            Assert.Throws<InvalidOperationException>(() => _userService.Create(user));
        Assert.That(invalidOperationException.Message, Is.EqualTo("Email already exists."));
    }
    [Test]
    public void Test_UpdateShouldUpdateUser()
    {
        //Arrange
        User existingUser = new User()
        {
            Comments = new List<Comment>(),
            Email = "email@email.com",
            Id = 1,
            PasswordHash = "pass",
            Posts = new List<Post>(),
            Role = UserRole.User,
            Username = "UserOne"
        };
        User user = new User()
        {
            Comments = new List<Comment>(),
            Email = "email@email.com",
            Id = 1,
            PasswordHash = "pass",
            Posts = new List<Post>(),
            Role = UserRole.User,
            Username = "User1"
        };
        _userRepo.Setup(u => u.GetById(1)).Returns(existingUser);

        //Act
        _userService.Update(user);
        //Assert
        _userRepo.Verify(u => u.Update(user), Times.Once);
    }
    [Test]
    public void Test_UpdateShouldThrowInvalidOperationExceptionIfUserIsNull()
    {
        //Arrange
        User? existingUser = null;
        User user = new User()
        {
            Comments = new List<Comment>(),
            Email = "email@email.com",
            Id = 1,
            PasswordHash = "pass",
            Posts = new List<Post>(),
            Role = UserRole.User,
            Username = "User1"
        };
        _userRepo.Setup(u => u.GetById(1)).Returns(existingUser);
        //Act && Assert
        InvalidOperationException invalidOperationException =
            Assert.Throws<InvalidOperationException>(() => _userService.Update(user));
        Assert.That(invalidOperationException.Message, Is.EqualTo("User not found."));
    }
    [Test]
    public void Test_DeleteShouldDeleteUser()
    {
        //Arrange
        User existingUser = new User()
        {
            Comments = new List<Comment>(),
            Email = "email@email.com",
            Id = 1,
            PasswordHash = "pass",
            Posts = new List<Post>(),
            Role = UserRole.User,
            Username = "UserOne"
        };
        _userRepo.Setup(u => u.GetById(1)).Returns(existingUser);

        //Act
        _userService.Delete(1);
        //Assert
        _userRepo.Verify(u => u.Delete(existingUser), Times.Once);
    }
    [Test]
    public void Test_DeleteShouldThrowInvalidOperationExceptionIfUserIsNull()
    {
        //Arrange
        User? existingUser = null;
        User user = new User()
        {
            Comments = new List<Comment>(),
            Email = "email@email.com",
            Id = 1,
            PasswordHash = "pass",
            Posts = new List<Post>(),
            Role = UserRole.User,
            Username = "User1"
        };
        _userRepo.Setup(u => u.GetById(1)).Returns(existingUser);
        //Act && Assert
        InvalidOperationException invalidOperationException =
            Assert.Throws<InvalidOperationException>(() => _userService.Delete(1));
        Assert.That(invalidOperationException.Message, Is.EqualTo("User not found."));
    }
}