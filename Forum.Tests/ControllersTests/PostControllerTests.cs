using Forum.Controllers;
using Forum.Models;
using Forum.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Forum.Tests.ControllersTests;

public class PostControllerTests
{
    private Mock<IPostService> _postService;
    private PostController _postController;

    [SetUp]
    public void SetUp()
    {
        _postService = new Mock<IPostService>();
        _postController = new PostController(_postService.Object);
    }

    [Test]
    public void Test_GetByIdShouldReturnOkStatus()
    {
        //Arrange
        Post post = new Post()
        {
            Comments = new List<Comment>(),
            Content = "discussions",
            CreatedAt = new DateTime(2026, 10, 09),
            Id = 1,
            Title = "Title",
            User = new User(),
            UserId = 1
        };
        _postService.Setup(p => p.GetById(1)).Returns(post);
        //Act
        var result = _postController.GetById(1);
        //Assert
        Assert.That(result, Is.TypeOf<OkObjectResult>());
        OkObjectResult objectResult = (OkObjectResult)result;
        Assert.That(objectResult.Value, Is.EqualTo(post));
        _postService.Verify(p => p.GetById(1), Times.Once);
    }

    [Test]
    public void Test_GetByIdShouldReturnNotFoundStatusWhenNull()
    {
        //Arrange
        Post? post = null;

        _postService.Setup(p => p.GetById(1)).Returns(post);
        //Act
        var result = _postController.GetById(1);
        //Assert
        Assert.That(result, Is.TypeOf<NotFoundResult>());
    }

    [Test]
    public void Test_GetByUserIdShouldReturnOkStatus()
    {
        //Arrange
        List<Post> posts = new List<Post>()
        {
            new Post()
            {
                Comments = new List<Comment>(),
                Content = "discussions",
                CreatedAt = new DateTime(2026, 10, 09),
                Id = 1,
                Title = "Title",
                UserId = 1
            },
            new Post()
            {
                Comments = new List<Comment>(),
                Content = "discussions about electric vehicles",
                CreatedAt = new DateTime(2026, 10, 09),
                Id = 2,
                Title = "EVs",
                UserId = 1
            }
        };
        _postService.Setup(p => p.GetByUserId(1)).Returns(posts);
        //Act
        var result = _postController.GetByUserId(1);
        //Assert
        Assert.That(result, Is.TypeOf<OkObjectResult>());
        OkObjectResult objectResult = (OkObjectResult)result;
        Assert.That(objectResult.Value, Is.EqualTo(posts));
        _postService.Verify(p => p.GetByUserId(1), Times.Once);
    }

    [Test]
    public void Test_GetAllShouldReturnOkStatus()
    {
        //Arrange
        List<Post> posts = new List<Post>()
        {
            new Post()
            {
                Comments = new List<Comment>(),
                Content = "discussions",
                CreatedAt = new DateTime(2026, 10, 09),
                Id = 1,
                Title = "Title",
                UserId = 1
            },
            new Post()
            {
                Comments = new List<Comment>(),
                Content = "discussions about electric vehicles",
                CreatedAt = new DateTime(2026, 10, 09),
                Id = 2,
                Title = "EVs",
                UserId = 1
            }
        };
        _postService.Setup(p => p.GetAll()).Returns(posts);
        //Act
        var result = _postController.GetAll();
        //Assert
        Assert.That(result, Is.TypeOf<OkObjectResult>());
        OkObjectResult objectResult = (OkObjectResult)result;
        Assert.That(objectResult.Value, Is.EqualTo(posts));
        _postService.Verify(p => p.GetAll(), Times.Once);
    }

    [Test]
    public void Test_CreateShouldCreatePostWithOkStatus()
    {
        //Arrange
        Post post = new Post()
        {
            Comments = new List<Comment>(),
            Content = "discussions about electric vehicles",
            CreatedAt = new DateTime(2026, 10, 09),
            Id = 2,
            Title = "EVs",
            UserId = 1
        };
        //Act
        var result = _postController.Create(post);
        //Assert
        _postService.Verify(p=>p.Create(post),Times.Once);
        Assert.That(result,Is.TypeOf<OkResult>());
    }
    [Test]
    public void Test_UpdateShouldUpdatePostWithOkStatus()
    {
        //Arrange
        Post post = new Post()
        {
            Comments = new List<Comment>(),
            Content = "discussions about Evs",
            CreatedAt = new DateTime(2026, 10, 09),
            Id = 2,
            Title = "EVs",
            UserId = 1
        };
        //Act
        var result = _postController.Update(post);
        //Assert
        _postService.Verify(p=>p.Update(post),Times.Once);
        Assert.That(result,Is.TypeOf<OkResult>());
    }
    [Test]
    public void Test_DeleteShouldDeletePostWithOkStatus()
    {
        //Arrange
        Post post = new Post()
        {
            Comments = new List<Comment>(),
            Content = "discussions about Evs",
            CreatedAt = new DateTime(2026, 10, 09),
            Id = 2,
            Title = "EVs",
            UserId = 1
        };
        //Act
        var result = _postController.Delete(1);
        //Assert
        _postService.Verify(p=>p.Delete(1),Times.Once);
        Assert.That(result,Is.TypeOf<OkResult>());
    }
}