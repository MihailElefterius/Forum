using Forum.Models;

namespace Forum.Services;

public interface IUserService
{
    User? GetById(int id);

    User? GetByUsername(string username);

    IEnumerable<User> GetAll();

    void Create(User user);

    void Update(User user);

    void Delete(int id);
}