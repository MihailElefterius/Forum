using Forum.Controllers;
using Forum.Models;
using Forum.Repositories;
using Forum.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Forum.Tests.ControllersTests;

public class CommentControllerTests
{
    private Mock<ICommentService> _commentService;
    private CommentController _commentController;

    [SetUp]
    public void SetUp()
    {
        _commentService = new Mock<ICommentService>();
        _commentController = new CommentController(_commentService.Object);
    }

    [Test]
    public void Test_CommentGetByIdShouldReturnOkStatusWhenCommentFound()
    {
        //Arrange
        Comment comment = new Comment()
        {
            Content = "Evs discussion",
            CreatedAt = new DateTime(2026, 10, 09),
            Id = 1,
            PostId = 1,
            UserId = 1
        };
        _commentService.Setup(c => c.GetById(1)).Returns(comment);
        //Act
        var result = _commentController.GetById(1);
        //Assert
        Assert.That(result, Is.TypeOf<OkObjectResult>());
        OkObjectResult objectResult = (OkObjectResult)result;
        Assert.That(objectResult.Value, Is.EqualTo(comment));
        _commentService.Verify(c => c.GetById(1), Times.Once);
    }

    [Test]
    public void Test_CommentGetByIdShouldReturnNotFoundStatusWhenCommentNotFound()
    {
        //Arrange
        Comment? comment = null;

        _commentService.Setup(c => c.GetById(1)).Returns(comment);
        //Act
        var result = _commentController.GetById(1);
        //Assert
        Assert.That(result, Is.TypeOf<NotFoundResult>());
    }

    [Test]
    public void Test_CommentGetAllShouldReturnOkStatusWhenCommentsFound()
    {
        //Arrange
        List<Comment> comments = new List<Comment>()
        {
            new Comment()
            {
                Content = "Evs discussion",
                CreatedAt = new DateTime(2026, 10, 09),
                Id = 1,
                PostId = 1,
                UserId = 1
            },
            new Comment()
            {
                Content = "Evs discussion continue",
                CreatedAt = new DateTime(2026, 10, 09),
                Id = 2,
                PostId = 1,
                UserId = 2
            }
        };
        _commentService.Setup(c => c.GetAll()).Returns(comments);
        //Act
        var result = _commentController.GetAll();
        //Assert
        Assert.That(result, Is.TypeOf<OkObjectResult>());
        OkObjectResult objectResult = (OkObjectResult)result;
        Assert.That(objectResult.Value, Is.EqualTo(comments));
        _commentService.Verify(c => c.GetAll(), Times.Once);
    }
    [Test]
    public void Test_CommentGetByPostIdShouldReturnOkStatusWhenCommentFound()
    {
        //Arrange
        List<Comment> comments = new List<Comment>()
        {
            new Comment()
            {
                Content = "Evs discussion",
                CreatedAt = new DateTime(2026, 10, 09),
                Id = 1,
                PostId = 1,
                UserId = 1
            },
            new Comment()
            {
                Content = "Evs discussion continue",
                CreatedAt = new DateTime(2026, 10, 09),
                Id = 2,
                PostId = 1,
                UserId = 2
            }
        };
        _commentService.Setup(c => c.GetByPostId(1)).Returns(comments);
        //Act
        var result = _commentController.GetByPostId(1);
        //Assert
        Assert.That(result, Is.TypeOf<OkObjectResult>());
        OkObjectResult objectResult = (OkObjectResult)result;
        Assert.That(objectResult.Value, Is.EqualTo(comments));
        _commentService.Verify(c => c.GetByPostId(1), Times.Once);
    }
    [Test]
    public void Test_CommentGetByUserIdShouldReturnOkStatusWhenCommentFound()
    {
        //Arrange
        List<Comment> comments = new List<Comment>()
        {
            new Comment()
            {
                Content = "Evs discussion",
                CreatedAt = new DateTime(2026, 10, 09),
                Id = 1,
                PostId = 1,
                UserId = 1
            },
            new Comment()
            {
                Content = "Evs discussion continue",
                CreatedAt = new DateTime(2026, 10, 09),
                Id = 2,
                PostId = 1,
                UserId = 2
            }
        };
        _commentService.Setup(c => c.GetByUserId(1)).Returns(comments);
        //Act
        var result = _commentController.GetByUserId(1);
        //Assert
        Assert.That(result, Is.TypeOf<OkObjectResult>());
        OkObjectResult objectResult = (OkObjectResult)result;
        Assert.That(objectResult.Value, Is.EqualTo(comments));
        _commentService.Verify(c => c.GetByUserId(1), Times.Once);
    }
    [Test]
    public void Test_CommentCreateShouldReturnOkStatus()
    {
        //Arrange
        Comment comment = new Comment()
        {
            Content = "Evs discussion",
            CreatedAt = new DateTime(2026, 10, 09),
            Id = 1,
            PostId = 1,
            UserId = 1
        };
        //Act
        var result = _commentController.Create(comment);
        //Assert
        Assert.That(result, Is.TypeOf<OkResult>());
        _commentService.Verify(c => c.Create(comment), Times.Once);
    }
    [Test]
    public void Test_CommentUpdateShouldReturnOkStatus()
    {
        //Arrange
        Comment comment = new Comment()
        {
            Content = "Evs discussion",
            CreatedAt = new DateTime(2026, 10, 09),
            Id = 1,
            PostId = 1,
            UserId = 1
        };
        //Act
        var result = _commentController.Update(comment);
        //Assert
        Assert.That(result, Is.TypeOf<OkResult>());
        _commentService.Verify(c => c.Update(comment), Times.Once);
    }
    [Test]
    public void Test_CommentDeleteShouldReturnOkStatus()
    {
        //Arrange
        Comment comment = new Comment()
        {
            Content = "Evs discussion",
            CreatedAt = new DateTime(2026, 10, 09),
            Id = 1,
            PostId = 1,
            UserId = 1
        };
        //Act
        var result = _commentController.Delete(1);
        //Assert
        Assert.That(result, Is.TypeOf<OkResult>());
        _commentService.Verify(c => c.Delete(1), Times.Once);
    }
}