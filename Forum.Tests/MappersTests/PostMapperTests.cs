using Forum.DTOs.Comment;
using Forum.DTOs.Post;
using Forum.Mappers;
using Forum.Models;

namespace Forum.Tests.MappersTests;

public class PostMapperTests
{
    [Test]
    public void Test_PostResponseDtoShouldReturnProperParams()
    {
        //Arrange
        Post post = new Post()
        {
            Content = "content",
            CreatedAt = new DateTime(2026, 10, 09),
            Id = 1,
            User = new User(),
            UserId = 1,
            Comments = new List<Comment>()
            {
                new Comment(), new Comment()
            },
            Title = "Title"
        };
        //Act
        var result = PostMapper.ToDto(post);
        //Assert
        Assert.That(result.Id, Is.EqualTo(post.Id));
        Assert.That(result.Content, Is.EqualTo(post.Content));
        Assert.That(result.UserId, Is.EqualTo(post.UserId));
        Assert.That(result.CreatedAt, Is.EqualTo(post.CreatedAt));
        Assert.That(result.Title, Is.EqualTo(post.Title));
    }

    [Test]
    public void Test_ToEntityShouldReturnProperParams()
    {
        //Arrange
        PostCreateDto postCreateDto = new PostCreateDto()
        {
            Content = "content",
            UserId = 1,
            Title = "Title"
        };
        //Act
        var result = PostMapper.ToEntity(postCreateDto);
        //Assert
        Assert.That(result.Content, Is.EqualTo(postCreateDto.Content));
        Assert.That(result.Title, Is.EqualTo(postCreateDto.Title));
        Assert.That(result.UserId, Is.EqualTo(postCreateDto.UserId));
    }
}