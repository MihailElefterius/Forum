using Forum.Models;

namespace Forum.Repositories;

public class CommentRepository : ICommentRepository
{
    private readonly List<Comment> _comments = new();

    public Comment? GetById(int id)
    {
        foreach (Comment comment in _comments)
        {
            if (comment.Id == id)
            {
                return comment;
            }
        }

        return null;
    }

    public IEnumerable<Comment> GetAll()
    {
        return _comments;
    }

    public IEnumerable<Comment> GetByPostId(int postId)
    {
        List<Comment> postComments = new();

        foreach (Comment comment in _comments)
        {
            if (comment.PostId == postId)
            {
                postComments.Add(comment);
            }
        }

        return postComments;
    }

    public IEnumerable<Comment> GetByUserId(int userId)
    {
        List<Comment> userComments = new();

        foreach (Comment comment in _comments)
        {
            if (comment.UserId == userId)
            {
                userComments.Add(comment);
            }
        }

        return userComments;
    }

    public void Add(Comment comment)
    {
        _comments.Add(comment);
    }

    public void Update(Comment comment)
    {
        Comment? existingComment = GetById(comment.Id);

        if (existingComment == null)
        {
            return;
        }

        existingComment.Content = comment.Content;
    }

    public void Delete(Comment comment)
    {
        _comments.Remove(comment);
    }
}