using Forum.Models;
using Forum.Repositories;
using Forum.Services;
using Moq;

namespace Forum.Tests;

public class CommentServiceTests
{
    private Mock<ICommentRepository> _commentRepo;
    private Mock<IUserRepository> _userRepo;
    private Mock<IPostRepository> _postRepo;
    private CommentService _commentService;

    [SetUp]
    public void SetUp()
    {
        _commentRepo = new Mock<ICommentRepository>();
        _userRepo = new Mock<IUserRepository>();
        _postRepo = new Mock<IPostRepository>();
        _commentService = new CommentService(_commentRepo.Object, _userRepo.Object, _postRepo.Object);
    }

    [Test]
    public void Test_ShouldReturn_CommentById()
    {
        //Arrange
        Comment comment = new Comment()
        {
            Content = "Carl Benz 1884",
            CreatedAt = new DateTime(2026, 10, 09),
            Id = 1,
            Post = new Post(),
            PostId = 1,
            User = new User(),
            UserId = 1
        };
        _commentRepo.Setup(c => c.GetById(1)).Returns(comment);
        //Act
        Comment result = _commentService.GetById(1);

        //Assert
        Assert.That(result, Is.EqualTo(comment));
        _commentRepo.Verify(c => c.GetById(1), Times.Once());
    }

    [Test]
    public void Test_ShouldReturnAllComments()
    {
        //Arrange
        List<Comment> commentsList = new List<Comment>()
        {
            new Comment()
            {
                Content = "Carl Benz 1884",
                CreatedAt = new DateTime(2026, 10, 09),
                Id = 1,
                PostId = 1,
                UserId = 1
            },
            new Comment()
            {
                Content = "Carl Benz 1885 patent Motorwagen",
                CreatedAt = new DateTime(2026, 10, 09),
                Id = 2,
                PostId = 1,
                UserId = 2
            }
        };
        _commentRepo.Setup(c => c.GetAll()).Returns(commentsList);
        //Act
        var result = _commentService.GetAll();

        //Assert
        Assert.That(result, Is.EqualTo(commentsList));
        _commentRepo.Verify(c => c.GetAll(), Times.Once());
    }

    [Test]
    public void Test_ShouldReturnCommentByPostId()
    {
        //Arrange
        List<Comment> commentsList = new List<Comment>()
        {
            new Comment()
            {
                Content = "Carl Benz 1884",
                CreatedAt = new DateTime(2026, 10, 09),
                Id = 1,
                PostId = 1,
                UserId = 1
            },
            new Comment()
            {
                Content = "Carl Benz 1885 patent Motorwagen",
                CreatedAt = new DateTime(2026, 10, 09),
                Id = 2,
                PostId = 1,
                UserId = 2
            }
        };

        _commentRepo.Setup(c => c.GetByPostId(1)).Returns(commentsList);
        //Act
        var result = _commentService.GetByPostId(1);

        //Assert
        Assert.That(result, Is.EqualTo(commentsList));
        _commentRepo.Verify(c => c.GetByPostId(1), Times.Once());
    }

    [Test]
    public void Test_ShouldReturnCommentByUserId()
    {
        //Arrange
        List<Comment> commentsList = new List<Comment>()
        {
            new Comment()
            {
                Content = "Carl Benz 1884",
                CreatedAt = new DateTime(2026, 10, 09),
                Id = 1,
                PostId = 1,
                UserId = 1
            },
            new Comment()
            {
                Content = "Carl Benz 1885 patent Motorwagen",
                CreatedAt = new DateTime(2026, 10, 09),
                Id = 2,
                PostId = 1,
                UserId = 2
            }
        };

        _commentRepo.Setup(c => c.GetByUserId(1)).Returns(commentsList);
        //Act
        var result = _commentService.GetByUserId(1);

        //Assert
        Assert.That(result, Is.EqualTo(commentsList));
        _commentRepo.Verify(c => c.GetByUserId(1), Times.Once());
    }

    [Test]
    public void Test_CreateShouldThrowInvalidOperationExceptionWhenUserIsNull()
    {
        //Arrange
        Comment comment = new Comment()
        {
            Content = "Carl Benz 1884",
            CreatedAt = new DateTime(2026, 10, 09),
            Id = 1,
            Post = new Post(),
            PostId = 1,
            User = null,
            UserId = 1
        };
        _userRepo.Setup(u => u.GetById(1)).Returns(comment.User);
        _postRepo.Setup(p => p.GetById(1)).Returns(comment.Post);

        //Act && Assert
        InvalidOperationException invalidOperationException =
            Assert.Throws<InvalidOperationException>(() => _commentService.Create(comment));
        Assert.That(invalidOperationException.Message, Is.EqualTo("User not found."));
    }

    [Test]
    public void Test_CreateShouldThrowInvalidOperationExceptionWhenPostIsNull()
    {
        //Arrange
        Comment comment = new Comment()
        {
            Content = "Carl Benz 1884",
            CreatedAt = new DateTime(2026, 10, 09),
            Id = 2,
            Post = null,
            PostId = 2,
            User = new User(),
            UserId = 2
        };
        _userRepo.Setup(u => u.GetById(2)).Returns(comment.User);
        _postRepo.Setup(p => p.GetById(2)).Returns(comment.Post);
        //Act && Assert
        InvalidOperationException invalidOperationException =
            Assert.Throws<InvalidOperationException>(() => _commentService.Create(comment));
        Assert.That(invalidOperationException.Message, Is.EqualTo("Post not found."));
    }

    [Test]
    public void Test_CreateShouldAddCommentToRepository()
    {
        //Arrange
        Comment comment = new Comment()
        {
            Content = "Carl Benz 1884",
            CreatedAt = new DateTime(2026, 10, 09),
            Id = 1,
            Post = new Post(),
            PostId = 1,
            User = new User(),
            UserId = 1
        };
        _userRepo.Setup(u => u.GetById(1)).Returns(comment.User);
        _postRepo.Setup(p => p.GetById(1)).Returns(comment.Post);
        //Act
        _commentService.Create(comment);
        //Assert
        _commentRepo.Verify(c => c.Add(comment), Times.Once());
    }

    [Test]
    public void Test_UpdateShouldThrowInvalidOperationExceptionWhenCommentIsNull()
    {
        //Arrange
        Comment? comment = new Comment()
        {
            Content = "Lets continue our talks about a good Benz",
            CreatedAt = new DateTime(2026, 10, 09),
            Id = 1,
            Post = new Post(),
            PostId = 1,
            User = new User(),
            UserId = 1
        };
        Comment? existingComment = null;
        _commentRepo.Setup(c => c.GetById(1)).Returns(existingComment);
        //Act && Assert
        InvalidOperationException invalidOperationException =
            Assert.Throws<InvalidOperationException>(() => _commentService.Update(comment));
        Assert.That(invalidOperationException.Message, Is.EqualTo("Comment not found."));
    }

    [Test]
    public void Test_ShouldUpdateCommentInRepository()
    {
        //Arrange
        Comment? comment = new Comment()
        {
            Content = "Lets continue our talks about a good Benz",
            CreatedAt = new DateTime(2026, 10, 09),
            Id = 1,
            Post = new Post(),
            PostId = 1,
            User = new User(),
            UserId = 1
        };
        Comment existingComment = new Comment()
        {
            Content = "Carl Benz 1884",
            CreatedAt = new DateTime(2026, 10, 09),
            Id = 1,
            Post = new Post(),
            PostId = 1,
            User = new User(),
            UserId = 1
        };
        _commentRepo.Setup(c => c.GetById(1)).Returns(existingComment);

        //Act
        _commentService.Update(comment);
        //Assert
        _commentRepo.Verify(c => c.Update(comment), Times.Once());
    }
    [Test]
    public void Test_DeleteShouldThrowInvalidOperationExceptionWhenCommentIsNull()
    {
        //Arrange
        Comment? existingComment = null;
        _commentRepo.Setup(c => c.GetById(1)).Returns(existingComment);
        //Act && Assert
        InvalidOperationException invalidOperationException =
            Assert.Throws<InvalidOperationException>(() => _commentService.Delete(1));
        Assert.That(invalidOperationException.Message, Is.EqualTo("Comment not found."));
    }
    [Test]
    public void Test_DeleteShouldDeleteCommentWhenFound()
    {
        //Arrange
        Comment comment = new Comment()
        {
            Content = "Carl Benz 1884",
            CreatedAt = new DateTime(2026, 10, 09),
            Id = 1,
            Post = new Post(),
            PostId = 1,
            User = new User(),
            UserId = 1
        };
        _commentRepo.Setup(c => c.GetById(1)).Returns(comment);
        //Act
        _commentService.Delete(1);

        //Assert
        _commentRepo.Verify(c => c.Delete(comment), Times.Once());
    }

}