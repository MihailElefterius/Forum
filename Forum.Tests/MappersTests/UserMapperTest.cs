using Forum.Mappers;
using Forum.Models;

namespace Forum.Tests.MappersTests;

public class UserMapperTest
{
    [Test]
    public void Test_PostResponseDtoShouldReturnProperParams()
    {
        //Arrange
        User user = new User()
        {
            Id = 1,
            Comments = new List<Comment>()
            {
                new Comment(), new Comment()
            },
            Email = "email@email.com",
            PasswordHash = "pass1234",
            Posts = new List<Post>()
            {
                new Post(),
                new Post()
            },
            Role = UserRole.User,
            Username = "UserOne"
        };
        //Act
        var result = UserMapper.ToDto(user);
        //Assert
        Assert.That(result.Id, Is.EqualTo(user.Id));
        Assert.That(result.Email, Is.EqualTo(user.Email));
        Assert.That(result.Username, Is.EqualTo(user.Username));
        Assert.That(result.Role, Is.EqualTo(user.Role));
    }
}