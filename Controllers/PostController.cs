using Forum.Models;
using Forum.Services;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Controllers;

[ApiController]
[Route("api/posts")]
public class PostController : ControllerBase
{
    private readonly IPostService _postService;

    public PostController(IPostService postService)
    {
        _postService = postService;
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        Post? post = _postService.GetById(id);

        if (post == null)
        {
            return NotFound();
        }

        return Ok(post);
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        IEnumerable<Post> posts = _postService.GetAll();

        return Ok(posts);
    }

    [HttpGet("user/{userId}")]
    public IActionResult GetByUserId(int userId)
    {
        IEnumerable<Post> posts = _postService.GetByUserId(userId);

        return Ok(posts);
    }

    [HttpPost]
    public IActionResult Create(Post post)
    {
        _postService.Create(post);

        return Ok();
    }

    [HttpPut]
    public IActionResult Update(Post post)
    {
        _postService.Update(post);

        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _postService.Delete(id);

        return Ok();
    }
}