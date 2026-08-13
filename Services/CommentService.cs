using Forum.Models;
using Forum.Repositories;

namespace Forum.Services;

public class CommentService : ICommentService
{
    private readonly ICommentRepository _commentRepository;
    private readonly IUserRepository _userRepository;
    private readonly IPostRepository _postRepository;

    public CommentService(
        ICommentRepository commentRepository,
        IUserRepository userRepository,
        IPostRepository postRepository)
    {
        _commentRepository = commentRepository;
        _userRepository = userRepository;
        _postRepository = postRepository;
    }

    public Comment? GetById(int id)
    {
        return _commentRepository.GetById(id);
    }

    public IEnumerable<Comment> GetAll()
    {
        return _commentRepository.GetAll();
    }

    public IEnumerable<Comment> GetByPostId(int postId)
    {
        return _commentRepository.GetByPostId(postId);
    }

    public IEnumerable<Comment> GetByUserId(int userId)
    {
        return _commentRepository.GetByUserId(userId);
    }

    public void Create(Comment comment)
    {
        User? user = _userRepository.GetById(comment.UserId);

        if (user == null)
        {
            throw new InvalidOperationException("User not found.");
        }

        Post? post = _postRepository.GetById(comment.PostId);

        if (post == null)
        {
            throw new InvalidOperationException("Post not found.");
        }

        _commentRepository.Add(comment);
    }

    public void Update(Comment comment)
    {
        Comment? existingComment = _commentRepository.GetById(comment.Id);

        if (existingComment == null)
        {
            throw new InvalidOperationException("Comment not found.");
        }

        _commentRepository.Update(comment);
    }

    public void Delete(int id)
    {
        Comment? comment = _commentRepository.GetById(id);

        if (comment == null)
        {
            throw new InvalidOperationException("Comment not found.");
        }

        _commentRepository.Delete(comment);
    }
}