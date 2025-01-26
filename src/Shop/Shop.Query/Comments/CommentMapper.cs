using Shop.Domain.CommentAgg;
using Shop.Infrastructure.Persistent.Ef.OrderAgg;
using Shop.Query.Comments.DTOs;

namespace Shop.Query.Comments;

internal static class CommentMapper
{
    public static CommentDto? MapNullable(this Comment? comment)
    {
        if (comment == null) return null;
        return new CommentDto
        {
            Id = comment.Id,
            CreationDate = comment.CreationDate,
            ProductId = comment.ProductId,
            Status = comment.Status,
            Text = comment.Text,
            UserId = comment.UserId
        };
    }

    public static CommentDto Map(this Comment comment)
    {
        return new CommentDto
        {
            Id = comment.Id,
            CreationDate = comment.CreationDate,
            ProductId = comment.ProductId,
            Status = comment.Status,
            Text = comment.Text,
            UserId = comment.UserId
        };
    }
}