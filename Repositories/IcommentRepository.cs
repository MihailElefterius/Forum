using Forum.Models;

namespace Forum.Repositories;

public interface ICommentRepository
{
    Comment? GetById(int id);

    IEnumerable<Comment> GetAll();

    IEnumerable<Comment> GetByPostId(int postId);

    IEnumerable<Comment> GetByUserId(int userId);

    void Add(Comment comment);

    void Update(Comment comment);

    void Delete(Comment comment);
}