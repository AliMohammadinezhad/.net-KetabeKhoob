using Common.Query;
using Shop.Application.Users.Register;
using Shop.Domain.CommentAgg;
using Shop.Query.Comments.DTOs;

namespace Shop.Query.Comments.GetByFilter;

public class GetCommentByFilterQuery : QueryFilter<CommentFilterResult, CommentFilterParams>
{
    public GetCommentByFilterQuery(CommentFilterParams filterParams) : base(filterParams)
    {
    }
}