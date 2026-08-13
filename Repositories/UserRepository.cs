using Forum.Models;

namespace Forum.Repositories;

public class UserRepository : IUserRepository
{
    private readonly List<User> _users = new();

    public User? GetById(int id)
    {
        foreach (User user in _users)
        {
            if (user.Id == id)
            {
                return user;
            }
        }

        return null;
    }

    public User? GetByUsername(string username)
    {
        foreach (User user in _users)
        {
            if (user.Username.Equals(
                    username,
                    StringComparison.OrdinalIgnoreCase))
            {
                return user;
            }
        }

        return null;
    }

    public User? GetByEmail(string email)
    {
        foreach (User user in _users)
        {
            if (user.Email.Equals(
                    email,
                    StringComparison.OrdinalIgnoreCase))
            {
                return user;
            }
        }

        return null;
    }

    public IEnumerable<User> GetAll()
    {
        return _users;
    }

    public bool ExistsByUsername(string username)
    {
        foreach (User user in _users)
        {
            if (user.Username.Equals(
                    username,
                    StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
        }

        return false;
    }

    public bool ExistsByEmail(string email)
    {
        foreach (User user in _users)
        {
            if (user.Email.Equals(
                    email,
                    StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
        }

        return false;
    }

    public void Add(User user)
    {
        _users.Add(user);
    }

    public void Update(User user)
    {
        User? existingUser = GetById(user.Id);

        if (existingUser == null)
        {
            return;
        }

        existingUser.Username = user.Username;
        existingUser.Email = user.Email;
        existingUser.PasswordHash = user.PasswordHash;
        existingUser.Role = user.Role;
    }

    public void Delete(User user)
    {
        _users.Remove(user);
    }
}