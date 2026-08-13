using Forum.Models;

namespace Forum.Repositories;

public class PostRepository : IPostRepository
{
    private readonly List<Post> _posts = new();

    public Post? GetById(int id)
    {
        foreach (Post post in _posts)
        {
            if (post.Id == id)
            {
                return post;
            }
        }

        return null;
    }

    public IEnumerable<Post> GetAll()
    {
        return _posts;
    }

    public IEnumerable<Post> GetByUserId(int userId)
    {
        List<Post> userPosts = new();

        foreach (Post post in _posts)
        {
            if (post.UserId == userId)
            {
                userPosts.Add(post);
            }
        }
        return userPosts;
    }

    public void Add(Post post)
    {
        _posts.Add(post);
    }

    public void Update(Post post)
    {
        Post? existingPost = GetById(post.Id);

        if (existingPost == null)
        {
            return;
        }

        existingPost.Title = post.Title;
        existingPost.Content = post.Content;
    }

    public void Delete(Post post)
    {
        _posts.Remove(post);
    }
}