using Forum.Models;

namespace Forum.Repositories;

public interface IUserRepository
{
    User? GetById(int id);

    User? GetByUsername(string username);

    User? GetByEmail(string email);

    IEnumerable<User> GetAll();

    bool ExistsByUsername(string username);

    bool ExistsByEmail(string email);

    void Add(User user);

    void Update(User user);

    void Delete(User user);
}