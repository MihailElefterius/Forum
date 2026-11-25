using Forum.Models;
using Forum.Repositories;

namespace Forum.Tests.RepositoriesTests;

public class PostRepositoryTests
{
    private PostRepository _postRepository;

    private Post post;
    private Post postTwo;

    [SetUp]
    public void SetUp()
    {
        _postRepository = new PostRepository();
        post = new Post()
        {
            Comments = new List<Comment>(),
            Content = "Content",
            CreatedAt = new DateTime(2026, 10, 09),
            Id = 1,
            Title = "Title", User = new User(),
            UserId = 1
        };
        postTwo = new Post()
        {
            Comments = new List<Comment>(),
            Content = "ContentTwo",
            CreatedAt = new DateTime(2026, 10, 09),
            Id = 2,
            Title = "TitleTwo", User = new User(),
            UserId = 2
        };
    }

    [Test]
    public void Test_GetByIdShouldReturnPost()
    {
        //Arrange
        _postRepository.Add(post);
        //Act
        Post? result = _postRepository.GetById(1);
        //Assert
        Assert.That(result, Is.EqualTo(post));
    }

    [Test]
    public void Test_GetByIdShouldReturnNullWhenNotFound()
    {
        //Arrange

        //Act
        Post? result = _postRepository.GetById(1);
        //Assert
        Assert.That(result, Is.EqualTo(null));
    }

    [Test]
    public void Test_GetAllShouldReturnAllPosts()
    {
        //Arrange
        _postRepository.Add(post);
        _postRepository.Add(postTwo);
        //Act
        var result = _postRepository.GetAll();
        //Assert
        Assert.That(result, Does.Contain(post));
        Assert.That(result, Does.Contain(postTwo));
    }

    [Test]
    public void Test_GetByUserIdShouldReturnPosts()
    {
        //Arrange
        _postRepository.Add(post);

        //Act
        var result = _postRepository.GetByUserId(1);

        //Assert
        Assert.That(result, Does.Contain(post));
    }

    [Test]
    public void Test_GetByUserIdShouldNotReturnPosts()
    {
        //Arrange
        _postRepository.Add(post);

        //Act
        var result = _postRepository.GetByUserId(2);

        //Assert
        Assert.That(result, Does.Not.Contain(postTwo));
    }

    [Test]
    public void Test_AddShouldAddPost()
    {
        //Arrange
        _postRepository.Add(post);

        //Act
        var result = _postRepository.GetAll();
        //Assert
        Assert.That(result, Does.Contain(post));
    }

    [Test]
    public void Test_UpdateShouldUpdatePost()
    {
        //Arrange
        _postRepository.Add(post);
        Post postNew = new Post()
        {
            Comments = new List<Comment>(),
            Content = "ContentNew",
            CreatedAt = new DateTime(2026, 10, 09),
            Id = 1,
            Title = "TitleNew", User = new User(),
            UserId = 1
        };
        //Act
        _postRepository.Update(postNew);
        Post? result = _postRepository.GetById(1);
        
        //Assert
        Assert.That(result.Content, Is.EqualTo("ContentNew"));
        Assert.That(result.Title, Is.EqualTo("TitleNew"));
        Assert.That(result.Id, Is.EqualTo(1));
    }
    [Test]
    public void Test_UpdateShouldNotUpdateNonExistentPost()
    {
        //Arrange
        Post postNew = new Post()
        {
            Comments = new List<Comment>(),
            Content = "ContentNew",
            CreatedAt = new DateTime(2026, 10, 09),
            Id = 99,
            Title = "TitleNew", User = new User(),
            UserId = 1
        };
        //Act
        _postRepository.Update(postNew);
        Post? result = _postRepository.GetById(99);
        
        //Assert
        Assert.That(result,Is.Null);
    }
    [Test]
    public void Test_DeleteShouldDeletePost()
    {
        //Arrange
        _postRepository.Add(post);
      
        //Act
        _postRepository.Delete(post);
        var result = _postRepository.GetAll();
        
        //Assert
      Assert.That(result,Does.Not.Contain(post));
    }
}