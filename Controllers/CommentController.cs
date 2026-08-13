using Forum.Models;
using Forum.Services;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Controllers;

[ApiController]
[Route("api/comments")]
public class CommentController : ControllerBase
{
    private readonly ICommentService _commentService;

    public CommentController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        Comment? comment = _commentService.GetById(id);

        if (comment == null)
        {
            return NotFound();
        }

        return Ok(comment);
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        IEnumerable<Comment> comments = _commentService.GetAll();

        return Ok(comments);
    }

    [HttpGet("post/{postId}")]
    public IActionResult GetByPostId(int postId)
    {
        IEnumerable<Comment> comments =
            _commentService.GetByPostId(postId);

        return Ok(comments);
    }

    [HttpGet("user/{userId}")]
    public IActionResult GetByUserId(int userId)
    {
        IEnumerable<Comment> comments =
            _commentService.GetByUserId(userId);

        return Ok(comments);
    }

    [HttpPost]
    public IActionResult Create(Comment comment)
    {
        _commentService.Create(comment);

        return Ok();
    }

    [HttpPut]
    public IActionResult Update(Comment comment)
    {
        _commentService.Update(comment);

        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _commentService.Delete(id);

        return Ok();
    }
}