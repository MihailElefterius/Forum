using Forum.Models;
using Forum.Services;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        User? user = _userService.GetById(id);

        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    [HttpGet("username/{username}")]
    public IActionResult GetByUsername(string username)
    {
        User? user = _userService.GetByUsername(username);

        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        IEnumerable<User> users = _userService.GetAll();

        return Ok(users);
    }

    [HttpPost]
    public IActionResult Create(User user)
    {
        _userService.Create(user);

        return Ok();
    }

    [HttpPut]
    public IActionResult Update(User user)
    {
        _userService.Update(user);

        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _userService.Delete(id);

        return Ok();
    }
}