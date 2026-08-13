using Forum.DTOs.Post;
using Forum.Models;

namespace Forum.Mappers;

public static class PostMapper
{
    public static PostResponseDto ToDto(Post post)
    {
        return new PostResponseDto
        {
            Id = post.Id,
            Title = post.Title,
            Content = post.Content,
            CreatedAt = post.CreatedAt,
            UserId = post.UserId
        };
    }

    public static Post ToEntity(PostCreateDto dto)
    {
        return new Post
        {
            Title = dto.Title,
            Content = dto.Content,
            UserId = dto.UserId,
            CreatedAt = DateTime.UtcNow
        };
    }
}