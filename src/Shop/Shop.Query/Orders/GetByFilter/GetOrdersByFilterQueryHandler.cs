using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Orders.DTOs;

namespace Shop.Query.Orders.GetByFilter;

public class GetOrdersByFilterQueryHandler : IQueryHandler<GetOrdersByFilterQuery, OrderFilterResult>
{
    private readonly ShopContext _context;

    public GetOrdersByFilterQueryHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<OrderFilterResult> Handle(GetOrdersByFilterQuery request, CancellationToken cancellationToken)
    {
        var result = _context.Orders.OrderByDescending(x => x.Id).AsQueryable();
        var @param = request.FilterParams;

        if (param.Status is not null)
            result = result.Where(x => x.Status == param.Status);

        if (param.UserId is not null)
            result = result.Where(x => x.UserId == param.UserId);

        if (param.StartDate is not null)
            result = result.Where(x => x.CreationDate.Date >= param.StartDate.Value.Date);

        if (param.EndDate is not null)
            result = result.Where(x => x.CreationDate <= param.EndDate.Value.Date);


        var skip = (param.PageId - 1) * param.Take;

        var model = new OrderFilterResult()
        {
            Data = await result
                .Skip(skip)
                .Take(param.Take)
                .Select(order => order.MapFilterData(_context))
                .ToListAsync(cancellationToken),
            FilterParams = param
        };
        model.GeneratePaging(result, param.Take, param.PageId);

        return model;
    }
}