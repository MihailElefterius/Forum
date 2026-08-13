namespace Forum.Models;

public class User
{
    public int Id { get; set; }

    public string Username { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    public UserRole Role { get; set; }

    public ICollection<Post> Posts { get; set; } = new List<Post>();

    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
}