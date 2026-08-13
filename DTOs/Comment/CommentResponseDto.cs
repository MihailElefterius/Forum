namespace Forum.DTOs.Comment;

public class CommentResponseDto
{
    public int Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public int UserId { get; set; }
    public int PostId { get; set; }
}