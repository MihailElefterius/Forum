using Forum.Models;

namespace Forum.Services;

public interface IPostService
{
    Post? GetById(int id);

    IEnumerable<Post> GetAll();

    IEnumerable<Post> GetByUserId(int userId);

    void Create(Post post);

    void Update(Post post);

    void Delete(int id);
}