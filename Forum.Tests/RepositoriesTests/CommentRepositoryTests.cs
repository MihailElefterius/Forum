using Forum.Models;
using Forum.Repositories;

namespace Forum.Tests.RepositoriesTests;

public class CommentRepositoryTests
{
    private CommentRepository _commentRepository;
    private Comment _comment;
    private Comment _nextComment;

    [SetUp]
    public void SetUp()
    {
        _commentRepository = new CommentRepository();
        _comment = new Comment()
        {
            Content = "Electric cars",
            CreatedAt = new DateTime(2026, 10, 09),
            Id = 1,
            PostId = 1,
            UserId = 1
        };
        _nextComment = new Comment()
        {
            Content = "I like Benz EQE or EQS",
            CreatedAt = new DateTime(2026, 10, 09),
            Id = 2,
            PostId = 1,
            UserId = 2
        };
    }

    [Test]
    public void Test_GetByIdShouldReturnComment()
    {
        //Arrange
        _commentRepository.Add(_comment);
        //Act
        Comment? result = _commentRepository.GetById(1);
        //Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.EqualTo(_comment));
    }

    [Test]
    public void Test_GetByIdShouldReturnNullWhenCommentIsNotFound()
    {
        //Arrange

        //Act
        Comment? result = _commentRepository.GetById(1);
        //Assert
        Assert.That(result, Is.Null);
    }

    [Test]
    public void Test_GetAllShouldReturnAllComments()
    {
        //Arrange
        _commentRepository.Add(_comment);
        _commentRepository.Add(_nextComment);
        //Act
        var result = _commentRepository.GetAll;
        //Assert
        Assert.That(result, Does.Contain(_comment));
        Assert.That(result, Does.Contain(_nextComment));
    }

    [Test]
    public void Test_GetByPostIdShouldReturnComment()
    {
        //Arrange
        _commentRepository.Add(_comment);
        //Act
        var result = _commentRepository.GetByPostId(1);
        //Assert
        Assert.That(result, Does.Contain(_comment));
    }

    [Test]
    public void Test_GetByUserIdShouldReturnComment()
    {
        //Arrange
        _commentRepository.Add(_comment);
        _commentRepository.Add(_nextComment);
        //Act
        var result = _commentRepository.GetByUserId(1);
        var resultNext = _commentRepository.GetByUserId(2);

        //Assert
        Assert.That(result, Does.Contain(_comment));
        Assert.That(resultNext, Does.Contain(_nextComment));
    }

    [Test]
    public void Test_AddShouldAddCommentToRepo()
    {
        //Arrange
        _commentRepository.Add(_comment);
        _commentRepository.Add(_nextComment);
        //Act
        var result = _commentRepository.GetAll();
        //Assert
        Assert.That(result, Does.Contain(_comment));
        Assert.That(result, Does.Contain(_nextComment));
    }

    [Test]
    public void Test_UpdateShouldUpdateComment()
    {
        //Arrange
        _commentRepository.Add(_comment);
        Comment toUpdate = new Comment()
        {
            Content = "Electric cars or internal combustion engines",
            CreatedAt = new DateTime(2026, 10, 09),
            Id = 1,
            PostId = 1,
            UserId = 1
        };
        //Act
        _commentRepository.Update(toUpdate);
        Comment? result = _commentRepository.GetById(1);
        //Assert
        Assert.That(result.Content, Is.EqualTo("Electric cars or internal combustion engines"));
    }

    [Test]
    public void Test_UpdateShouldNotUpdateCommentWhenNotFound()
    {
        //Arrange
        Comment toUpdate = new Comment()
        {
            Content = "Electric cars or internal combustion engines",
            CreatedAt = new DateTime(2026, 10, 09),
            Id = 1,
            PostId = 1,
            UserId = 1
        };
        //Act
        _commentRepository.Update(toUpdate);
        Comment? result = _commentRepository.GetById(1);
        //Assert
        Assert.That(result, Is.Null);
    }

    [Test]
    public void Test_DeleteShouldDeleteComment()
    {
        //Arrange
        _commentRepository.Add(_comment);
        _commentRepository.Add(_nextComment);
        //Act
        _commentRepository.Delete(_comment);
        var result = _commentRepository.GetAll();
        //Assert
        Assert.That(result,Does.Contain(_nextComment));
        Assert.That(result, Does.Not.Contain(_comment));
    }
}