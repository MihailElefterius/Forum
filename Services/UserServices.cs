using Forum.Models;
using Forum.Repositories;

namespace Forum.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public User? GetById(int id)
    {
        return _userRepository.GetById(id);
    }

    public User? GetByUsername(string username)
    {
        return _userRepository.GetByUsername(username);
    }

    public IEnumerable<User> GetAll()
    {
        return _userRepository.GetAll();
    }

    public void Create(User user)
    {
        if (_userRepository.ExistsByUsername(user.Username))
        {
            throw new InvalidOperationException("Username already exists.");
        }

        if (_userRepository.ExistsByEmail(user.Email))
        {
            throw new InvalidOperationException("Email already exists.");
        }

        _userRepository.Add(user);
    }

    public void Update(User user)
    {
        User? existingUser = _userRepository.GetById(user.Id);

        if (existingUser == null)
        {
            throw new InvalidOperationException("User not found.");
        }

        _userRepository.Update(user);
    }

    public void Delete(int id)
    {
        User? user = _userRepository.GetById(id);

        if (user == null)
        {
            throw new InvalidOperationException("User not found.");
        }

        _userRepository.Delete(user);
    }
}