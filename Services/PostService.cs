using Forum.Models;
using Forum.Repositories;

namespace Forum.Services;

public class PostService : IPostService
{
    private readonly IPostRepository _postRepository;
    private readonly IUserRepository _userRepository;

    public PostService(
        IPostRepository postRepository,
        IUserRepository userRepository)
    {
        _postRepository = postRepository;
        _userRepository = userRepository;
    }

    public Post? GetById(int id)
    {
        return _postRepository.GetById(id);
    }

    public IEnumerable<Post> GetAll()
    {
        return _postRepository.GetAll();
    }

    public IEnumerable<Post> GetByUserId(int userId)
    {
        return _postRepository.GetByUserId(userId);
    }

    public void Create(Post post)
    {
        User? user = _userRepository.GetById(post.UserId);

        if (user == null)
        {
            throw new InvalidOperationException("User not found.");
        }

        _postRepository.Add(post);
    }

    public void Update(Post post)
    {
        Post? existingPost = _postRepository.GetById(post.Id);

        if (existingPost == null)
        {
            throw new InvalidOperationException("Post not found.");
        }

        _postRepository.Update(post);
    }

    public void Delete(int id)
    {
        Post? post = _postRepository.GetById(id);

        if (post == null)
        {
            throw new InvalidOperationException("Post not found.");
        }

        _postRepository.Delete(post);
    }
}