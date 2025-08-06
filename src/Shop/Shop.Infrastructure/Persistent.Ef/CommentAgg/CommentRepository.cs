using Shop.Domain.CommentAgg;
using Shop.Domain.CommentAgg.Enums;
using Shop.Infrastructure.Utilities;

namespace Shop.Infrastructure.Persistent.Ef.CommentAgg;

public class CommentRepository : BaseRepository<Comment>, ICommentRepository
{
    private readonly ShopContext _context;
    public CommentRepository(ShopContext context) : base(context)
    {
        _context = context;
    }

    public Task DeleteComment(Comment comment)
    {
        _context.Comments.Remove(comment);
        return Task.CompletedTask;
    }
}