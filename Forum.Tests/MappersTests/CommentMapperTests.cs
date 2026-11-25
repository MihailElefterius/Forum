using Forum.DTOs.Comment;
using Forum.Mappers;
using Forum.Models;

namespace Forum.Tests.MappersTests;

public class CommentMapperTests
{
    [Test]
    public void Test_CommentResponseDtoShouldReturnProperParams()
    {
        //Arrange
        Comment comment = new Comment()
        {
            Content = "content",
            CreatedAt = new DateTime(2026, 10, 09),
            Id = 1,
            PostId = 1,
            Post = new Post(),
            User = new User(),
            UserId = 1
        };
        //Act
        var result = CommentMapper.ToDto(comment);
        //Assert
        Assert.That(result.Id, Is.EqualTo(comment.Id));
        Assert.That(result.Content, Is.EqualTo(comment.Content));
        Assert.That(result.PostId, Is.EqualTo(comment.PostId));
        Assert.That(result.UserId, Is.EqualTo(comment.UserId));
        Assert.That(result.CreatedAt, Is.EqualTo(comment.CreatedAt));
    }

    [Test]
    public void Test_ToEntityShouldReturnProperParams()
    {
        //Arrange
        CreateCommentDto createCommentDto = new CreateCommentDto()
        {
            Content = "content",
            PostId = 1,
            UserId = 1
        };
        //Act
        var result = CommentMapper.ToEntity(createCommentDto);
        //Assert
        Assert.That(result.Content, Is.EqualTo(createCommentDto.Content));
        Assert.That(result.PostId, Is.EqualTo(createCommentDto.PostId));
        Assert.That(result.UserId, Is.EqualTo(createCommentDto.UserId));
    }
}