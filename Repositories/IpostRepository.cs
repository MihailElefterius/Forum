using Forum.Models;

namespace Forum.Repositories;

public interface IPostRepository
{
    Post? GetById(int id);

    IEnumerable<Post> GetAll();

    IEnumerable<Post> GetByUserId(int userId);

    void Add(Post post);

    void Update(Post post);

    void Delete(Post post);
}