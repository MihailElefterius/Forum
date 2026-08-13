using Forum.Models;

namespace Forum.Services;

public interface ICommentService
{
    Comment? GetById(int id);

    IEnumerable<Comment> GetAll();

    IEnumerable<Comment> GetByPostId(int postId);

    IEnumerable<Comment> GetByUserId(int userId);

    void Create(Comment comment);

    void Update(Comment comment);

    void Delete(int id);
}