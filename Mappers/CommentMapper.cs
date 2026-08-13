using Forum.DTOs.Comment;
using Forum.Models;

namespace Forum.Mappers;

public static class CommentMapper
{
    public static CommentResponseDto ToDto(Comment comment)
    {
        return new CommentResponseDto
        {
            Id = comment.Id,
            Content = comment.Content,
            CreatedAt = comment.CreatedAt,
            UserId = comment.UserId,
            PostId = comment.PostId
        };
    }

    public static Comment ToEntity(CreateCommentDto dto)
    {
        return new Comment
        {
            Content = dto.Content,
            UserId = dto.UserId,
            PostId = dto.PostId,
            CreatedAt = DateTime.UtcNow
        };
    }
}