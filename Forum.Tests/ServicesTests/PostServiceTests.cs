using Forum.Models;
using Forum.Repositories;
using Forum.Services;
using Moq;

namespace Forum.Tests;

public class PostServiceTests
{
    private Mock<IPostRepository> _postRepo;
    private Mock<IUserRepository> _userRepo;
    private PostService _postService;

    [SetUp]
    public void SetUp()
    {
        _postRepo = new Mock<IPostRepository>();
        _userRepo = new Mock<IUserRepository>();
        _postService = new PostService(_postRepo.Object, _userRepo.Object);
    }

    [Test]
    public void Test_ShouldReturnPostById()
    {
        //Arrange
        Post post = new Post()
        {
            Comments = new List<Comment>()
            {
                new Comment(),
                new Comment()
            },
            Content = "Electric cars",
            CreatedAt = new DateTime(2026, 10, 09),
            Id = 1,
            Title = "Discussion",
            User = new User(),
            UserId = 1
        };
        _postRepo.Setup(p => p.GetById(1)).Returns(post);
        //Act
        Post result = _postService.GetById(1);
        //Assert
        Assert.That(result, Is.EqualTo(post));
        _postRepo.Verify(p => p.GetById(1), Times.Once);
    }

    [Test]
    public void Test_ShouldReturnAllPosts()
    {
        //Arrange
        List<Post> posts = new List<Post>()
        {
            new Post(),
            new Post()
        };
        _postRepo.Setup(p => p.GetAll()).Returns(posts);
        //Act
        var result = _postService.GetAll();
        //Assert
        Assert.That(result, Is.EqualTo(posts));
        _postRepo.Verify(p => p.GetAll(), Times.Once);
    }

    [Test]
    public void Test_ShouldGetPostByUserId()
    {
        //Arrange
        List<Post> posts = new List<Post>()
        {
            new Post()
            {
                Comments = new List<Comment>()
                {
                    new Comment(),
                    new Comment()
                },
                Content = "Electric cars",
                CreatedAt = new DateTime(2026, 10, 09),
                Id = 1,
                Title = "Discussion",
                User = new User(),
                UserId = 1
            },
            new Post()
        };
        _postRepo.Setup(p => p.GetByUserId(1)).Returns(posts);
        //Act
        var result = _postService.GetByUserId(1);
        //Assert
        Assert.That(result, Is.EqualTo(posts));
        _postRepo.Verify(p => p.GetByUserId(1), Times.Once);
    }

    [Test]
    public void Test_ShouldAddCreatedPostToRepository()
    {
        //Arrange

        Post post = new Post()
        {
            Comments = new List<Comment>(),
            Content = "Discussions",
            CreatedAt = new DateTime(2026, 10, 09),
            Id = 1,
            Title = "Electric cars",
            User = new User(),
            UserId = 1
        };
        _userRepo.Setup(u => u.GetById(1)).Returns(post.User);
        //Act
        _postService.Create(post);
        //Arrange
        _postRepo.Verify(p => p.Add(post), Times.Once);
    }

    [Test]
    public void Test_CreateShouldThrowInvalidOperationExceptionWhenUserIsNull()
    {
        Post post = new Post()
        {
            Comments = new List<Comment>(),
            Content = "Discussions",
            CreatedAt = new DateTime(2026, 10, 09),
            Id = 1,
            Title = "Electric cars",
            UserId = 1
        };
        _userRepo.Setup(u => u.GetById(1)).Returns(post.User);
        //Act && Assert
        InvalidOperationException invalidOperationException =
            Assert.Throws<InvalidOperationException>(() => _postService.Create(post));
        Assert.That(invalidOperationException.Message, Is.EqualTo("User not found."));
    }

    [Test]
    public void Test_ShouldUpdatePost()
    {
        //Arrange

        Post existingPost = new Post()
        {
            Comments = new List<Comment>(),
            Content = "Discussions",
            CreatedAt = new DateTime(2026, 10, 09),
            Id = 1,
            Title = "Electric cars",
            User = new User(),
            UserId = 1
        };
        Post post = new Post()
        {
            Comments = new List<Comment>(),
            Content = "Discussions Continue",
            CreatedAt = new DateTime(2026, 10, 09),
            Id = 1,
            Title = "Electric cars are the future.My choice is a Benz eqe 350+",
            User = new User(),
            UserId = 1
        };
        _postRepo.Setup(u => u.GetById(1)).Returns(existingPost);
        //Act
        _postService.Update(post);
        //Arrange
        _postRepo.Verify(p => p.Update(post), Times.Once);
    }

    [Test]
    public void Test_UpdateShouldThrowInvalidOperationExceptionWhenExistingPostIsNull()
    {
        Post? existingPost = null;

        Post post = new Post()
        {
            Comments = new List<Comment>(),
            Content = "Discussions",
            CreatedAt = new DateTime(2026, 10, 09),
            Id = 1,
            Title = "Electric cars",
            UserId = 1
        };
        _postRepo.Setup(u => u.GetById(1)).Returns(existingPost);
        //Act && Assert
        InvalidOperationException invalidOperationException =
            Assert.Throws<InvalidOperationException>(() => _postService.Update(post));
        Assert.That(invalidOperationException.Message, Is.EqualTo("Post not found."));
    }

    [Test]
    public void Test_ShouldDeletePostById()
    {
        //Arrange
        Post post = new Post()
        {
            Comments = new List<Comment>()
            {
                new Comment(),
                new Comment()
            },
            Content = "Electric cars",
            CreatedAt = new DateTime(2026, 10, 09),
            Id = 1,
            Title = "Discussion",
            User = new User(),
            UserId = 1
        };
        _postRepo.Setup(p => p.GetById(1)).Returns(post);
        //Act
        _postService.Delete(1);
        //Assert

        _postRepo.Verify(p => p.Delete(post), Times.Once);
    }

    [Test]
    public void Test_DeleteShouldThrowInvalidOperationExceptionWhenPostIsNull()
    {
        Post? post = null;
        
        _postRepo.Setup(u => u.GetById(1)).Returns(post);
        //Act && Assert
        InvalidOperationException invalidOperationException =
            Assert.Throws<InvalidOperationException>(() => _postService.Delete(1));
        Assert.That(invalidOperationException.Message, Is.EqualTo("Post not found."));
    }
}