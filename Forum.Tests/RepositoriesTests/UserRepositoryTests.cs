using Forum.Models;
using Forum.Repositories;

namespace Forum.Tests.RepositoriesTests;

public class UserRepositoryTests
{
    private User? user;
    private User? userTwo;
    private UserRepository _userRepository;

    [SetUp]
    public void SetUp()
    {
        user = new User()
        {
            Comments = new List<Comment>(),
            Email = "email@email.com",
            Id = 1,
            PasswordHash = "pass",
            Posts = new List<Post>(),
            Role = UserRole.User,
            Username = "UserOne"
        };
        userTwo = new User()
        {
            Comments = new List<Comment>(),
            Email = "emailTwo@email.com",
            Id = 2,
            PasswordHash = "passTwo",
            Posts = new List<Post>(),
            Role = UserRole.User,
            Username = "UserTwo"
        };
        _userRepository = new UserRepository();
    }

    [Test]
    public void Test_GetByIdShouldReturnUser()
    {
        //Arrange
        _userRepository.Add(user);
        //Act
        User? result = _userRepository.GetById(1);
        //Assert
        Assert.That(result, Is.EqualTo(user));
    }

    [Test]
    public void Test_GetByIdShouldReturnNullWhenUserNotInList()
    {
        //Arrange

        //Act
        User? result = _userRepository.GetById(1);
        //Assert
        Assert.That(result, Is.EqualTo(null));
    }

    [Test]
    public void Test_GetByUsernameShouldReturnUser()
    {
        //Arrange
        _userRepository.Add(user);
        //Act
        User? result = _userRepository.GetByUsername("UserOne");
        //Assert
        Assert.That(result, Is.EqualTo(user));
    }

    [Test]
    public void Test_GetByUsernameShouldReturnNullWhenUserNotInList()
    {
        //Arrange

        //Act
        User? result = _userRepository.GetByUsername("UserOne");
        //Assert
        Assert.That(result, Is.EqualTo(null));
    }

    [Test]
    public void Test_GetByEmailShouldReturnUser()
    {
        //Arrange
        _userRepository.Add(user);
        //Act
        User? result = _userRepository.GetByEmail("email@email.com");
        //Assert
        Assert.That(result, Is.EqualTo(user));
    }

    [Test]
    public void Test_GetByEmailShouldReturnNullWhenUserNotInList()
    {
        //Arrange

        //Act
        User? result = _userRepository.GetByEmail("email@email.com");
        //Assert
        Assert.That(result, Is.EqualTo(null));
    }

    [Test]
    public void Test_ShouldReturnTrueIfEmailExists()
    {
        //Arrange
        _userRepository.Add(user);
        //Act
        bool result = _userRepository.ExistsByEmail("email@email.com");
        //Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void Test_ShouldReturnFalseIfEmailDoesNotExists()
    {
        //Arrange

        //Act
        bool result = _userRepository.ExistsByEmail("email@email.com");
        //Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void Test_ShouldReturnTrueIfUsernameExists()
    {
        //Arrange
        _userRepository.Add(user);
        //Act
        bool result = _userRepository.ExistsByUsername("UserOne");
        //Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void Test_ShouldReturnFalseIfUsernameDoesNotExists()
    {
        //Arrange

        //Act
        bool result = _userRepository.ExistsByUsername("UserOne");
        //Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void Test_GetAllShouldReturnAllUsers()
    {
        //Arrange
        _userRepository.Add(user);
        _userRepository.Add(userTwo);
        //Act
        var result = _userRepository.GetAll();
        //Assert
        Assert.That(result, Does.Contain(user));
        Assert.That(result, Does.Contain(userTwo));
    }

    [Test]
    public void Test_AddShouldReturnAddedUsers()
    {
        //Arrange
        _userRepository.Add(user);
        _userRepository.Add(userTwo);

        //Act
        var result = _userRepository.GetAll();
        //Assert
        Assert.That(result, Does.Contain(user));
        Assert.That(result, Does.Contain(userTwo));
    }

    [Test]
    public void Test_UpdateShouldUpdateUser()
    {
        //Arrange
        _userRepository.Add(user);
        User? toUpdate = new User()
        {
            Comments = new List<Comment>(),
            Email = "email@email.com",
            Id = 1,
            PasswordHash = "pass",
            Posts = new List<Post>(),
            Role = UserRole.User,
            Username = "User1"
        };

        //Act
        _userRepository.Update(toUpdate);
        User? result = _userRepository.GetById(1);
        //Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Username, Is.EqualTo("User1"));
    }

    [Test]
    public void Test_UpdateShouldNotUpdateUserWhenUserDoesNotExist()
    {
        //Arrange

        //Act
        _userRepository.Update(user);
        //Assert
        User result = _userRepository.GetById(1);
        Assert.That(result, Is.Null);
    }

    [Test]
    public void Test_DeleteShouldDeleteUser()
    {
        //Arrange
        _userRepository.Add(user);
        //Act
        _userRepository.Delete(user);
        var result = _userRepository.GetAll();
        //Assert
        Assert.That(result, Does.Not.Contain(user));
    }
}