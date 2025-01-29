using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Comments.DTOs;

namespace Shop.Query.Comments.GetByFilter;

public class GetCommentByFilterQueryHandler : IQueryHandler<GetCommentByFilterQuery, CommentFilterResult>
{
    private readonly ShopContext _context;

    public GetCommentByFilterQueryHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<CommentFilterResult> Handle(GetCommentByFilterQuery request, CancellationToken cancellationToken)
    {
        var param = request.FilterParams;
        var result = _context.Comments.OrderByDescending(x => x.CreationDate).AsQueryable();

        if (param.Status is not null)
            result = result.Where(x => x.Status == param.Status);

        if (param.UserId is not null)
            result = result.Where(x => x.UserId == param.UserId);

        if (param.StartDate is not null)
            result = result.Where(x => x.CreationDate.Date >= param.StartDate.Value.Date);

        if (param.EndDate is not null)
            result = result.Where(x => x.CreationDate <= param.EndDate.Value.Date);

        var skip = (param.PageId - 1) * param.Take;

        var model = new CommentFilterResult()
        {
            Data = await result.Skip(skip).Take(param.Take).Select(comment => comment.Map())
                .ToListAsync(cancellationToken),
            FilterParams = param
        };
        model.GeneratePaging(result, param.Take, param.PageId);

        return model;
    }
}